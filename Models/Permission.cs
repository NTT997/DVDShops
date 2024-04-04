using System;
using System.Collections.Generic;

namespace DVDShops.Models;

public partial class Permission
{
    public int PermissionId { get; set; }

    public string PermissionType { get; set; } = null!;

    public virtual ICollection<AdminsPermission> AdminsPermissions { get; set; } = new List<AdminsPermission>();
}
