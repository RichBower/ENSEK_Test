using interview.test.ensek.Core.Domain.Loader;

namespace interview.test.ensek.Tests.UseCases.MeterReadingsBatchBuilder
{
    public sealed class BatchBuilderDuplicateTests
    {
        [Fact]
        public async void When_TryAdd_Item_Already_In_Batch()
        {
            var accountsRepositoryMock = new Mock<IAccountsRespository>();
            var meterReadingsRepositoryMock = new Mock<IMeterReadingsRepository>();
            var builder = new interview.test.ensek.Infrastructure.Services.MeterReadingsBatchBuilder(accountsRepositoryMock.Object, meterReadingsRepositoryMock.Object);
            accountsRepositoryMock.Setup(s => s.GetAccountAsync(It.IsAny<AccountId>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult<Account?>(BuilderTestsHelper.ValidAccount));

            var firstRecordShouldWork = await builder.TryAddAsync(BuilderTestsHelper.ValidMeterReading, CancellationToken.None);
            var secondShouldFail = await builder.TryAddAsync(BuilderTestsHelper.ValidMeterReading, CancellationToken.None);

            firstRecordShouldWork.Should_Report_Success();
            secondShouldFail.Should_Report_Failure(BatchError.MeterReadingAlreadyExists(BuilderTestsHelper.ValidMeterReading.AccountId, BuilderTestsHelper.ValidMeterReading.MeterReadingDateTime));
        }
    }
}
