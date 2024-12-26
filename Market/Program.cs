using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Market
{
    class Program
    {
        static void Main(string[] args)
        {
            Inventory inventory = new Inventory();
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("Supermarket Storage System");
                Console.WriteLine("1. Add Perishable Item");
                Console.WriteLine("2. Add Non-Perishable Item");
                Console.WriteLine("3. Display Inventory");
                Console.WriteLine("4. Update Item Quantity");
                Console.WriteLine("5. Exit");
                Console.Write("Choose an option: ");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        AddPerishableItem(inventory);
                        break;
                    case "2":
                        AddNonPerishableItem(inventory);
                        break;
                    case "3":
                        inventory.DisplayItems();
                        break;
                    case "4":
                        UpdateItemQuantity(inventory);
                        break;
                    case "5":
                        running = false;
                        Console.WriteLine("Exiting system...");
                        break;
                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }

                if (running)
                {
                    Console.WriteLine("\nPress Enter to continue...");
                    Console.ReadLine();
                }
            }
        }

        static void AddPerishableItem(Inventory inventory)
        {
            Console.Write("Enter item name: ");
            string name = Console.ReadLine();

            Console.Write("Enter item price: ");
            double price = double.Parse(Console.ReadLine());

            Console.Write("Enter item quantity: ");
            int quantity = int.Parse(Console.ReadLine());

            DateTime expirationDate = DateTime.Now;
            bool validDate = false;

            // Loop to ensure valid date input in yyyy-MM-dd format
            while (!validDate)
            {
                Console.Write("Enter expiration date (yyyy-MM-dd): ");
                string dateInput = Console.ReadLine();

                // Try to parse the date in the exact format
                validDate = DateTime.TryParseExact(dateInput, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out expirationDate);

                if (!validDate)
                {
                    Console.WriteLine("Invalid date format. Please enter the date in the correct format (yyyy-MM-dd).");
                }
            }

            PerishableItem item = new PerishableItem(name, price, quantity, expirationDate);
            inventory.AddItem(item);

            Console.WriteLine($"Added perishable item: {name}");
        }

        static void AddNonPerishableItem(Inventory inventory)
        {
            Console.Write("Enter item name: ");
            string name = Console.ReadLine();

            Console.Write("Enter item price: ");
            double price = double.Parse(Console.ReadLine());

            Console.Write("Enter item quantity: ");
            int quantity = int.Parse(Console.ReadLine());

            NonPerishableItem item = new NonPerishableItem(name, price, quantity);
            inventory.AddItem(item);

            Console.WriteLine($"Added non-perishable item: {name}");
        }

        static void UpdateItemQuantity(Inventory inventory)
        {
            Console.Write("Enter item name to update: ");
            string name = Console.ReadLine();

            Console.Write("Enter new quantity: ");
            int quantity = int.Parse(Console.ReadLine());

            inventory.UpdateItemQuantity(name, quantity);
        }
    }
}
