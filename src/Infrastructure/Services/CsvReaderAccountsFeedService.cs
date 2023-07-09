using CsvHelper;
using interview.test.ensek.Core.Domain.Feed;

namespace interview.test.ensek.Infrastructure.Services;

public sealed class CsvReaderAccountsFeedService : CsvReaderFeedServiceBase<AccountRecord>, IAccountsFeedParserService
{
    const int RequiredColumnCount = 3;

    public CsvReaderAccountsFeedService()
        : base()
    {

    }

    protected override bool IsRowValid(CsvReader reader) => reader.Parser.Count >= RequiredColumnCount;

    protected override AccountRecord Map(CsvReader reader)
    {
        var accountId = reader.GetField("AccountId") ?? string.Empty;
        var firstName = reader.GetField("FirstName") ?? string.Empty;
        var secondName = reader.GetField("SecondName") ?? string.Empty;

        return new AccountRecord(accountId, firstName, secondName);
    }
}
