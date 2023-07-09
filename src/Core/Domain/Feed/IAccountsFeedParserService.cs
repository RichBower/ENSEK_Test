using interview.test.ensek.Core.Domain.Common;

namespace interview.test.ensek.Core.Domain.Feed;

public interface IAccountsFeedParserService
{
    IEnumerable<ProcessedRecord<Account>> Read(Stream source);
}
