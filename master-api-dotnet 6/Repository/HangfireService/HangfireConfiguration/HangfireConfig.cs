using Hangfire;
using HangfireBasicAuthenticationFilter;
#pragma warning disable
namespace master_api_dotnet_6.Repository.HangfireService.HangfireConfiguration
{
    public static class HangfireConfig
    {
        public static int StaticServerEnvironment = 0;

        public static void HangFireConfiguration(this IServiceCollection services, IConfiguration Configuration, int serverEnvironment)
        {
            #region Hangfire : Task Schedular
            string connection;
            StaticServerEnvironment = serverEnvironment;
            if (serverEnvironment == 1)
            {
                connection = Environment.GetEnvironmentVariable("ConnectionStrings");
            }
            else if (serverEnvironment == 2)
            {
                connection = Configuration.GetConnectionString("Staging");
            }
            else
            {
                connection = Configuration.GetConnectionString("Development");

            }

            services.AddHangfire(option =>
            {
                option
                .UseSimpleAssemblyNameTypeSerializer()
                .UseDefaultTypeSerializer()
                .UseSqlServerStorage(connection);
            });

            services.AddHangfireServer();

            #endregion

        }

        public static void Configuration(this IApplicationBuilder app, IConfiguration Configuration, int serverEnvironment)
        {
            #region Hangfire : Task Schedular

            app.UseHangfireDashboard("/dashboard", new DashboardOptions
            {
                Authorization = new[] { new HangfireCustomBasicAuthenticationFilter{User="admin",Pass="admin" }
        }
            });

            app.UseHangfireServer();


            RecurringJob.AddOrUpdate("UserCreate",
                 () => (new AllHangfireSchedules()).UserCreate(StaticServerEnvironment), Cron.Minutely());


            #endregion
        }

    }
}
