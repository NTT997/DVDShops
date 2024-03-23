using System;
using System.Collections.Generic;

namespace DVDShops.Models;

public partial class Feedback
{
    public int FbId { get; set; }

    public string FbDetail { get; set; } = null!;

    public string? FbReplay { get; set; }

    public DateOnly FbDate { get; set; }

    public int UsersId { get; set; }

    public int PId { get; set; }

    public virtual ICollection<Album> Albums { get; set; } = new List<Album>();

    public virtual Product PIdNavigation { get; set; } = null!;

    public virtual User Users { get; set; } = null!;
}
