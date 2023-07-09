using interview.test.ensek.Core.Domain.Common;

namespace interview.test.ensek.Tests.UseCases.Feeds;

public sealed class ProcessRecordMeterReadingWithFailureTests
{
    public ProcessRecordMeterReadingWithFailureTests() : base()
    {
    }

    [Fact]
    public void When_ProcessRecord_MeterReading_is_failure()
    {
        var rowNumber = 10;
        var ex = FeedException.AccountIdCannotBeNull();

        var sut = ProcessedRecord<MeterReading>.WithFailure(10, FeedException.AccountIdCannotBeNull());

        sut.Should().NotBeNull();
        sut.IsSuccess.Should().BeFalse();
        sut.Result.Should().BeNull();
        sut.FailureReason.Should().NotBeNull();
        sut.FailureReason!.Message.Should().NotBeNull();
        sut.FailureReason!.Message.Should().Be(ex.Message);
        sut.RowNumber.Should().Be(rowNumber);
    }
}