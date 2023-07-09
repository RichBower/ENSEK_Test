

namespace interview.test.ensek.Tests.UseCases.Feeds;
public sealed class MeterReadingRowContainsNoDelimeterTests : MeterReadingsFeedBase
{
    public MeterReadingRowContainsNoDelimeterTests()
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
               AccountId,MeterReadingDateTime,MeterReadValue,
               234422/04/2019 09:24
 2344,22/04/2019 09:24,1002,
               2233,22/04/2019 12:25,323,

            ",
            new List<List<string>> {
                new List<string> { "2344", "22/04/2019 09:24", "1002" },
                new List<string> { "2233", "22/04/2019 12:25", "323" },
            }
              };
        yield return new object[]
        {
            @"
               AccountId,MeterReadingDateTime,MeterReadValue,
               234422/04/2019 09:24


              t




            2344,22/04/2019 09:24,1002,
               2233,22/04/2019 12:25,323,",
            new List<List<string>> {
                new List<string> { "2344", "22/04/2019 09:24", "1002" },
                new List<string> { "2233", "22/04/2019 12:25", "323" },
            }
        };
        yield return new object[]
         {
            @"
               AccountId,MeterReadingDateTime,MeterReadValue,
                2344,22/04/2019 09:24,1002
               234422/04/2019 09:24


              t



2233,22/04/2019 12:25,323,
            22/04/2019 09:24",
           new List<List<string>> {
                new List<string> { "2344", "22/04/2019 09:24", "1002" },
                new List<string> { "2233", "22/04/2019 12:25", "323" },
            }
         };
    }
}
