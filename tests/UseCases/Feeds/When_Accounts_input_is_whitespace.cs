using interview.test.ensek.Core.Domain.Common;

namespace interview.test.ensek.Tests.UseCases.Feeds;

public sealed partial class AccountsFeedIsWhitespaceTests : AccountsFeedBase
{

    public AccountsFeedIsWhitespaceTests() : base()
    {
    }

    [Theory]
    [MemberData(nameof(Data))]
    public void When_input_is_whitelines(string input, List<ProcessedRecord<Account>> expected)
    {
        Runner(input, expected);
    }

    public static IEnumerable<object[]> Data()
    {
        yield return new object[]
        {
            @"
            

            ",
             new List<ProcessedRecord<Account>>()
        };
        yield return new object[]
        {
            @" ",
            new List<ProcessedRecord<Account>>()
        };
        yield return new object[]
       {
            @"",
            new List<ProcessedRecord<Account>>()
       };
    }
}
