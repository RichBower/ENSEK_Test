
namespace interview.test.ensek.Core.Domain.Feed;


public interface IMeterReadingsFeedParserService
{
    IEnumerable<MeterReadingRecord> Read(Stream source);
}
