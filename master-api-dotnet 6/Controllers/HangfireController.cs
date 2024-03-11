using Hangfire;
using master_api_dotnet_6.DBContext;
using master_api_dotnet_6.IRepository.Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace master_api_dotnet_6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangfireController : ControllerBase
    {
        private readonly IHangFireRepo _repo;
        public HangfireController(IHangFireRepo repo)
        {
            _repo = repo;
        }

        [HttpPost]
        [Route("CreateBackgroundJob")]
        public ActionResult CreateBackgroundJob()
        {
            var data = _repo.CreateBackgroundJob();
            return Ok();
        }

        [HttpPost]
        [Route("CreateScheduledJob")]
        public ActionResult CreateScheduledJob()
        {
            var data = _repo.CreateScheduledJob();
            return Ok(data);
        }

        [HttpPost]
        [Route("CreateContinuationJob")]
        public ActionResult CreateContinuationJob()
        {
            var data = _repo.CreateContinuationJob();
            return Ok(data);
        }

        [HttpPost]
        [Route("CreateRecurring")]
        public ActionResult CreateRecurring()
        {
            var data = _repo.CreateRecurring();
            return Ok(data);
        }

    }
}
