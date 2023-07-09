namespace interview.test.ensek.Tests.UseCases.MeterReadingsBatchBuilder;

public static class BuilderTestsHelper
{
    public static readonly Account ValidAccount = new Account(new AccountId("2344"), new FirstName("Tommy"), new LastName("Test"));
    public static readonly MeterReading ValidMeterReading = new MeterReading(ValidAccount.AccountId, new MeterReadingDateTime("22/04/2019 09:24"), new MeterReadValue("1002"));
    public static readonly MeterReading OldMeterReading = new MeterReading(ValidAccount.AccountId, new MeterReadingDateTime("22/04/2019 08:24"), new MeterReadValue("1002"));

    public static void Should_Report_Failure(this AddToBatchResult source, BatchError expectedError)
    {
        source.Should().NotBeNull();
        source.IsSuccess.Should().BeFalse();
        source.FailureReason.Should().NotBeNull();
        source.FailureReason?.Message.Should().Be(expectedError.Message);
    }

    public static void Should_Report_Success(this AddToBatchResult source )
    {
       source.Should().NotBeNull();
       source.IsSuccess.Should().BeTrue();
       source.FailureReason.Should().BeNull();
       source.FailureReason?.Message.Should().BeNull();
    }

}
