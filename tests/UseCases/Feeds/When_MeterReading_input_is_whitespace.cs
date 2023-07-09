
namespace interview.test.ensek.Tests.UseCases.Feeds;

public sealed partial class MeterReadingInputIsWhitespaceTests : MeterReadingsFeedBase
{

    public MeterReadingInputIsWhitespaceTests() : base()
    {
    }

    [Theory]
    [MemberData(nameof(Data))]
    public void When_input_is_whitelines(string input, List<List<string>> expected)
    {
        Runner(input, expected);
    }

    public static IEnumerable<object[]> Data()
    {
        yield return new object[]
        {
            @"
            

            ",
             new List<List<string>>()
        };
        yield return new object[]
        {
            @" ",
            new List<List<string>>()
        };
        yield return new object[]
       {
            @"",
            new List<List<string>>()
       };
    }
}
