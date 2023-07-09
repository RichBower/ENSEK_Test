using interview.test.ensek.Core.Domain.Loader;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace interview.test.ensek.Infrastructure.SqlServer;

public sealed class ApplicationDbContextInitialiser
{
    public readonly List<AccountEntity> _accountSeedData = new List<AccountEntity>
    {
        new AccountEntity { AccountEntityID= 2344, FirstName = "Tommy", LastName="Test" },
        new AccountEntity { AccountEntityID= 2233, FirstName = "Barry", LastName="Test"},
        new AccountEntity { AccountEntityID= 8766, FirstName = "Sally", LastName="Test"},
        new AccountEntity { AccountEntityID= 2345, FirstName = "Jerry", LastName="Test"},
        new AccountEntity { AccountEntityID= 2346, FirstName = "Ollie", LastName="Test"},
        new AccountEntity { AccountEntityID= 2347, FirstName = "Tara", LastName="Test"},
        new AccountEntity { AccountEntityID= 2348, FirstName = "Tammy", LastName="Test"},
        new AccountEntity { AccountEntityID= 2349, FirstName = "Simon", LastName="Test"},
        new AccountEntity { AccountEntityID= 2350, FirstName = "Colin", LastName="Test"},
        new AccountEntity { AccountEntityID= 2351, FirstName = "Gladys", LastName="Test"},
        new AccountEntity { AccountEntityID= 2352, FirstName = "Greg", LastName="Test"},
        new AccountEntity { AccountEntityID= 2353, FirstName = "Tony", LastName="Test"},
        new AccountEntity { AccountEntityID= 2355, FirstName = "Arthur", LastName="Test"},
        new AccountEntity { AccountEntityID= 2356, FirstName = "Craig", LastName="Test"},
        new AccountEntity { AccountEntityID= 6776, FirstName = "Laura", LastName="Test"},
        new AccountEntity { AccountEntityID= 4534, FirstName = "JOSH", LastName="TEST"},
        new AccountEntity { AccountEntityID= 1234, FirstName = "Freya", LastName="Test"},
        new AccountEntity { AccountEntityID= 1239, FirstName = "Noddy", LastName="Test"},
        new AccountEntity { AccountEntityID= 1240, FirstName = "Archie", LastName="Test"},
        new AccountEntity { AccountEntityID= 1241, FirstName = "Lara", LastName="Test"},
        new AccountEntity { AccountEntityID= 1242, FirstName = "Tim", LastName="Test"},
        new AccountEntity { AccountEntityID= 1243, FirstName = "Graham", LastName="Test"},
        new AccountEntity { AccountEntityID= 1244, FirstName = "Tony", LastName="Test"},
        new AccountEntity { AccountEntityID= 1245, FirstName = "Neville", LastName="Test"},
        new AccountEntity { AccountEntityID= 1246, FirstName = "Jo", LastName="Test"},
        new AccountEntity { AccountEntityID= 1247, FirstName = "Jim", LastName="Test"},
        new AccountEntity { AccountEntityID= 1248, FirstName = "Pam", LastName="Test"},
    };

    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;

    public ApplicationDbContextInitialiser(
        ILogger<ApplicationDbContextInitialiser> logger,
        ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            await _context.Database.EnsureCreatedAsync();
            await _context.Database.MigrateAsync();
            
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        if (_context.Accounts.Any())
        {
            return;   // DB has been seeded
        }

        _context.Accounts.AddRange(_accountSeedData);
        await _context.SaveChangesAsync();
    }
}

