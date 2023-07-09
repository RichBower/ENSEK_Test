namespace interview.test.ensek.Tests.UseCases.Feeds;

public sealed class AccountsFeedContainsWhitespaceTests : AccountsFeedBase
{

    public AccountsFeedContainsWhitespaceTests() : base()
    {
    }

    [Theory]
    [MemberData(nameof(Data))]
    public void When_column_in_data_row_contains_whitespace(string input, List<List<string>> expected)
    {
        Runner(input, expected);
    }

    public static IEnumerable<object[]> Data()
    {
        yield return new object[] 
        {
            @"
               AccountId,FirstName,LastName
               2344,Tommy,  
            ",
            new List<List<string>> { new List<string> { "2344", "Tommy", string.Empty } }
        };
        yield return new object[]
        {
            @"
               AccountId,FirstName,LastName
               2344,Tommy,  

                ,Tim,Test
            ",
            new List<List<string>> 
            { 
                new List<string> { "2344", "Tommy", string.Empty },
                new List<string> { string.Empty, "Tim", "Test" }
            }
        };
        yield return new object[]
         {
            @"
               AccountId,FirstName,LastName
                 , ,  

                ,Tim,Test
            ",
            new List<List<string>>
            {
                new List<string> { string.Empty, string.Empty, string.Empty },
                new List<string> { string.Empty, "Tim", "Test" }

            }
         };
    }
}
