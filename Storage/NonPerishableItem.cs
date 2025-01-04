using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class NonPerishableItem : ConcreteStorageItem
{
    public NonPerishableItem(string name, double price, int quantity)
        : base(name, price, quantity) { }

    public override double CalculatePrice()
    {
        return Price; // No discount for non-perishable items
    }
}