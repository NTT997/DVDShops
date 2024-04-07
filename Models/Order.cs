using System;
using System.Collections.Generic;

namespace DVDShops.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int UsersId { get; set; }

    public DateOnly OrderDate { get; set; }

    public bool OrderStatus { get; set; }

    public double TotalAmount { get; set; }

    public string ShipAddress { get; set; } = null!;

    public string? Note { get; set; }

    public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual User Users { get; set; } = null!;
}
