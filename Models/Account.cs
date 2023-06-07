using System;
using System.Collections.Generic;

namespace WebVPP.Models;

public partial class Account
{
    public int AccountId { get; set; }

    public string? FullName { get; set; }

    public string? PassWord { get; set; }

    public int? RoleId { get; set; }

    public virtual Role? Role { get; set; }
}
