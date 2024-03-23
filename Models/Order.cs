using System;
using System.Collections.Generic;

namespace DVDShops.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int UsersId { get; set; }

    public DateOnly OrderDate { get; set; }

    public int PId { get; set; }

    public int OrderQuantity { get; set; }

    public int InvId { get; set; }

    public int ShipmentId { get; set; }

    public virtual Invoice Inv { get; set; } = null!;

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual Product PIdNavigation { get; set; } = null!;

    public virtual Shipment Shipment { get; set; } = null!;

    public virtual ICollection<Shipment> Shipments { get; set; } = new List<Shipment>();

    public virtual User Users { get; set; } = null!;
}
