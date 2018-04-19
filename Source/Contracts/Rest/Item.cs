namespace DotNetCore.Contracts.Rest
{
    public class Item
    {
        public string Name { get; set; }

        public string Sku { get; set; } 

        public int Quantity { get; set; }

        public decimal Amount { get; set; }
    }
}