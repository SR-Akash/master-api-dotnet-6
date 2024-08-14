using master_api_dotnet_6.DBContext;
using master_api_dotnet_6.Models;
using Microsoft.EntityFrameworkCore;


namespace master_api_dotnet_6.Repository.HangfireService
{
    public class AllHangfireSchedules
    {
        public AllHangfireSchedules()
        {

        }

        public async Task UserCreate(int serverEnvironment)
        {
            var connection = "";
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile(
                    $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",
                    optional: true)
                .Build();
            if (serverEnvironment == 1)
            {
                connection = Environment.GetEnvironmentVariable("ConnectionString");
            }
            else if (serverEnvironment == 2)
            {
                connection = configuration.GetConnectionString("Staging");
            }
            else
            {
                connection = configuration.GetConnectionString("Development");
            }

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(connection);

            var context = new AppDbContext(optionsBuilder.Options);

            long CRMAccountId = 20255;
            long ProductBranchId = 40394;
            long DealWonId = 100;
            var Today = DateTime.Now;


            var user = new TblUser
            {
                ConfirmPassword = "RECURRINGAkash25@",
                Email = "RECURRINGGakash@ibos.io",
                IsActive = true,
                IsDelete = false,
                LastActionDatetime = DateTime.UtcNow,
                Mobile = "01634860323",
                Password = "RECURRINGAkash25@",
                PreviousPassword = "RECURRINGAkash25@",
                UserName = "AkashHRECURRING"
            };

            context.TblUsers.Add(user);
            await context.SaveChangesAsync();

        }
    }
}
