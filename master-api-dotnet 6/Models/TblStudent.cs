using System;
using System.Collections.Generic;

namespace master_api_dotnet_6.Models;

public partial class TblStudent
{
    public long StudentId { get; set; }

    public string? Name { get; set; }

    public long? Age { get; set; }

    public long? Class { get; set; }
}
