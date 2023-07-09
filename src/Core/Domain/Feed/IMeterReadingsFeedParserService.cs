using interview.test.ensek.Core.Domain.Common;

namespace interview.test.ensek.Core.Domain.Feed;

/// <summary>
/// Parses each row from source stream.  Attempts to report why a row cannot be parsed into required output
/// </summary>
public interface IMeterReadingsFeedParserService
{
    IEnumerable<ProcessedRecord<MeterReading>> Read(Stream source);
}
