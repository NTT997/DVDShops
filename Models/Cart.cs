using System;
using System.Collections.Generic;

namespace DVDShops.Models;

public partial class Cart
{
    public int CartId { get; set; }

    public int UsersId { get; set; }

    public int Quantity { get; set; }

    public double Price { get; set; }

    public int ProductId { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual User Users { get; set; } = null!;
}
