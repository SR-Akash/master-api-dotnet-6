using FastMember;
using master_api_dotnet_6.DTO.CommonDTO;
using master_api_dotnet_6.IRepository;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;
using System.Diagnostics;
using System.Reflection;
#pragma warning disable

namespace master_api_dotnet_6.Repository
{
    public class BulkInsertService : IBulkInsert
    {
        private readonly string ConnectionString;
        public BulkInsertService(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("Development");
        }

        public async Task<string> BulkInsert(List<BulkInsertDTO> obj)
        {
            var stopWatch = new Stopwatch();
            try
            {
                stopWatch.Start();
                using var bulkCopy = new SqlBulkCopy(ConnectionString)
                {
                    DestinationTableName = "[CRUD_DB].[dbo].[tblStudent]",
                    BatchSize = 10000,
                    BulkCopyTimeout = 600
                };

                bulkCopy.ColumnMappings.Add(nameof(BulkInsertDTO.Name), "Name");
                bulkCopy.ColumnMappings.Add(nameof(BulkInsertDTO.Age), "Age");
                bulkCopy.ColumnMappings.Add(nameof(BulkInsertDTO.Class), "Class");

                var dataTable = new DataTable();
                dataTable.Columns.Add("Name", typeof(string));
                dataTable.Columns.Add("Age", typeof(int));
                dataTable.Columns.Add("Class", typeof(int));

                foreach (var student in obj)
                {
                    dataTable.Rows.Add(student.Name, student.Age, student.Class);
                }

                bulkCopy.WriteToServer(dataTable);
                stopWatch.Stop();
                var elapsedTime = stopWatch.Elapsed;

                return "Success, Time required:" + elapsedTime.TotalSeconds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
