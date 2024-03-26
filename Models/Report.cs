using System;
using System.Collections.Generic;

namespace DVDShops.Models;

public partial class Report
{
    public int ReportId { get; set; }

    public string ReportContent { get; set; } = null!;

    public int? UsersId { get; set; }

    public virtual User? Users { get; set; }
}
