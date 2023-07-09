
namespace interview.test.ensek.Tests.UseCases.Feeds;

public abstract class MeterReadingsFeedBase
{
    protected readonly IMeterReadingsFeedParserService _sut;

    protected MeterReadingsFeedBase()
    {
        _sut = new CsvReaderMeterReadingsFeedService();
    }

    protected void Runner(string input, List<List<string>> expected)
    {
        using var ms =  string.IsNullOrEmpty(input) ? new MemoryStream() :  new MemoryStream(Encoding.ASCII.GetBytes(input));
        var result = _sut.Read(ms);

        result.Should().NotBeNull();

        result.Should().HaveSameCount(expected);

        for (var rowIndex = 0; rowIndex < result.Count(); rowIndex++)
        {
            var left = result.ElementAtOrDefault(rowIndex);
            var right = expected.ElementAtOrDefault(rowIndex);

            left.Should().Equals(right);
        }
    }
}
