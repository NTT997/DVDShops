using System;
using System.Collections.Generic;

namespace DVDShops.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public float? ProductPrice { get; set; }

    public int ProductQuantity { get; set; }

    public int SoldUnit { get; set; }

    public int? ProductRate { get; set; }

    public DateOnly CreatedAt { get; set; }

    public int SupplierId { get; set; }

    public int? AlbumId { get; set; }

    public int PromotionId { get; set; }

    public virtual Album? Album { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();

    public virtual Promotion Promotion { get; set; } = null!;

    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();

    public virtual Supplier Supplier { get; set; } = null!;
}
