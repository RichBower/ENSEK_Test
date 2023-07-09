using interview.test.ensek.Core.Domain.Common;
using interview.test.ensek.Core.Domain.Loader;
using Microsoft.EntityFrameworkCore;

namespace interview.test.ensek.Infrastructure.SqlServer;

public sealed class AccountsRepository : IAccountsRespository
{
    public AccountsRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    private ApplicationDbContext _context { get; init; }

    public async Task<Account?> GetAccountAsync(AccountId accountId, CancellationToken cancellation)
    {
        var matching = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountEntityID == accountId.Value);

        if (matching is null)
        {
            return null;
        }
        return new Account(new AccountId(matching.AccountEntityID.ToString()), new FirstName(matching.FirstName), new LastName(matching.LastName));


    }
}
