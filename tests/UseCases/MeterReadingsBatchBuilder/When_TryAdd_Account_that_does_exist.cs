using interview.test.ensek.Core.Domain.Loader;

namespace interview.test.ensek.Tests.UseCases.MeterReadingsBatchBuilder;

public sealed class BatchBuilderInvalidAccountTests
{
    [Fact]
    public async void When_TryAdd_Account_that_does_exist()
    {
        var accountsRepositoryMock = new Mock<IAccountsRespository>();
        var meterReadingsRepositoryMock = new Mock<IMeterReadingsRepository>();
        var builder = new interview.test.ensek.Infrastructure.Services.MeterReadingsBatchBuilder(accountsRepositoryMock.Object, meterReadingsRepositoryMock.Object);

        accountsRepositoryMock.Setup(s => s.GetAccountAsync(It.IsAny<AccountId>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult<Account?>(BuilderTestsHelper.ValidAccount));
        accountsRepositoryMock.Setup(s => s.DoesTheAccountExistAsync(It.IsAny<AccountId>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult<bool>(true));
        meterReadingsRepositoryMock.Setup(s => s.IsMeterReadingUniqueAsync(It.IsAny<AccountId>(), It.IsAny<MeterReadingDateTime>(), It.IsAny<MeterReadValue>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult<bool>(true));

        var builderResult = await builder.TryAddAsync(BuilderTestsHelper.ValidMeterReading, CancellationToken.None);

        builderResult.Should_Report_Success();
    }
}
