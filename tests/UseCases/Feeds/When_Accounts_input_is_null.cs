using interview.test.ensek.Core.Domain.Common;

namespace interview.test.ensek.Tests.UseCases.Feeds;
public sealed class AccountsFeedInputIsNullTests : AccountsFeedBase
{
    public AccountsFeedInputIsNullTests() : base()
    {
    }

    [Theory]
    [MemberData(nameof(Data))]
    public void When_input_is_null(string input, List<ProcessedRecord<Account>> expected)
    {
        Runner(input, expected);
    }

    public static IEnumerable<object[]> Data()
    {
        yield return new object[]
        {
            string.Empty,
             new List<ProcessedRecord<Account>>()
        };
        yield return new object[]
        {
           null,
             new List<ProcessedRecord<Account>>()
        };
    }
}
