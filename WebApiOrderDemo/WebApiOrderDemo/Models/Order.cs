using System;
using System.Collections.Generic;

namespace WebApiOrderDemo.Models;

public partial class Order
{
    public Guid OrderId { get; set; }

    public DateTime? OrderDate { get; set; }

    public Guid? CustomerId { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
