
using interview.test.ensek.Core.Domain.Common;

namespace interview.test.ensek.Tests.UseCases.Feeds;

public abstract class MeterReadingsFeedBase
{
    protected readonly IMeterReadingsFeedParserService _sut;

    protected MeterReadingsFeedBase()
    {
        _sut = new CsvReaderMeterReadingsFeedService();
    }

    protected void Runner(string input, List<ProcessedRecord<MeterReading>> expected)
    {
        using var ms = string.IsNullOrEmpty(input) ? new MemoryStream() : new MemoryStream(Encoding.ASCII.GetBytes(input));
        var collection = _sut.Read(ms);

        collection.Should().NotBeNull();
        var result = collection.ToList();

        result.Should().HaveSameCount(expected);

        for (var rowIndex = 0; rowIndex < expected.Count(); rowIndex++)
        {
            var left = result.ElementAtOrDefault(rowIndex);
            var right = expected.ElementAtOrDefault(rowIndex);

            left.RowNumber.Should().Be(right.RowNumber);
            left.Result.Should().Be(right.Result);
            left.IsSuccess.Should().Be(right.IsSuccess);
            if (right.IsSuccess == false)
            {
                left.FailureReason.Should().NotBeNull();
                right.FailureReason.Should().NotBeNull();

                left.FailureReason!.Message.Should().Be(right.FailureReason!.Message);
            }
        }
    }
}
