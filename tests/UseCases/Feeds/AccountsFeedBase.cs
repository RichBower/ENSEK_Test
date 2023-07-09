using System.Text;


namespace interview.test.ensek.Tests.UseCases.Feeds;

public abstract class AccountsFeedBase
{
    protected readonly IAccountsFeedParserService _sut;

    protected AccountsFeedBase()
    {
        _sut = new CsvReaderAccountsFeedService();
    }

    protected void Runner(string input, List<List<string>> expected)
    {
        using var ms = string.IsNullOrEmpty(input) ? new MemoryStream() : new MemoryStream(Encoding.ASCII.GetBytes(input));
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
