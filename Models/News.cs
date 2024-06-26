﻿using System;
using System.Collections.Generic;

namespace DVDShops.Models;

public partial class News
{
    public int NewsId { get; set; }

    public string NewsTitle { get; set; } = null!;

    public string NewsImage { get; set; } = null!;

    public string NewsContent { get; set; } = null!;

    public string? NewsSource { get; set; }

    public DateOnly PublishDate { get; set; }
}
