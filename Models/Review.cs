using System;
using System.Collections.Generic;

namespace DVDShops.Models;

public partial class Review
{
    public int ReviewId { get; set; }

    public string ReviewTitle { get; set; } = null!;

    public string ReviewContent { get; set; } = null!;

    public int UsersId { get; set; }

    public virtual ICollection<Album> Albums { get; set; } = new List<Album>();

    public virtual User Users { get; set; } = null!;
}
