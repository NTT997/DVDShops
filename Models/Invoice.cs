using System;
using System.Collections.Generic;

namespace DVDShops.Models;

public partial class Invoice
{
    public int InvId { get; set; }

    public int Total { get; set; }

    public int OrderId { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
