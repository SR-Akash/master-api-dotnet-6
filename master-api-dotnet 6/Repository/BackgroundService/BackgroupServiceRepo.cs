using master_api_dotnet_6.DBContext;

namespace master_api_dotnet_6.Repository
{
    public class BackgroupServiceRepo : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        public BackgroupServiceRepo(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }



        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
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
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken); // Delay for 30 seconds


            }
        }
    }
}
