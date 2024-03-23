using System;
using System.Collections.Generic;

namespace DVDShops.Models;

public partial class Advertisement
{
    public int AdsId { get; set; }

    public string AdsName { get; set; } = null!;

    public int AdsImage { get; set; }

    public string? AdsDetails { get; set; }
}
