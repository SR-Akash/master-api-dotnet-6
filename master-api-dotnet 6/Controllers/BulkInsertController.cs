using master_api_dotnet_6.DTO.CommonDTO;
using master_api_dotnet_6.IRepository;
using master_api_dotnet_6.IRepository.IReports;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Xml.Linq;

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

        [HttpPost("UploadBulkExcel")]
        public async Task<IActionResult> UploadBulkExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            var dataList = new List<BulkInsertDTO>();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0]; // Assuming data is in the first sheet
                    int rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++) // Starting from row 2 to skip header
                    {
                        var data = new BulkInsertDTO
                        {
                            Name = (worksheet.Cells[row, 1].Text),          // Assuming Name is in column 1
                            Age = int.Parse(worksheet.Cells[row, 2].Text),                  // Assuming Age is in column 2
                            Class = int.Parse(worksheet.Cells[row, 3].Text) // Assuming Class is in column 3
                        };
                        dataList.Add(data);
                    }
                }
            }

            var msg = _IRepository.BulkInsert(dataList);
            return Ok(msg);
        }
    }
}
