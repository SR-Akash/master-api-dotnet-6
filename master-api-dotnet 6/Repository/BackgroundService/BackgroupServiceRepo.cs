using master_api_dotnet_6.DBContext;
#pragma warning disable
namespace master_api_dotnet_6.Repository
{
    public class BackgroupServiceRepo : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        public BackgroupServiceRepo(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }


        #region this is one way
        //protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        //{
        //    while (!stoppingToken.IsCancellationRequested)
        //    {
        //        using (var scope = _serviceProvider.CreateScope())
        //        {
        //            var _context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        //            var user = new TblUser
        //            {
        //                ConfirmPassword = "Akash25@",
        //                Email = "akash@ibos.io",
        //                IsActive = true,
        //                IsDelete = false,
        //                LastActionDatetime = DateTime.UtcNow,
        //                Mobile = "01634860323",
        //                Password = "Akash25@",
        //                PreviousPassword = "Akash25@",
        //                UserName = "Akash"
        //            };

        //            _context.TblUsers.Add(user);
        //            await _context.SaveChangesAsync();
        //        };
        //        await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken); // Delay for 30 seconds


        //    }
        //}
        #endregion

        #region this is another way background service

        private readonly PeriodicTimer _timer = new(TimeSpan.FromDays(1));
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (await _timer.WaitForNextTickAsync(stoppingToken)
                && !stoppingToken.IsCancellationRequested)
            {
                DoWorkAsync();
            }
        }


        private async void DoWorkAsync()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                var user = new TblUser
                {
                    ConfirmPassword = "Akash25@",
                    Email = "akash@ibos.io",
                    IsActive = true,
                    IsDelete = false,
                    LastActionDatetime = DateTime.UtcNow,
                    Mobile = "01634860323",
                    Password = "Akash25@",
                    PreviousPassword = "Akash25@",
                    UserName = "Akash"
                };

                _context.TblUsers.Add(user);
                await _context.SaveChangesAsync();
            };
        }



        #endregion
    }
}
