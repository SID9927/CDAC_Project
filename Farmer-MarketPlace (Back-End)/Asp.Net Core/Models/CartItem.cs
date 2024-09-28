namespace DotnetBackend.Models
{
    public class CartItem
    {
        private int id;
        private string item;
        private int qty;
        private double? price;
        private double? amount;
        private int? farmer_id;

        public CartItem()
        {
            Console.WriteLine("Cart Constructor invoked");
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Item
        {
            get { return item; }
            set { item = value; }
        }

        public int Qty
        {
            get { return qty; }
            set { qty = value; }
        }

        public double? Price
        {
            get { return price; }
            set { price = value; }
        }

        public double? Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        public int? Farmer_id
        {
            get { return farmer_id; }
            set { farmer_id = value; }
        }

        public override string ToString()
        {
            return "CartItem [id=" + id + ", item=" + item + ", qty=" + qty + ", price=" + price + ", amount=" + amount
                + "]";
        }
    }
}