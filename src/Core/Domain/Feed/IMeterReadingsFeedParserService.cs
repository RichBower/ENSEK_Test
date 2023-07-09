using interview.test.ensek.Core.Domain.Common;

namespace interview.test.ensek.Core.Domain.Feed;


public interface IMeterReadingsFeedParserService
{
    IEnumerable<ProcessedRecord<MeterReading>> Read(Stream source);
}
