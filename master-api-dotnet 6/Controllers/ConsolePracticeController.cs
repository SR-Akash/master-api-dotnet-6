using master_api_dotnet_6.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace master_api_dotnet_6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsolePracticeController : ControllerBase
    {
        private readonly IConsolePractice _Irepo;
        public ConsolePracticeController(IConsolePractice Irepo)
        {
            _Irepo = Irepo;
        }

        [HttpGet]
        [Route("CollectionsInCSharp")]
        public async Task<IActionResult> CollectionsInCSharp()
        {
            var data = await _Irepo.CollectionsInCSharp();
            return Ok(data);

        }
    }
}
