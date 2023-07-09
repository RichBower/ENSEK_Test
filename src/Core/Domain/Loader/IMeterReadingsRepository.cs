using interview.test.ensek.Core.Domain.Common;

namespace interview.test.ensek.Core.Domain.Loader;

public interface IMeterReadingsRepository
{
    Task<int> SaveBatchAsync(IReadOnlyCollection<MeterReading> meterReadings, CancellationToken cancellationToken);

    Task<bool> DoesNewerReadingExistAsync(AccountId accountId, MeterReadingDateTime meterReadingDateTime, CancellationToken cancellationToken);

    Task<bool> IsMeterReadingUniqueAsync(AccountId accountId, MeterReadingDateTime meterReadingDateTime, MeterReadValue meterReadValue, CancellationToken cancellationToken);

}
