using interview.test.ensek.Host.WebApi.Setup;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
SetupServices.Configure(builder.Services, builder.Configuration, builder.Environment);

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    SetupDevelopmentEnvironment.InitialiseDevelopmentEnvironment(app.Services);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
