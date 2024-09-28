namespace DotnetBackend.DTO;

public partial class StockDetailDTO
{
    public int ProductId { get; set; }

    public string? ProductImage { get; set; }

    public double? PricePerUnit { get; set; }

    public int Quantity { get; set; }

    public string StockItem { get; set; } = null!;

    public int? CategoryId { get; set; }

    public int? FarmerId { get; set; }

}
