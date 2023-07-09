
namespace interview.test.ensek.Tests.UseCases.Feeds;
public sealed class MeterReadingColumnIsEmptyTests : MeterReadingsFeedBase
{

    public MeterReadingColumnIsEmptyTests() : base()
    {
    }

    [Theory]
    [MemberData(nameof(Data))]
    public void When_column_in_data_row_is_empty(string input, List<List<string>> expected)
    {
        Runner(input, expected);
    }

    public static IEnumerable<object[]> Data()
    {
        yield return new object[]
       {
            @"
               AccountId,MeterReadingDateTime,MeterReadValue,
               2344,22/04/2019 09:24,,
            ",
            new List<List<string>> {
                new List<string> { "2344", "22/04/2019 09:24", string.Empty }
            }
       };
        yield return new object[]
        {
            @"
               AccountId,MeterReadingDateTime,MeterReadValue,
               2344,22/04/2019 09:24,     ,


             ,22/04/2019 12:25,VOID,
            ",
            new List<List<string>>
            {
              new List<string> { "2344", "22/04/2019 09:24", string.Empty },
              new List<string> { string.Empty, "22/04/2019 12:25", "VOID" }


            }
        };
        yield return new object[]
         {
            @"
               AccountId,MeterReadingDateTime,MeterReadValue,
               2344,22/04/2019 09:24,,


               ,22/04/2019 12:25,VOID,
                ,  ,     ,",
            new List<List<string>>
            {
                new List<string> { "2344", "22/04/2019 09:24", string.Empty },
                new List<string> { string.Empty, "22/04/2019 12:25", "VOID" },
                new List<string> { string.Empty, string.Empty, string.Empty },
            }
         };
    }
}
