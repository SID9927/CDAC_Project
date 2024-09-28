namespace DotnetBackend.DTO;

public partial class OrderDTO
{
    public int OrderId { get; set; }

    public DateOnly? DeliveryDate { get; set; }

    public ulong? DeliveryStatus { get; set; }

    public ulong? PaymentStatus { get; set; }

    public DateOnly? PlaceOrderDate { get; set; }

    public int UserId { get; set; }


}
