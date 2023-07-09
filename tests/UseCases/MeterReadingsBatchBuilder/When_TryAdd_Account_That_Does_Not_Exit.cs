
using interview.test.ensek.Core.Domain.Loader;
using interview.test.ensek.Infrastructure.SqlServer;
using Microsoft.EntityFrameworkCore;

namespace interview.test.ensek.Tests.UseCases.MeterReadingsBatchBuilder
{
    public sealed class BatchBuilderValidAccountTests
    {
        [Fact]
        public async void When_TryAdd_Account_That_Does_Not_Exit()
        {
            //var dbSetAccountMock = new Mock<DbSet<AccountEntity>>();
            //var dbSetMeterReadingMock = new Mock<DbSet<MeterReadingEntity>>();
            //var dbApplicationDbContextMock = new Mock<ApplicationDbContext>();
            //dbApplicationDbContextMock.Setup(s => s.Set<AccountEntity>()).Returns(dbSetAccountMock.Object);
            //dbApplicationDbContextMock.Setup(s => s.Set<MeterReadingEntity>()).Returns(dbSetMeterReadingMock.Object);

            var account = new Account(new AccountId("2344"), new FirstName("Tommy"), new LastName("Test"));

            var accountsRepositoryMock = new Mock<IAccountsRespository>();
            var meterReadingsRepositoryMock = new Mock<IMeterReadingsRepository>();
            var builder = new interview.test.ensek.Infrastructure.Services.MeterReadingsBatchBuilder(accountsRepositoryMock.Object, meterReadingsRepositoryMock.Object);
            accountsRepositoryMock.Setup(s => s.GetAccountAsync(It.IsAny<AccountId>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult<Account?>(null));
            var builderResult = await builder.TryAddAsync(new MeterReading(account.AccountId, new MeterReadingDateTime("22/04/2019 09:24"), new MeterReadValue("1002")), CancellationToken.None);

            builderResult.Should().NotBeNull();
            builderResult.IsSuccess.Should().BeFalse();
            builderResult.FailureReason.Should().NotBeNull();
            builderResult.FailureReason?.Message.Should().Be(BatchError.AccountIdDoesNotExist(account.AccountId).Message);

        }

    }
}

