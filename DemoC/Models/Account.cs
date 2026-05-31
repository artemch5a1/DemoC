using DemoC.Enums;
using System;
using System.Collections.Generic;

namespace DemoC.Models;

public partial class Account
{
    public int Accountid { get; set; }

    public Role Role { get; set; }

    public string Surname { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Patronymic { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
