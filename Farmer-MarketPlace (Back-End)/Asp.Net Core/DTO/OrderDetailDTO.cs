namespace DotnetBackend.DTO;

public partial class OrderDetailDTO
{
    public int Id { get; set; }

    public double? Amount { get; set; }

    public string OrderItem { get; set; } = null!;

    public int Quantity { get; set; }

    public int? FarmerId { get; set; }

    public int OrderId { get; set; }


}
