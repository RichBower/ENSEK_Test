using CsvHelper;
using interview.test.ensek.Core.Domain.Feed;

namespace interview.test.ensek.Infrastructure.Services;

public sealed class CsvReaderMeterReadingsFeedService : CsvReaderFeedServiceBase<MeterReadingRecord>, IMeterReadingsFeedParserService
{
    const int RequiredColumnCount = 3;

    public CsvReaderMeterReadingsFeedService()
        :base()
    {
            
    }

    protected override bool IsRowValid(CsvReader reader) => reader.Parser.Count >= RequiredColumnCount;

    protected override MeterReadingRecord Map(CsvReader reader) 
    {
        var accountId = reader.GetField("AccountId") ?? string.Empty;
        var date = reader.GetField("MeterReadingDateTime") ?? string.Empty;
        var value = reader.GetField("MeterReadValue") ?? string.Empty;

        return new MeterReadingRecord(accountId, date, value);
    }
}
