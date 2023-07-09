using System.Threading;
using interview.test.ensek.Core.Domain.Common;
using interview.test.ensek.Core.Domain.Loader;
using Microsoft.EntityFrameworkCore;

namespace interview.test.ensek.Infrastructure.SqlServer;

public sealed class MeterReadingsRespository : IMeterReadingsRepository
{
    const int EmptyBatch = 0;
    public MeterReadingsRespository(ApplicationDbContext context)
    {
        _context = context;
    }

    private ApplicationDbContext _context { get; init; }

    public async Task<bool> DoesNewerReadingExistAsync(AccountId accountId, MeterReadingDateTime meterReadingDateTime, CancellationToken cancellationToken) =>
        await _context.MeterReadings.AnyAsync(mr => mr.AccountEntityID == accountId.Value && mr.MeterReadingDateTime > meterReadingDateTime.Value, cancellationToken) ;

    public async Task<bool> IsMeterReadingUniqueAsync(AccountId accountId, MeterReadingDateTime meterReadingDateTime, MeterReadValue meterReadValue, CancellationToken cancellationToken) =>
        await _context.MeterReadings.AnyAsync(mr => mr.AccountEntityID == accountId.Value && mr.MeterReadingDateTime == meterReadingDateTime.Value && mr.MeterReadValue == meterReadValue.Value, cancellationToken) == false;

    public async Task<int> SaveBatchAsync(IReadOnlyCollection<MeterReading> meterReadings, CancellationToken cancellationToken)
    {
        var entities = new List<MeterReadingEntity>();

        foreach (var meterReading in meterReadings)
        {
            entities.Add(new MeterReadingEntity
            {
                AccountEntityID = meterReading.AccountId.Value,
                MeterReadingDateTime = meterReading.MeterReadingDateTime.Value,
                MeterReadValue = meterReading.MeterReadValue.Value
            });
        }

        if (entities.Count == 0)
        {
            return EmptyBatch;
        }

        await _context.MeterReadings.AddRangeAsync(entities, cancellationToken);
        var result =  await _context.SaveChangesAsync(cancellationToken);

        return result;
    }
}
