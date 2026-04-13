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
      Product[] store = new Product[3]; //created an array of products to represent the store's inventory

    store[0] = new Product { Id = 1, Name = "Bottled Water", Price = 15, RemainingStock = 30 };
    store[1] = new Product { Id = 2, Name = "Potato Chips", Price = 20, RemainingStock = 25 };
    store[2] = new Product { Id = 3, Name = "Coke", Price = 25, RemainingStock = 30 };

    //menu display
    for (int i = 0; i < store.Length; i++)  
    {
        store[i].DisplayProduct();
    }

    //cart management
    Product[] cart = new Product[10]; //stores the products added to the cart
    int[] quantities = new int[10]; //stores the quantities of each product in the cart
    int cartCount = 0;  // keeps track of the number of items in the cart

    //user input and validation
    
    while (true)
    {
        Console.Write("Enter product number: ");
        string input = Console.ReadLine();

        if (!int.TryParse(input, out int choice))
        {
        Console.WriteLine("Invalid input!");
        continue;
        }
        if (choice < 1 || choice > store.Length)
        {
        Console.WriteLine("Invalid product number!");
        continue;
        }

        Console.Write("Enter quantity: ");
        string qtyInput = Console.ReadLine();

        if (!int.TryParse(qtyInput, out int qty) || qty <= 0)
        {
        Console.WriteLine("Invalid quantity!");
        continue;
        }
        Product selected = store[choice - 1];

        if (selected.RemainingStock == 0)
        {
        Console.WriteLine("Product is out of stock!");
        continue;
        }

        if (!selected.HasEnoughStock(qty))
        {
        Console.WriteLine("Not enough stock available.");
        continue;
        }
    }

    Console.WriteLine("Valid input! Proceeding..."); 
    }

    
}

