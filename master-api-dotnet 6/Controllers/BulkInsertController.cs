using master_api_dotnet_6.IRepository;
using master_api_dotnet_6.IRepository.IReports;
using Microsoft.AspNetCore.Mvc;

namespace master_api_dotnet_6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BulkInsertController : ControllerBase
    {
        private readonly IBulkInsert _IRepository;
        public BulkInsertController(IBulkInsert IRepository)
        {
            _IRepository = IRepository;
        }


    }
}
