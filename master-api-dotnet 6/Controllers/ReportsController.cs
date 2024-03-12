using master_api_dotnet_6.DTO.Reports;
using master_api_dotnet_6.IRepository.IReports;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics.Metrics;
using System.Reflection;

namespace master_api_dotnet_6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReports _IRepository;
        private readonly IReportrdlc _iReportrdlc;
        public ReportsController(IReports IRepository, IReportrdlc iReportrdlc)
        {
            _IRepository = IRepository;
            _iReportrdlc = iReportrdlc;
        }

        [HttpGet]
        [Route("GetUserReport")]
        public async Task<IActionResult> GetUserReport()
        {
            var data = await _IRepository.GetUserReport();
            IList<UserReportDTO> check = data;
            var datas = _iReportrdlc.GetHTML("master-api-dotnet 6.RDLCReports.RDLCDesign.GetUserReport.rdlc", check, "sprMonthlySalesSummaryWithSalesForce", "HTML5");
            return File(datas, "text/html", "report.html");
        }
        //D:\Personal\Project\master-api-dotnet-6\master-api-dotnet 6\RDLCReports\RDLCDesign\GetUserReport.rdlc
        
        
        //[HttpGet]
        //[Route("GetUserReportRDLCXLSX")]
        //public IActionResult GetUserReportRDLCXLSX()
        //{
        //    var dt = _IRepository.GetUserReport();
        //    var datatable = ListToDataTableConverter.ConvertToDataTable<DataRow>(dt);

        //    var data = _iReportrdlc.GetHTML("master-api-dotnet 6.RDLCReports.RDLCDesign.GetUserReport.rdlc", dt, "GetUserReport", "EXCELOPENXML");
        //    return File(data, "application/octet-stream", "All user list.xlsx");


        //}
    }
}
