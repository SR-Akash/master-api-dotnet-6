using master_api_dotnet_6.DBContext;
using master_api_dotnet_6.Repository;
using Microsoft.EntityFrameworkCore;
using Hangfire;
using master_api_dotnet_6.Repository.HangfireService.HangfireConfiguration;
using master_api_dotnet_6.Repository.HangfireService;
using master_api_dotnet_6.IRepository.Hangfire;
using master_api_dotnet_6.IRepository.IReports;
using master_api_dotnet_6.Repository.Reports;
using DinkToPdf.Contracts;
using DinkToPdf;
using master_api_dotnet_6;
using master_api_dotnet_6.Helper;
using master_api_dotnet_6.IRepository;

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


#region Services Register List Start
builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
builder.Services.AddScoped<IHangFireRepo, HangFireRepo>();
builder.Services.AddScoped<IReports, Reports>();
builder.Services.AddScoped<IConsolePractice, ConsolePractice>();
builder.Services.AddTransient<IReportrdlc, Reportrdlc>();
#endregion

#region PDF DI
var OsPlatform = System.Runtime.InteropServices.RuntimeInformation.OSDescription.ToLower();
var context = new CustomAssemblyLoadContext();

if (OsPlatform.Contains("windows"))
{
    context.LoadUnmanagedLibrary(Path.Combine(Directory.GetCurrentDirectory(), "libwkhtmltox.dll"));
}
else
{
    // This "libwkhtmltox.so" for Linux server;
    context.LoadUnmanagedLibrary(Path.Combine(Directory.GetCurrentDirectory(), "libwkhtmltox.so"));
}
#endregion
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
