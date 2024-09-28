namespace DotnetBackend.Models;

public partial class CategoryStockDetail
{
    public int CategoryCategoryId { get; set; }

    public int StockDetailsProductId { get; set; }

    public virtual Category CategoryCategory { get; set; } = null!;

    public virtual StockDetail StockDetailsProduct { get; set; } = null!;
}
