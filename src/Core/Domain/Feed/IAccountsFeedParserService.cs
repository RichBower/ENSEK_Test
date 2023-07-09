namespace interview.test.ensek.Core.Domain.Feed;

public interface IAccountsFeedParserService
{
    IEnumerable<AccountRecord> Read(Stream source);
}
