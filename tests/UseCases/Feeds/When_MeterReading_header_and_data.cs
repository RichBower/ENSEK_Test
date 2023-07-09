
namespace interview.test.ensek.Tests.UseCases.Feeds;
public sealed class MeterReadingHeaderAndDataTests : MeterReadingsFeedBase
{
    public MeterReadingHeaderAndDataTests() : base()
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
               AccountId,MeterReadingDateTime,MeterReadValue,
               2349,22/04/2019 12:25,VOID,
            ",
            new List<List<string>> {
                new List<string> { "2349", "22/04/2019 12:25", "VOID" }
            }
            };
        yield return new object[]
            {
            @"
               AccountId,MeterReadingDateTime,MeterReadValue,
               2349,22/04/2019 12:25,VOID,

                2344,22/04/2019 09:24,1002,
               2233,22/04/2019 12:25,323,


            ",
            new List<List<string>> {
                new List<string> { "2349", "22/04/2019 12:25", "VOID" },
                new List<string> { "2344", "22/04/2019 09:24", "1002" },
                new List<string> { "2233", "22/04/2019 12:25", "323" }
            }
            };
        yield return new object[]
            {
            @"
               AccountId,MeterReadingDateTime,MeterReadValue,
               2349,22/04/2019 12:25,VOID,

                2344,22/04/2019 09:24,1002,
               2233,22/04/2019 12:25,323,


            1248,26/05/2019 09:24,3467,",
            new List<List<string>> {
                new List<string> { "2349", "22/04/2019 12:25", "VOID" },
                new List<string> { "2344", "22/04/2019 09:24", "1002" },
                new List<string> { "2233", "22/04/2019 12:25", "323" },
                new List<string> { "1248", "26/05/2019 09:24", "3467" }
            }
            };
    }
}

