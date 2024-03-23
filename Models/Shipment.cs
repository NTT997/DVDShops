using System;
using System.Collections.Generic;

namespace DVDShops.Models;

public partial class Shipment
{
    public int ShipmentId { get; set; }

    public DateOnly ShipmentDate { get; set; }

    public DateOnly DeliveryDate { get; set; }

    public int OrderId { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
