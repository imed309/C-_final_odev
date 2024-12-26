using System;
using System.Collections.Generic;
using System.Globalization;

// Abstract class for storage items
public abstract class StorageItem
{
    public string Name { get; private set; }
    public double Price { get; private set; }
    public int Quantity { get; private set; }

    public StorageItem(string name, double price, int quantity)
    {
        Name = name;
        Price = price;
        Quantity = quantity;
    }

    public abstract double CalculatePrice();

    public void UpdateQuantity(int quantity)
    {
        if (quantity >= 0)
        {
            Quantity = quantity;
        }
        else
        {
            Console.WriteLine("Quantity can't be negative.");
        }
    }
}

public class PerishableItem : StorageItem
{
    public DateTime ExpirationDate { get; private set; }

    public PerishableItem(string name, double price, int quantity, DateTime expirationDate)
        : base(name, price, quantity)
    {
        ExpirationDate = expirationDate;
    }

    public override double CalculatePrice()
    {
        if (ExpirationDate < DateTime.Now.AddDays(7))
        {
            return Price * 0.8; // 20% discount if the item is near expiration
        }
        return Price;
    }
}

public class NonPerishableItem : StorageItem
{
    public NonPerishableItem(string name, double price, int quantity)
        : base(name, price, quantity) { }

    public override double CalculatePrice()
    {
        return Price; // No discount for non-perishable items
    }
}

public class Inventory
{
    private List<StorageItem> items;

    public Inventory()
    {
        items = new List<StorageItem>();
    }

    public void AddItem(StorageItem item)
    {
        items.Add(item);
    }

    public void DisplayItems()
    {
        Console.WriteLine("Inventory Items:");
        foreach (var item in items)
        {
            Console.WriteLine($"{item.Name}: {item.CalculatePrice()} (Quantity: {item.Quantity})");
        }
    }

    public void UpdateItemQuantity(string name, int quantity)
    {
        var item = items.Find(i => i.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        if (item != null)
        {
            item.UpdateQuantity(quantity);
            Console.WriteLine($"Updated quantity for {item.Name} to {item.Quantity}.");
        }
        else
        {
            Console.WriteLine("Item not found.");
        }
    }
}