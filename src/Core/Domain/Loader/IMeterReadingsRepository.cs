using interview.test.ensek.Core.Domain.Common;

namespace interview.test.ensek.Core.Domain.Loader;

public interface IMeterReadingsRepository
{
    Task<int> SaveBatchAsync(IReadOnlyCollection<MeterReading> meterReadings, CancellationToken cancellationToken);
}
