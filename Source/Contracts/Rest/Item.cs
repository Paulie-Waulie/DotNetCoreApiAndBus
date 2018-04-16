namespace Contracts.Rest
{
    using System;

    public class Item
    {
        public Item(string name, string sku, int quantity, decimal amount)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Sku = sku ?? throw new ArgumentNullException(nameof(sku));
            Quantity = quantity;
            Amount = amount;
        }

        public string Name { get; set; }

        public string Sku { get; set; } 

        public int Quantity { get; set; }

        public decimal Amount { get; set; }
    }
}