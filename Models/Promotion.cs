using System;
using System.Collections.Generic;

namespace DVDShops.Models;

public partial class Promotion
{
    public int PromotionId { get; set; }

    public string PromotionName { get; set; } = null!;

    public string? PromotionBanner { get; set; }

    public int PromotionPercent { get; set; }

    public DateOnly PromotionStartDate { get; set; }

    public DateOnly PromotionExpireDate { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
