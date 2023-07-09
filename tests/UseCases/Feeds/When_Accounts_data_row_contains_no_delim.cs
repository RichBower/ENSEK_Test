using interview.test.ensek.Core.Domain.Common;

namespace interview.test.ensek.Tests.UseCases.Feeds;
public sealed class AccountsFeedRowContainsNoDelimeterTests : AccountsFeedBase
{
    public AccountsFeedRowContainsNoDelimeterTests() 
        : base()
    {
    }

    [Theory]
    [MemberData(nameof(Data))]
    public void When_data_row_contains_no_delim(string input, List<ProcessedRecord<Account>> expected)
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
            new List<ProcessedRecord<Account>> {
                ProcessedRecord<Account>.WithFailure(3, FeedException.InsufficientFields()),
                ProcessedRecord<Account>.WithSuccess(5, new Account(new AccountId("2344"), new FirstName("Tommy"), new LastName("Test"))),
                ProcessedRecord<Account>.WithSuccess(6, new Account(new AccountId("4534"), new FirstName("JOSH"), new LastName("TEST")))
            }
        };
        yield return new object[]
        {
            @"
               AccountId,FirstName,LastName


              TimTest



            ",
            new List<ProcessedRecord<Account>> {
                ProcessedRecord<Account>.WithFailure(5, FeedException.InsufficientFields()),
            }
        };
        yield return new object[]
         {
            @"
               AccountId,FirstName,LastName
                2344,Tommy,Test

               2344

              TimTest


            4534,JOSH,TEST",
           new List<ProcessedRecord<Account>> {
                ProcessedRecord<Account>.WithSuccess(3, new Account(new AccountId("2344"), new FirstName("Tommy"), new LastName("Test"))),
                ProcessedRecord<Account>.WithFailure(5, FeedException.InsufficientFields()),
                ProcessedRecord<Account>.WithFailure(7, FeedException.InsufficientFields()),
                ProcessedRecord<Account>.WithSuccess(10, new Account(new AccountId("4534"), new FirstName("JOSH"), new LastName("TEST")))

            }
         };
    }
}
