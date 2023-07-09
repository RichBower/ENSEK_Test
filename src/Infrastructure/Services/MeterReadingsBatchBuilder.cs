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

    readonly Dictionary<int, MeterReading> _currentBatch = new Dictionary<int, MeterReading>();


    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _meterReadingsRepository.SaveBatchAsync(_currentBatch.Values, cancellationToken);
    }

    public async Task<AddToBatchResult> TryAddAsync(MeterReading source, CancellationToken cancellationToken)
    {
        // Ensure the account exists
        if (await DoesTheAccountExist(source.AccountId, cancellationToken) == false)
        {
            return AddToBatchResult.WithFailure(BatchException.AccountIdDoesNotExist(source.AccountId));
        }

        // Ensure the entry is not already part of this batch
        if (DoesTheBatchAlreadyContainTheReading(source.AccountId, source.MeterReadValue) == true)
        {
            return AddToBatchResult.WithFailure(BatchException.MeterReadingAlreadyExists(source.AccountId, source.MeterReadingDateTime));
        }

        // Ensure the entry is newer than matching item in batch
        if (DoesTheBatchAlreadyContainANewerReading(source.AccountId, source.MeterReadingDateTime) == true)
        {
            return AddToBatchResult.WithFailure(BatchException.MeterReadingIsOld(source.AccountId, source.MeterReadingDateTime));
        }

        _currentBatch.Add(source.AccountId.Value, source);

        return AddToBatchResult.WithSuccess();
    }

    private bool DoesTheBatchAlreadyContainANewerReading(AccountId accountId, MeterReadingDateTime meterReadingDateTime)
    {
        if(_currentBatch.TryGetValue(accountId.Value, out var itemInBatch) == false)
        {
            return false;
        }

        return (itemInBatch.MeterReadingDateTime.Value > meterReadingDateTime.Value);
    }

    private bool DoesTheBatchAlreadyContainTheReading(AccountId accountId, MeterReadValue meterReadValue)
    {
        if (_currentBatch.TryGetValue(accountId.Value, out var itemInBatch) == false)
        {
            return false;
        }

        return (itemInBatch.MeterReadValue.Value != meterReadValue.Value);
    }

    private async Task<bool> DoesTheAccountExist(AccountId accountId, CancellationToken cancellationToken)
    {
        // Save a DB lookup
        if (_currentBatch.TryGetValue(accountId.Value, out var itemInBatch) == true)
        {
            return true;
        }

        var account = await _accountsRespository.GetAccountAsync(accountId, cancellationToken);

        return !(account is null);
    }
}
