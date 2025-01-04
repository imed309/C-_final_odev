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

public class ConcreteStorageItem : StorageItem
{
    public ConcreteStorageItem(string name, double price, int quantity) : base(name, price, quantity) { 
    }
    public override double CalculatePrice()
    {
        return 0.0;
    }
}