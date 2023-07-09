using interview.test.ensek.Core.Domain.Feed;
using interview.test.ensek.Core.Domain.Loader;
using interview.test.ensek.Core.Service.Abstractions;
using interview.test.ensek.Core.Services;
using interview.test.ensek.Infrastructure.Services;
using interview.test.ensek.Infrastructure.SqlServer;
using Microsoft.EntityFrameworkCore;

namespace interview.test.ensek.Host.WebApi.Setup;

public static class SetupServices
{
    public static void Configure(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("EnsekMeterReadingsDb"));
        }
        else
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("EnsekMeterReadingsContext")));
        }

        services.AddScoped<ApplicationDbContextInitialiser>();

        services.AddTransient<IAccountsRespository, AccountsRepository>();
        services.AddTransient<IMeterReadingsRepository, MeterReadingsRespository>();
        services.AddTransient<IAccountsFeedParserService, CsvReaderAccountsFeedService>();
        services.AddTransient<IMeterReadingsFeedParserService, CsvReaderMeterReadingsFeedService>();
        services.AddTransient<IMeterReadingsImporter, MeterReadingsImporter>();
        services.AddTransient<IMeterReadingBatchBuilder, MeterReadingsBatchBuilder>();
    }
}
