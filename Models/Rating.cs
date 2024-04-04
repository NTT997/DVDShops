using System;
using System.Collections.Generic;

namespace DVDShops.Models;

public partial class Rating
{
    public int RatingId { get; set; }

    public int? ProductId { get; set; }

    public int? UsersId { get; set; }

    public int? Rating1 { get; set; }

    public string? Comment { get; set; }

    public virtual Product? Product { get; set; }

    public virtual User? Users { get; set; }
}
