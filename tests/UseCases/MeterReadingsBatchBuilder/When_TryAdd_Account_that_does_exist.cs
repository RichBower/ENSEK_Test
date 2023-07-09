using interview.test.ensek.Core.Domain.Loader;

namespace interview.test.ensek.Tests.UseCases.MeterReadingsBatchBuilder;

public sealed class BatchBuilderInvalidAccountTests
{
    [Fact]
    public async void When_TryAdd_Account_that_does_exist()
    {
        var account = new Account(new AccountId("2344"), new FirstName("Tommy"), new LastName("Test"));

        var accountsRepositoryMock = new Mock<IAccountsRespository>();
        var meterReadingsRepositoryMock = new Mock<IMeterReadingsRepository>();
        var builder = new interview.test.ensek.Infrastructure.Services.MeterReadingsBatchBuilder(accountsRepositoryMock.Object, meterReadingsRepositoryMock.Object);
        accountsRepositoryMock.Setup(s => s.GetAccountAsync(It.IsAny<AccountId>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult<Account?>(account));
        var builderResult = await builder.TryAddAsync(new MeterReading(account.AccountId, new MeterReadingDateTime("22/04/2019 09:24"), new MeterReadValue("1002")), CancellationToken.None);

        builderResult.Should().NotBeNull();
        builderResult.IsSuccess.Should().BeTrue();
        builderResult.FailureReason.Should().BeNull();
        builderResult.FailureReason?.Message.Should().BeNull();

    }
    
}
