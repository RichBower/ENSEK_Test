namespace interview.test.ensek.Tests.UseCases.Feeds;
public sealed class AccountsFeedRowContainsNoDelimeterTests : AccountsFeedBase
{
    public AccountsFeedRowContainsNoDelimeterTests() 
        : base()
    {
    }

    [Theory]
    [MemberData(nameof(Data))]
    public void When_data_row_contains_no_delim(string input, List<List<string>> expected)
    {
        Runner(input, expected);
    }

    public static IEnumerable<object[]> Data()
    {
      

        yield return new object[]
        {
            @"
               AccountId,FirstName,LastName
               2344

                2344,Tommy,Test
              4534,JOSH,TEST
            ",
            new List<List<string>> {
                 new List<string> { "2344", "Tommy","Test" },
                new List<string> { "4534", "JOSH", "TEST" },
            }
        };
        yield return new object[]
        {
            @"
               AccountId,FirstName,LastName


              TimTest



            ",
             new List<List<string>>()
        };
        yield return new object[]
         {
            @"
               AccountId,FirstName,LastName
                2344,Tommy,Test

               2344

              TimTest


            4534,JOSH,TEST",
            new List<List<string>> {
                 new List<string> { "2344", "Tommy","Test" },
                new List<string> { "4534", "JOSH", "TEST" },
            }
         };
    }
}
