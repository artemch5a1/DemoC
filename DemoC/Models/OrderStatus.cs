using System;
using System.Collections.Generic;

namespace DemoC.Models;

public partial class OrderStatus
{
    public int OrderStatusId { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
