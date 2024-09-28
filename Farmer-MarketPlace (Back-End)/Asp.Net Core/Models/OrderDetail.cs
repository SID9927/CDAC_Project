using Newtonsoft.Json;

namespace DotnetBackend.Models;

public partial class OrderDetail
{
    public int Id { get; set; }

    public Double? Amount { get; set; }

    public string OrderItem { get; set; } = null!;

    public int Quantity { get; set; }

    public int? FarmerId { get; set; }

    public int OrderId { get; set; }

    [JsonIgnore]
    public virtual Farmer? Farmer { get; set; }

    [JsonIgnore]
    public virtual Order Order { get; set; } = null!;
}
