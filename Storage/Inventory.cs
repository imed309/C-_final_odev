using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.Json;

public class Inventory
{
    private readonly FileStorageRepository repository;

    public Inventory(FileStorageRepository repository)
    {
        this.repository = repository;
    }

    public Result AddItem(ConcreteStorageItem item)
    {
        if (item == null)
        {
            return Result.Fail("Item cannot be null.");
        }

        List<ConcreteStorageItem> items = repository.GetAll();
        items.Add(item);
        repository.SaveAll(items);

        return Result.Success($"Item '{item.Name}' added successfully.");
    }

    public Result<List<ConcreteStorageItem>> GetItems()
    {
        var items = repository.GetAll();
        if (items.Any())
        {
            return Result<List<ConcreteStorageItem>>.Success(items);
        }
        return Result<List<ConcreteStorageItem>>.Fail("No items in inventory.");
    }

    public Result UpdateItemQuantity(string name, int quantity)
    {
        var items = repository.GetAll();
        var item = items.Find(i => i.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        if (item == null)
        {
            return Result.Fail($"Item '{name}' not found.");
        }

        item.UpdateQuantity(quantity);
        repository.SaveAll(items);
        return Result.Success($"Updated quantity for {item.Name} to {item.Quantity}.");
    }

    public Result<string> DisplayItems()
    {
        var items = repository.GetAll();
        if (!items.Any())
        {
            return Result<string>.Fail("Inventory is empty.");
        }

        var display = string.Join("\n", items.Select(item => $"{item.Name}: {item.CalculatePrice()} (Quantity: {item.Quantity})"));
        return Result<string>.Success(display);
    }
}

public interface IStorageRepository
{
    List<ConcreteStorageItem> GetAll();
    void SaveAll(List<ConcreteStorageItem> items);
}

public class FileStorageRepository : IStorageRepository
{
    private readonly string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "inventory.json");
    public List<ConcreteStorageItem> items = new List<ConcreteStorageItem>();

    public FileStorageRepository()
    {
        items = LoadFromFile();
    }

    public List<ConcreteStorageItem> GetAll()
    {
        return items;
    }

    public void SaveAll(List<ConcreteStorageItem> items)
    {
        this.items = items;
        try
        {
            var json = JsonSerializer.Serialize(items, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving to file: {ex.Message}");
        }
    }

    private List<ConcreteStorageItem> LoadFromFile()
    {
        try
        {
            if (!File.Exists(filePath))
            {
                return new List<ConcreteStorageItem>();
            }

            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<ConcreteStorageItem>>(json) ?? new List<ConcreteStorageItem>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading from file: {ex.Message}");
            return new List<ConcreteStorageItem>();
        }
    }
}

public class Result
{
    public bool IsSuccess { get; }
    public string Message { get; }

    protected Result(bool isSuccess, string message)
    {
        IsSuccess = isSuccess;
        Message = message;
    }

    public static Result Success(string message = "Success")
    {
        return new Result(true, message);
    }

    public static Result Fail(string message)
    {
        return new Result(false, message);
    }
}

public class Result<T> : Result
{
    public T Data { get; }

    private Result(bool isSuccess, string message, T data) : base(isSuccess, message)
    {
        Data = data;
    }

    public static Result<T> Success(T data, string message = "Success")
    {
        return new Result<T>(true, message, data);
    }

    public static Result<T> Fail(string message)
    {
        return new Result<T>(false, message, default);
    }
}