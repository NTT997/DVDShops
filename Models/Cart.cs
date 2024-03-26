using System;
using System.Collections.Generic;

namespace DVDShops.Models;

public partial class Cart
{
    public int CartId { get; set; }

    public int UsersId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public double Amount { get; set; }

    public int PromotionId { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Promotion Promotion { get; set; } = null!;
}
