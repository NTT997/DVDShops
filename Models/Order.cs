using System;
using System.Collections.Generic;

namespace DVDShops.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int UsersId { get; set; }

    public DateOnly OrderDate { get; set; }

    public bool OrderStatus { get; set; }

    public int CartId { get; set; }

    public int InvoiceId { get; set; }

    public virtual Cart Cart { get; set; } = null!;

    public virtual Invoice Invoice { get; set; } = null!;

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual User Users { get; set; } = null!;
}
