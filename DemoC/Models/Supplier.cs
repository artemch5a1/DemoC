using System;
using System.Collections.Generic;

namespace DemoC.Models;

public partial class Supplier
{
    public int Supplierid { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
