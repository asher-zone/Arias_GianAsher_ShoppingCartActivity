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
      Product[] store = new Product[5]; //created an array of products to represent the store's inventory

    store[0] = new Product { Id = 1, Name = "Backpack", Price = 1500, RemainingStock = 300 };
    store[1] = new Product { Id = 2, Name = "Running Shoes", Price = 2000, RemainingStock = 250 };
    store[2] = new Product { Id = 3, Name = "Tumbler", Price = 500, RemainingStock = 300 };
    store[3] = new Product { Id = 4, Name = "Badminton Racket", Price = 3000, RemainingStock = 0 };
    store[4] = new Product { Id = 5, Name = "Yoga Mat", Price = 800, RemainingStock = 200 };

    //menu display
    Console.WriteLine("--- Welcome to the Store! ---");
    Console.WriteLine("Available Products:");
    for (int i = 0; i < store.Length; i++)  
    {
        store[i].DisplayProduct();
    }

    //cart management
    Product[] cart = new Product[3]; //stores the products added to the cart
    int[] quantities = new int[3]; //stores the quantities of each product in the cart
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
    
    //preventing duplicate entries in the cart and updating quantities if the product is already in the cart
    bool found = false;

    for (int i = 0; i < cartCount; i++)
    {
        if (cart[i].Id == selected.Id)
        {
            quantities[i] += qty;
            found = true;
            break;
        }
    }
    if (!found)
        {
            if (cartCount >= cart.Length)
            {
                Console.WriteLine("Cart is full.");
                
                Console.Write("Add more? (Y/N): ");
                string againFull = Console.ReadLine();

                if (againFull.ToUpper() != "Y")
                {
                    break; // exit loop → go to receipt
                }
                else
                {
                    continue; // go back to menu
            }

            cart[cartCount] = selected;
            quantities[cartCount] = qty;
            cartCount++;
        }
    selected.DeductStock(qty);
    Console.WriteLine("Item added to cart!");

        Console.Write("Add more? (Y/N): ");
        string again = Console.ReadLine();

        if (again.ToUpper() != "Y")
            break;
 //displaying the updated stock after purchase
        Console.WriteLine("\nUpdated Stock:");

        for (int i = 0; i < store.Length; i++)
        {
            store[i].DisplayProduct();
        }
    }
        //displaying the cart contents and calculating the total price
        double grandTotal = 0;

        Console.WriteLine("\n--- RECEIPT ---");

        for (int i = 0; i < cartCount; i++)
        {
            double total = cart[i].GetItemTotal(quantities[i]);
            grandTotal += total;

            Console.WriteLine($"{cart[i].Name} x{quantities[i]} = ₱{total}");
        }
        double discount = 0;

        if (grandTotal >= 5000)
        {
            discount = grandTotal * 0.10;
        }

        double finalTotal = grandTotal - discount;

        Console.WriteLine($"Grand Total: ₱{grandTotal}");
        Console.WriteLine($"Discount: ₱{discount}");
        Console.WriteLine($"Final Total: ₱{finalTotal}");



    }

}
