using System;
using System.Collections.Generic;

namespace DemoC.Models;

public partial class PickupPoint
{
    public int PickupPointid { get; set; }

    public int PostIndex { get; set; }

    public string City { get; set; } = null!;

    public string Street { get; set; } = null!;

    public int? HomeNum { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
