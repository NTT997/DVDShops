using System;
using System.Collections.Generic;

namespace DVDShops.Models;

public partial class Supplier
{
    public int SupplierId { get; set; }

    public string SupplierName { get; set; } = null!;

    public string SupplierEmail { get; set; } = null!;

    public int SupplierPhone { get; set; }

    public string SupplierAddress { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
