using interview.test.ensek.Core.Domain.Common;

namespace interview.test.ensek.Tests.UseCases.Feeds;

public sealed class AccountsFeedContainsWhitespaceTests : AccountsFeedBase
{

    public AccountsFeedContainsWhitespaceTests() : base()
    {
    }

    [Theory]
    [MemberData(nameof(Data))]
    public void When_column_in_data_row_contains_whitespace(string input, List<ProcessedRecord<Account>> expected)
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
            new List<ProcessedRecord<Account>> { 
                ProcessedRecord<Account>.WithFailure(3, FeedException.LastNameCannotBeNull()) 
            } 
        };
        yield return new object[]
        {
            @"
               AccountId,FirstName,LastName
               2344,Tommy,  

                ,Tim,Test
            ",
            new List<ProcessedRecord<Account>> {
                ProcessedRecord<Account>.WithFailure(3, FeedException.LastNameCannotBeNull()),
                ProcessedRecord<Account>.WithFailure(5, FeedException.AccountIdCannotBeNull())
            }
        };
        yield return new object[]
         {
            @"
               AccountId,FirstName,LastName
                 , ,  

                ,Tim,Test
            ",
           new List<ProcessedRecord<Account>> {
                ProcessedRecord<Account>.WithFailure(3, FeedException.AccountIdCannotBeNull()),
                ProcessedRecord<Account>.WithFailure(5, FeedException.AccountIdCannotBeNull())
            }
         };
    }
}
