using System;

class Product //created product class with properties and methods to manage product details, stock, and pricing
{
    public int Id;
    public string Name;
    public double Price;
    public int RemainingStock;
    public string Category;

    public void DisplayProduct() // shows the product details
    {
        Console.WriteLine($"{Id}. {Name} - ₱{Price} (Stock: {RemainingStock}) [{Category}]");
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
    static string[] orderHistory = new string[10];
    static int orderCount = 0;
    static void Main()
    {
        Product[] store = new Product[5]; //created an array of products to represent the store's inventory

        store[0] = new Product { Id = 1, Name = "Backpack", Price = 1500, RemainingStock = 300, Category = "Clothing" };
        store[1] = new Product { Id = 2, Name = "Running Shoes", Price = 2000, RemainingStock = 250, Category = "Clothing" };
        store[2] = new Product { Id = 3, Name = "Tumbler", Price = 500, RemainingStock = 300, Category = "Accessories" };
        store[3] = new Product { Id = 4, Name = "Badminton Racket", Price = 3000, RemainingStock = 0, Category = "Sports" };
        store[4] = new Product { Id = 5, Name = "Yoga Mat", Price = 800, RemainingStock = 200, Category = "Sports" };

        //cart management
        Product[] cart = new Product[3]; //stores the products added to the cart
        int[] quantities = new int[3]; //stores the quantities of each product in the cart
        int cartCount = 0;  // keeps track of the number of items in the cart

        while (true)
        {
            Console.WriteLine("\n=== MAIN MENU ===");
            Console.WriteLine("1. View Products");
            Console.WriteLine("2. Search Product");
            Console.WriteLine("3. Filter by Category");
            Console.WriteLine("4. Add to Cart");
            Console.WriteLine("5. Exit");
            Console.WriteLine("6. View Order History");

            Console.Write("Choose an option: ");
            string menuInput = Console.ReadLine();

            if (!int.TryParse(menuInput, out int menuChoice))
            {
                Console.WriteLine("Invalid input!");
                continue;
            }

            switch (menuChoice)
            {
                case 1:
                    for (int i = 0; i < store.Length; i++)
                        store[i].DisplayProduct();
                    break;

                case 2:
                    SearchProduct(store);
                    break;

                case 3:
                    FilterByCategory(store);
                    break;

                case 4:
                    Console.WriteLine("Add to cart selected.");

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

                        Console.WriteLine("Valid input! Proceeding...");

                        //ADD TO CART
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

                            }

                            cart[cartCount] = selected;
                            quantities[cartCount] = qty;
                            cartCount++;
                        }

                        selected.DeductStock(qty);
                        Console.WriteLine("Item added to cart!");

                        //ask user if they want to continue
                        if (!AskYesNo("Add more? (Y/N): "))
                            break;
                    }

                    //opens cart menu if user inputs n
                    CartMenu(store, cart, quantities, ref cartCount);

                    break;
            

                case 5:
                    Checkout(store, cart, quantities, ref cartCount);
                    return;
                
                case 6:
                    ShowOrderHistory();
                    break;

                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }   
    }
