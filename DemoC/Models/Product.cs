using System;
using System.Collections.Generic;

namespace DemoC.Models;

public partial class Product
{
    public int Productid { get; set; }

    public string Articul { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string CategoryAndTitle => $"{Category.Title} | {Title}";

    public string UnitOfMeasurement { get; set; } = null!;

    public decimal Price { get; set; }

    public int Supplierid { get; set; }

    public int Manufacturerid { get; set; }

    public int Categoryid { get; set; }

    public int Sale { get; set; }

    public int Countin { get; set; }

    public string Description { get; set; } = null!;

    public string? Image { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual Manufacturer Manufacturer { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual Supplier Supplier { get; set; } = null!;
}
