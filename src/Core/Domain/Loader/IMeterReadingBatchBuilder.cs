using interview.test.ensek.Core.Domain.Common;

namespace interview.test.ensek.Core.Domain.Loader;

public interface IMeterReadingBatchBuilder
{
    Task<AddToBatchResult> TryAddAsync(MeterReading source, CancellationToken cancellationToken);

    Task SaveChangesAsync(CancellationToken cancellationToken);
}
