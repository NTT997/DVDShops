using System;
using System.Collections.Generic;

namespace DVDShops.Models;

public partial class AdminsPermission
{
    public int Id { get; set; }

    public int? UsersId { get; set; }

    public int? PermissionId { get; set; }

    public virtual Permission? Permission { get; set; }

    public virtual User? Users { get; set; }
}
