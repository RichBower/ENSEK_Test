using CsvHelper;
using interview.test.ensek.Core.Domain.Common;
using interview.test.ensek.Core.Domain.Feed;

namespace interview.test.ensek.Infrastructure.Services;

public sealed class CsvReaderAccountsFeedService : CsvReaderFeedServiceBase<Account>, IAccountsFeedParserService
{
    const int RequiredColumnCount = 3;

    public CsvReaderAccountsFeedService()
        : base()
    {

    }

    protected override bool DoesRowContainSufficientFields(CsvReader reader) =>  reader.Parser.Count >= RequiredColumnCount;

    protected override Account Map(CsvReader reader)
    {
        var accountId = new AccountId(reader.GetField("AccountId"));
        var firstName = new FirstName(reader.GetField("FirstName"));
        var lastName = new LastName(reader.GetField("LastName"));

        return new Account(accountId, firstName, lastName);
    }
}
