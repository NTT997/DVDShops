using System;
using System.Collections.Generic;

namespace DVDShops.Models;

public partial class InvoiceDetail
{
    public int DetailId { get; set; }

    public int? InvoiceId { get; set; }

    public int? OrderId { get; set; }

    public int? ProductId { get; set; }

    public int? Quantity { get; set; }

    public float? PriceAfterPromotion { get; set; }

    public float? TotalAmount { get; set; }

    public virtual Invoice? Invoice { get; set; }

    public virtual Order? Order { get; set; }

    public virtual Product? Product { get; set; }
}
