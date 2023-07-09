

namespace interview.test.ensek.Tests.UseCases.MeterReadingsBatchBuilder;

public sealed class BatchBuilderValidAccountTests
{
    [Fact]
    public async void When_TryAdd_Account_That_Does_Not_Exit()
    {
        var accountsRepositoryMock = new Mock<IAccountsRespository>();
        var meterReadingsRepositoryMock = new Mock<IMeterReadingsRepository>();
        var builder = new interview.test.ensek.Infrastructure.Services.MeterReadingsBatchBuilder(accountsRepositoryMock.Object, meterReadingsRepositoryMock.Object);

        accountsRepositoryMock.Setup(s => s.GetAccountAsync(It.IsAny<AccountId>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult<Account?>(null));
        
        var builderResult = await builder.TryAddAsync(BuilderTestsHelper.ValidMeterReading, CancellationToken.None);

        builderResult.Should_Report_Failure(BatchError.AccountIdDoesNotExist(BuilderTestsHelper.ValidAccount.AccountId));
    }
}

