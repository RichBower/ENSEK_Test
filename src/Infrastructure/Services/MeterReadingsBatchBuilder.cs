using interview.test.ensek.Core.Domain.Common;
using interview.test.ensek.Core.Domain.Loader;

namespace interview.test.ensek.Infrastructure.Services;

public sealed class MeterReadingsBatchBuilder : IMeterReadingBatchBuilder
{
    private readonly IAccountsRespository _accountsRespository;
    private readonly IMeterReadingsRepository _meterReadingsRepository;

    public MeterReadingsBatchBuilder(
        IAccountsRespository accountsRespository,
        IMeterReadingsRepository meterReadingsRepository)
    {
        _accountsRespository = accountsRespository;
        _meterReadingsRepository = meterReadingsRepository;
    }

    /// <summary>
    /// A batch contains every record that should be inserted into the database.  
    /// I prefer bulk inserting the data, assuming the files remain small, rather than 
    /// individual inserts. Ideally the insert would be wrapped in a transaction, just 
    /// to protect concurrency issues, but it's already after 12am.
    /// </summary>
    public readonly Dictionary<int, MeterReading> _currentBatch = new Dictionary<int, MeterReading>();

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _meterReadingsRepository.SaveBatchAsync(_currentBatch.Values, cancellationToken);

        _currentBatch.Clear();
    }

    /// <summary>
    /// The current solution seems to produce the correct results (25 good and 10 bad rows); 
    /// however, not sure if the requirements are for additional files to be loaded, the same file reloaded.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<AddToBatchResult> TryAddAsync(MeterReading source, CancellationToken cancellationToken)
    {
        // Ensure the account exists
        if (await ShouldRecordBeIgnoredBecauseAccountIdIsInvalidAsync(source.AccountId, cancellationToken) == true)
        {
            return AddToBatchResult.WithFailure(BatchError.AccountIdDoesNotExist(source.AccountId));
        }

        // Ensure the entry is newer than matching item in batch
        if (await ShouldRecordBeIgnoreBecauseItIsTooOldAsync(source.AccountId, source.MeterReadingDateTime, cancellationToken) == true)
        {
            return AddToBatchResult.WithFailure(BatchError.MeterReadingIsOld(source.AccountId, source.MeterReadingDateTime));
        }

        // Ensure the entry is not already part of this batch - an item is deemed to be a match if all fields match
        // TODO: Not sure this is right.  The previous check will return if the record date is older than 
        if (await ShouldRecordBeIgnoredBecauseItIsADuplicateAsync(source.AccountId, source.MeterReadingDateTime, source.MeterReadValue, cancellationToken) == true)
        {
            return AddToBatchResult.WithFailure(BatchError.MeterReadingAlreadyExists(source.AccountId, source.MeterReadingDateTime));
        }

        // Replace the current value
        _currentBatch[source.AccountId.Value] = source;

        return AddToBatchResult.WithSuccess();
    }

    private async Task<bool> ShouldRecordBeIgnoreBecauseItIsTooOldAsync(AccountId accountId, MeterReadingDateTime meterReadingDateTime, CancellationToken cancellationToken) =>
        (_currentBatch.TryGetValue(accountId.Value, out var itemInBatch) == true && itemInBatch.MeterReadingDateTime.Value > meterReadingDateTime.Value) ||
            await _meterReadingsRepository.DoesNewerReadingExistAsync(accountId, meterReadingDateTime, cancellationToken) == true;

    private async Task<bool> ShouldRecordBeIgnoredBecauseItIsADuplicateAsync(AccountId accountId, MeterReadingDateTime meterReadingDateTime, MeterReadValue meterReadValue, CancellationToken cancellationToken) =>
        ((_currentBatch.TryGetValue(accountId.Value, out var itemInBatch) == true && itemInBatch.MeterReadValue.Value == meterReadValue.Value && itemInBatch.MeterReadingDateTime == meterReadingDateTime) ||
            await _meterReadingsRepository.IsMeterReadingUniqueAsync(accountId, meterReadingDateTime, meterReadValue, cancellationToken) == false);

    private async Task<bool> ShouldRecordBeIgnoredBecauseAccountIdIsInvalidAsync(AccountId accountId, CancellationToken cancellationToken) =>
        _currentBatch.TryGetValue(accountId.Value, out _) == false && (await _accountsRespository.DoesTheAccountExistAsync(accountId, cancellationToken) == false);
}
