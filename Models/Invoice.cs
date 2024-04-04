using System;
using System.Collections.Generic;

namespace DVDShops.Models;

public partial class Invoice
{
    public int InvoiceId { get; set; }

    public int? OrderId { get; set; }

    public DateOnly InvoiceDate { get; set; }

    public double TotalAmount { get; set; }

    public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();

    public virtual Order? Order { get; set; }
}
