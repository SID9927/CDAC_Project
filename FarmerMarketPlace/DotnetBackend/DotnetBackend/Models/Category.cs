using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DotnetBackend.Models;

public partial class Category
{
    public int CategoryId { get; set; }

    public string? CategoryName { get; set; }

    [JsonIgnore]
    public virtual ICollection<StockDetail> StockDetails { get; set; } = new List<StockDetail>();
    //interface System.Collections.Generic. Collection<T>
    //Defines methods to manipulate generic collections.
    //T is StockDetail
}
