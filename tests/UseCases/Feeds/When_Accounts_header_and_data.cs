namespace interview.test.ensek.Tests.UseCases.Feeds;
public sealed class AccountsFeedHeaderAndDataTests : AccountsFeedBase
{
    public AccountsFeedHeaderAndDataTests() : base()
    {
    }

    [Theory]
    [MemberData(nameof(Data))]
    public void When_header_and_many_data_rows(string input, List<ProcessedRecord<Account>> expected)
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
           new List<ProcessedRecord<Account>> {
                ProcessedRecord<Account>.WithSuccess(5, new Account(new AccountId("2344"), new FirstName("Tommy"), new LastName("Test"))),
            }
        };
        yield return new object[]
        {
            @"
               AccountId,FirstName,LastName


              2344,Tommy,Test
              4534,JOSH,TEST


              1234,Freya,Test",
            new List<ProcessedRecord<Account>> {
                ProcessedRecord<Account>.WithSuccess(5, new Account(new AccountId("2344"), new FirstName("Tommy"), new LastName("Test"))),
                ProcessedRecord<Account>.WithSuccess(6, new Account(new AccountId("4534"), new FirstName("JOSH"), new LastName("TEST"))),
                ProcessedRecord<Account>.WithSuccess(9, new Account(new AccountId("1234"), new FirstName("Freya"), new LastName("Test"))),
            }
        };
        yield return new object[]
         {
            @"
               AccountId,FirstName,LastName
               1234,Freya,Test",
            new List<ProcessedRecord<Account>> {
                ProcessedRecord<Account>.WithSuccess(3, new Account(new AccountId("1234"), new FirstName("Freya"), new LastName("Test"))),
            }
         };
    }
}

