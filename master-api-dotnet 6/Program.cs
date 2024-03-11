using master_api_dotnet_6.DBContext;
using master_api_dotnet_6.Repository;
using Microsoft.EntityFrameworkCore;
using Hangfire;
using master_api_dotnet_6.Repository.HangfireService.HangfireConfiguration;
using master_api_dotnet_6.Repository.HangfireService;
using master_api_dotnet_6.IRepository.Hangfire;

#pragma warning disable
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Hangfire Service
builder.Services.AddHangfire((sp, config) =>
{
    var connectionString = sp.GetRequiredService<IConfiguration>().GetConnectionString("Development");
    config.UseSqlServerStorage(connectionString);
});

if (builder.Environment.IsProduction())
{
    builder.Services.HangFireConfiguration(builder.Configuration, 1);
}
else if (builder.Environment.IsStaging())
{
    builder.Services.HangFireConfiguration(builder.Configuration, 1);
}
builder.Services.AddHangfireServer();
#endregion

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Development")));
builder.Services.AddHostedService<BackgroupServiceRepo>();

builder.Services.AddScoped<IHangFireRepo, HangFireRepo>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

if (builder.Environment.IsProduction())
{
    app.Configuration(builder.Configuration, 1);

}
else if (builder.Environment.IsStaging())
{
    app.Configuration(builder.Configuration, 1);
}
else
{
    app.Configuration(builder.Configuration, 1);

}
app.Run();
