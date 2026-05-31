using System;
using System.Collections.Generic;

namespace DemoC.Models;

public partial class Category
{
    public int Categoryid { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
