using System;
using System.Collections.Generic;

namespace DemoC.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public DateOnly DateOrder { get; set; }

    public DateOnly DateDelivery { get; set; }

    public int PickupPointId { get; set; }

    public int AccountId { get; set; }

    public int CodeTake { get; set; }

    public int StatusId { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual PickupPoint PickupPoint { get; set; } = null!;

    public virtual OrderStatus Status { get; set; } = null!;
}
