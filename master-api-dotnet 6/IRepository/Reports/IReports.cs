using master_api_dotnet_6.DTO.Reports;

namespace master_api_dotnet_6.IRepository.IReports
{
    public interface IReports
    {
        public Task<List<UserReportDTO>> GetUserReport();
    }
}
