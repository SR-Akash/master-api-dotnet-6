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
        public async Task<IActionResult> GetUserReport(string reportTypeName)
        {
            var data = await _IRepository.GetUserReport();
            IList<UserReportDTO> check = data;

            if (reportTypeName.ToLower().Contains("html"))
            {
                var datas = _iReportrdlc.GetHTML("master_api_dotnet_6.RDLCReports.RDLCDesign.GetUserReport.rdlc", check, "GetUserReport", "HTML5");
                return File(datas, "text/html", "GetUserReport.html");
            }
            else if(reportTypeName.ToLower().Contains("excel"))
            {
                var datas = _iReportrdlc.GetXLSX("master_api_dotnet_6.RDLCReports.RDLCDesign.GetUserReport.rdlc", check, "GetUserReport", "EXCELOPENXML");
                return File(datas, "application/octet-stream", "GetUserReport.xlsx");
            }
            else
            {
                var datas = _iReportrdlc.GetPDF("master_api_dotnet_6.RDLCReports.RDLCDesign.GetUserReport.rdlc", check, "GetUserReport", "PDF", 1);
                return File(datas, "application/pdf", "Ledger  Report Details.PDF");
            }

        }
    }
}
