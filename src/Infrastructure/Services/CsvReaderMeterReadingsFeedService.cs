using CsvHelper;
using interview.test.ensek.Core.Domain.Common;
using interview.test.ensek.Core.Domain.Feed;

namespace interview.test.ensek.Infrastructure.Services;

public sealed class CsvReaderMeterReadingsFeedService : CsvReaderFeedServiceBase<MeterReading>, IMeterReadingsFeedParserService
{
    const int RequiredColumnCount = 3;

    public CsvReaderMeterReadingsFeedService()
        :base()
    {
            
    }

    protected override bool DoesRowContainSufficientFields(CsvReader reader) => reader.Parser.Count >= RequiredColumnCount;

    protected override MeterReading Map(CsvReader reader)
    {
        var accountId = new AccountId(reader.GetField("AccountId"));
        var date = new MeterReadingDateTime(reader.GetField("MeterReadingDateTime"));
        var value = new MeterReadValue(reader.GetField("MeterReadValue"));

        return new MeterReading(accountId, date, value);
    }
}
