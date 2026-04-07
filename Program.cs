using System;

class Product
{
    public int Id;
    public string Name;
    public double Price;
    public int RemainingStock;

    public void DisplayProduct() // shows the product details
    {
        Console.WriteLine($"{Id}. {Name} - ₱{Price} (Stock: {RemainingStock})");
    }

    public double GetItemTotal(int quantity) // calculates the total price for a given quantity of a product
    {
        return Price * quantity;
    }

    public bool HasEnoughStock(int quantity) // checks stock
    {
        return quantity <= RemainingStock;
    }

    public void DeductStock(int quantity) //deducts the quantity from the remaining stock
    
    {
        RemainingStock -= quantity;
    }
}
class Program
{
    static void Main()
    {
      
    }

    
}

