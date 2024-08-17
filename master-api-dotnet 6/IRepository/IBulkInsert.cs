using master_api_dotnet_6.DTO.CommonDTO;

namespace master_api_dotnet_6.IRepository
{
    public interface IBulkInsert
    {
        Task<string> BulkInsert(List<BulkInsertDTO> obj);
    }

}
