using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DotnetBackend.Models;

public partial class StockDetail
{
    public int ProductId { get; set; }

    public string? ProductImage { get; set; }

    public double? PricePerUnit { get; set; }

    public int Quantity { get; set; }

    public string StockItem { get; set; } = null!;

    public int? CategoryId { get; set; }

    public int? FarmerId { get; set; }

    //[JsonIgnore]
    public virtual Category? Category { get; set; }

    //[JsonIgnore]
    public virtual Farmer? Farmer { get; set; }
}
