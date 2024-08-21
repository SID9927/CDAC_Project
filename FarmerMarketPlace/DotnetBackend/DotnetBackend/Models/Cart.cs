using DotnetBackend.Models;
using Google.Protobuf.Collections;
using System.Collections.Generic;

namespace DotnetBackend.Models
{
    public class Cart
    {
        public List<CartItem> Items { get; set; }
        //class System.Collections.Generic.List<T>
        //Represents a strongly typed list of objects that can be accessed by index.
        //Provides methods to search, sort, and manipulate lists.
        //T is Cartitem
        public double? GrandTotal { get; set; }

        public double? CalculateTotal(List<CartItem> items)
        {
            foreach (var item in items)
            {
                GrandTotal += item.Amount;
            }
            return GrandTotal;
        }
    }
}