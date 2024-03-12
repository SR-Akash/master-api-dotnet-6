using master_api_dotnet_6.DBContext;
using master_api_dotnet_6.DTO.Reports;
using master_api_dotnet_6.IRepository.IReports;
using Microsoft.EntityFrameworkCore;

namespace master_api_dotnet_6.Repository.Reports
{
    public class Reports : IReports
    {
        private readonly AppDbContext _context;
        public Reports(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserReportDTO>> GetUserReport()
        {
            try
            {
                var data = await (from u in _context.TblUsers
                                  where u.IsActive == true
                                  select new UserReportDTO
                                  {
                                      Password = u.Password,
                                      UserName = u.UserName,
                                  }).ToListAsync();
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
