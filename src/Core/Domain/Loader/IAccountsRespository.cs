using interview.test.ensek.Core.Domain.Common;

namespace interview.test.ensek.Core.Domain.Loader
{
    public interface IAccountsRespository
    {
        Task<Account?> GetAccountAsync(AccountId accountId, CancellationToken cancellation);

    }
}
