using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PerishableItem : ConcreteStorageItem
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