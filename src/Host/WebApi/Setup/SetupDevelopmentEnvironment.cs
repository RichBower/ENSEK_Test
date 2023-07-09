using interview.test.ensek.Infrastructure.SqlServer;

namespace interview.test.ensek.Host.WebApi.Setup;

public class SetupDevelopmentEnvironment
{
    public async static void InitialiseDevelopmentEnvironment(IServiceProvider services)
    {
        // Initialise and seed database
        using (var scope = services.CreateScope())
        {
            var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
            await initialiser.InitialiseAsync();
            await initialiser.SeedAsync();
        }
    }
}
