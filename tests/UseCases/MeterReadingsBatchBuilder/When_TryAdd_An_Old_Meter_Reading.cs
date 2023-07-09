namespace interview.test.ensek.Tests.UseCases.MeterReadingsBatchBuilder;

public sealed class BatchBuilderLatestDataTests
{
    [Fact]
    public async void When_TryAdd_An_Old_Meter_Reading()
    {
        var accountsRepositoryMock = new Mock<IAccountsRespository>();
        var meterReadingsRepositoryMock = new Mock<IMeterReadingsRepository>();
        var builder = new interview.test.ensek.Infrastructure.Services.MeterReadingsBatchBuilder(accountsRepositoryMock.Object, meterReadingsRepositoryMock.Object);
        
        accountsRepositoryMock.Setup(s => s.GetAccountAsync(It.IsAny<AccountId>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult<Account?>(BuilderTestsHelper.ValidAccount));
        accountsRepositoryMock.Setup(s => s.DoesTheAccountExistAsync(It.IsAny<AccountId>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult<bool>(true));
        meterReadingsRepositoryMock.Setup(s => s.IsMeterReadingUniqueAsync(It.IsAny<AccountId>(), It.IsAny<MeterReadingDateTime>(), It.IsAny<MeterReadValue>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult<bool>(true));



        var firstRecordShouldWork = await builder.TryAddAsync(BuilderTestsHelper.ValidMeterReading, CancellationToken.None);
        var secondShouldFail = await builder.TryAddAsync(BuilderTestsHelper.OldMeterReading, CancellationToken.None);

        firstRecordShouldWork.Should_Report_Success();
        secondShouldFail.Should_Report_Failure(BatchError.MeterReadingIsOld(BuilderTestsHelper.OldMeterReading.AccountId, BuilderTestsHelper.OldMeterReading.MeterReadingDateTime));
    }
}
