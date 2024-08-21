using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DotnetBackend.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public DateOnly? DeliveryDate { get; set; }

    public ulong? DeliveryStatus { get; set; }

    public ulong? PaymentStatus { get; set; }

    public DateOnly? PlaceOrderDate { get; set; }

    public int UserId { get; set; }

    [JsonIgnore]
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    [JsonIgnore]
    public virtual User User { get; set; } = null!;
}
