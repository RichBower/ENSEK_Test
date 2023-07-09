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

    public readonly Dictionary<int, MeterReading> _currentBatch = new Dictionary<int, MeterReading>();

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _meterReadingsRepository.SaveBatchAsync(_currentBatch.Values, cancellationToken);
    }

    /// <summary>
    /// The current solution seems to produce the correct results (25 good and 10 bad rows); 
    /// however, not sure if the requirements are for additional files to be loaded, the same file reloaded.
    /// TODO: Check against MeterReadings table as well as local cache.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<AddToBatchResult> TryAddAsync(MeterReading source, CancellationToken cancellationToken)
    {
        // Ensure the account exists
        if (await ShouldRecordBeIgnoredBecauseAccountIdIsInvalid(source.AccountId, cancellationToken) == true)
        {
            return AddToBatchResult.WithFailure(BatchError.AccountIdDoesNotExist(source.AccountId));
        }

        // Ensure the entry is newer than matching item in batch
        if (ShouldRecordBeIgnoreBecauseItIsTooOld(source.AccountId, source.MeterReadingDateTime) == true)
        {
            return AddToBatchResult.WithFailure(BatchError.MeterReadingIsOld(source.AccountId, source.MeterReadingDateTime));
        }

        // Ensure the entry is not already part of this batch - an item is deemed to be a match if all fields match
        // TODO: Not sure this is right.  The previous check will return if the record date is older than 
        if (ShouldRecordBeIgnoredBecauseItIsADuplicate(source.AccountId, source.MeterReadingDateTime, source.MeterReadValue) == true)
        {
            return AddToBatchResult.WithFailure(BatchError.MeterReadingAlreadyExists(source.AccountId, source.MeterReadingDateTime));
        }

        // Replace the current value
        _currentBatch[source.AccountId.Value] = source;

        return AddToBatchResult.WithSuccess();
    }

    private bool ShouldRecordBeIgnoreBecauseItIsTooOld(AccountId accountId, MeterReadingDateTime meterReadingDateTime)
    {
        if(_currentBatch.TryGetValue(accountId.Value, out var itemInBatch) == false)
        {
            return false;
        }

        return (itemInBatch.MeterReadingDateTime.Value > meterReadingDateTime.Value);
    }

    private bool ShouldRecordBeIgnoredBecauseItIsADuplicate(AccountId accountId, MeterReadingDateTime meterReadingDateTime, MeterReadValue meterReadValue)
    {
        if (_currentBatch.TryGetValue(accountId.Value, out var itemInBatch) == false)
        {
            return false;
        }

        return (itemInBatch.MeterReadValue.Value == meterReadValue.Value && itemInBatch.MeterReadingDateTime == meterReadingDateTime);
    }

    private async Task<bool> ShouldRecordBeIgnoredBecauseAccountIdIsInvalid(AccountId accountId, CancellationToken cancellationToken)
    {
        // Save a DB lookup
        if (_currentBatch.TryGetValue(accountId.Value, out _) == true)
        {
            return false;
        }

        var account = await _accountsRespository.GetAccountAsync(accountId, cancellationToken);

        return (account is null);
    }
}
