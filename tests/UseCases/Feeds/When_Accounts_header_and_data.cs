namespace interview.test.ensek.Tests.UseCases.Feeds;
public sealed class AccountsFeedHeaderAndDataTests : AccountsFeedBase
{
    public AccountsFeedHeaderAndDataTests() : base()
    {
    }

    [Theory]
    [MemberData(nameof(Data))]
    public void When_header_and_many_data_rows(string input, List<List<string>> expected)
    {
        Runner(input, expected);
    }

    public static IEnumerable<object[]> Data()
    {
        yield return new object[]
        {
            @"
               AccountId,FirstName,LastName


               2344,Tommy,Test



            ",
            new List<List<string>> {
                new List<string> { "2344", "Tommy", "Test" }

            }
        };
        yield return new object[]
        {
            @"
               AccountId,FirstName,LastName


              2344,Tommy,Test
              4534,JOSH,TEST


              1234,Freya,Test",
            new List<List<string>>
            {
                new List<string> { "2344", "Tommy","Test" },
                new List<string> { "4534", "JOSH", "TEST" },
                new List<string> { "1234", "Freya", "Test" },

            }
        };
        yield return new object[]
         {
            @"
               AccountId,FirstName,LastName
               1234,Freya,Test",
            new List<List<string>>
            {
                 new List<string> { "1234", "Freya", "Test" }
            }
         };
    }
}

