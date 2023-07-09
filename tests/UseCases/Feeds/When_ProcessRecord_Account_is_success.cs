using interview.test.ensek.Core.Domain.Common;

namespace interview.test.ensek.Tests.UseCases.Feeds;

public sealed class ProcessRecordAccountWithSuccessTests
{
    public ProcessRecordAccountWithSuccessTests() : base()
    {
    }

    [Fact]
    public void When_ProcessRecord_MeterReading_is_success()
    {
        var rowNumber = 10;
        var account = new Account(new AccountId("2344"), new FirstName("Tommy"), new LastName("Test"));

        var sut = ProcessedRecord<Account>.WithSuccess(rowNumber, account);

        sut.Should().NotBeNull();
        sut.IsSuccess.Should().BeTrue();
        sut.Result.Should().NotBeNull();
        sut.FailureReason.Should().BeNull();
        sut.RowNumber.Should().Be(rowNumber);
        sut.Result!.AccountId.Should().Be(account.AccountId);
        sut.Result!.FirstName.Should().Be(account.FirstName);
        sut.Result!.LastName.Should().Be(account.LastName);
    }
}