using Hangfire;

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
                connection = Environment.GetEnvironmentVariable("ConnectionString");
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


            //services.AddSingleton<IAsyncTaskManager, AsyncTaskManager>();


            #endregion

        }
        public static void Configuration(this IApplicationBuilder app, IConfiguration Configuration, int serverEnvironment)
        {
            #region Hangfire : Task Schedular

        //    app.UseHangfireDashboard("/crm/tasks", new DashboardOptions
        //    {
        //        Authorization = new[] { new HangfireCustomBasicAuthenticationFilter{User="admin",Pass="admin" }
        //}
        //    });

            app.UseHangfireServer();


            //RecurringJob.AddOrUpdate("LeadInfoEmailSend",
            //     () => (new AsyncTaskManager()).LeadInfoEmailSend(StaticServerEnvironment),
            //                 "0 8 * * *");


            #endregion
        }
    }
}
