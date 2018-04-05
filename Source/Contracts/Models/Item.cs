namespace Contracts.Models
{
    using System;

    public class Item
    {
        public Item(string description, int quantity, decimal amount)
        {
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Quantity = quantity;
            Amount = amount;
        }

        public string Description { get; set; }

        public int Quantity { get; set; }

        public decimal Amount { get; set; }
    }
}