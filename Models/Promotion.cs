using System;
using System.Collections.Generic;

namespace DVDShops.Models;

public partial class Promotion
{
    public int PromoId { get; set; }

    public string PromoName { get; set; } = null!;

    public int PromoPercent { get; set; }

    public DateOnly PromoStartDate { get; set; }

    public DateOnly PromoExpireDate { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
