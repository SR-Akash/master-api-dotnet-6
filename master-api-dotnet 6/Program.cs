using master_api_dotnet_6.DBContext;
using master_api_dotnet_6.Repository;
using Microsoft.EntityFrameworkCore;
using Hangfire;
using master_api_dotnet_6.Repository.HangfireService.HangfireConfiguration;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Development")));
builder.Services.AddHostedService<BackgroupServiceRepo>();


if (builder.Environment.IsProduction())
{
    builder.Services.HangFireConfiguration(builder.Configuration, 1);
}
else if (builder.Environment.IsStaging())
{
    // builder.Services.HangFireConfiguration(builder.Configuration, 2);

}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
