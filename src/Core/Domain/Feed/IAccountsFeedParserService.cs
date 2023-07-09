using interview.test.ensek.Core.Domain.Common;

namespace interview.test.ensek.Core.Domain.Feed;

/// <summary>
/// Attempts to parser each line from source stream.  
/// </summary>
public interface IAccountsFeedParserService
{
    IEnumerable<ProcessedRecord<Account>> Read(Stream source);
}
