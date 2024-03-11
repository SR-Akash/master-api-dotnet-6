using Hangfire;
using master_api_dotnet_6.DBContext;
using master_api_dotnet_6.IRepository.Hangfire;

namespace master_api_dotnet_6.Repository.HangfireService
{
    public class HangFireRepo : IHangFireRepo
    {

        private readonly IServiceProvider _serviceProvider;
        public HangFireRepo(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public object CreateBackgroundJob()
        {
            BackgroundJob.Enqueue(() => DoWorkAsynchangfire());
            return "Successfully data inserted";
        }

        public object CreateScheduledJob()
        {
            var scheduleDateTime = DateTime.UtcNow.AddSeconds(5);
            var dateTimeOffset = new DateTimeOffset(scheduleDateTime);

            BackgroundJob.Schedule(() => DoWorkAsynchangfire(), dateTimeOffset);
            return "Successfully data inserted";
        }

        public object CreateContinuationJob()
        {
            var scheduleDateTime = DateTime.UtcNow.AddSeconds(5);
            var dateTimeOffset = new DateTimeOffset(scheduleDateTime);
            var jobId = BackgroundJob.Schedule(() => DoWorkAsynchangfire(), dateTimeOffset);

            var job2Id = BackgroundJob.ContinueJobWith(jobId, () => DoWorkAsynchangfire());
            var job3Id = BackgroundJob.ContinueJobWith(job2Id, () => DoWorkAsynchangfire());
            var job4Id = BackgroundJob.ContinueJobWith(job3Id, () => DoWorkAsynchangfire());

            return "Successfully data inserted";
        }

        public object CreateRecurring()
        {
            RecurringJob.AddOrUpdate("UserInsert", () => DoWorkAsynchangfire(), Cron.MinuteInterval(10));
            return "";
        }

        public async Task RecurringSchedule()
        {
            DoWorkAsynchangfire();
        }

        public async Task DoWorkAsynchangfire()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                var user = new TblUser
                {
                    ConfirmPassword = "HANGAkash25@",
                    Email = "HANGakash@ibos.io",
                    IsActive = true,
                    IsDelete = false,
                    LastActionDatetime = DateTime.UtcNow,
                    Mobile = "01634860323",
                    Password = "HANGAkash25@",
                    PreviousPassword = "HANGAkash25@",
                    UserName = "AkashHANG"
                };

                _context.TblUsers.Add(user);
                await _context.SaveChangesAsync();
            };
        }

    }
}
