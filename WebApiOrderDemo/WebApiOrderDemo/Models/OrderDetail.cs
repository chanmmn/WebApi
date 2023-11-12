using System;
using System.Collections.Generic;

namespace WebApiOrderDemo.Models;

public partial class OrderDetail
{
    public Guid OrderDetailId { get; set; }

    public Guid? OrderId { get; set; }

    public Guid? ProductId { get; set; }

    public int? Quantity { get; set; }

    public virtual Order? Order { get; set; }
}
