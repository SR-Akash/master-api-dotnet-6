using System;
using System.Collections.Generic;

namespace master_api_dotnet_6.Models;

public partial class TblUser
{
    public long UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string Mobile { get; set; } = null!;

    public string? Email { get; set; }

    public string Password { get; set; } = null!;

    public string ConfirmPassword { get; set; } = null!;

    public string? PreviousPassword { get; set; }

    public bool? IsActive { get; set; }

    public bool IsDelete { get; set; }

    public DateTime LastActionDatetime { get; set; }
}
