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
        static void CartMenu(Product[] store, Product[] cart, int[] quantities, ref int cartCount)
        {
            while (true)
            {
                Console.WriteLine("\n=== CART MENU ===");
                Console.WriteLine("1. View Cart");
                Console.WriteLine("2. Remove Item");
                Console.WriteLine("3. Update Quantity");
                Console.WriteLine("4. Clear Cart");
                Console.WriteLine("5. Checkout");
                Console.WriteLine("6. View Order History");

                Console.Write("Choose an option: ");
                string input = Console.ReadLine();

                if (!int.TryParse(input, out int choice))
                {
                    Console.WriteLine("Invalid input!");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        ViewCart(cart, quantities, cartCount);
                        break;

                    case 2:
                        RemoveItem(cart, quantities, ref cartCount);
                        break;

                    case 3:
                        UpdateQuantity(cart, quantities, cartCount);
                        break;

                    case 4:
                        ClearCart(ref cartCount);
                        break;

                    case 5:
                        double finalTotal = ShowReceipt(cart, quantities, cartCount);

                        double payment = ProcessPayment(finalTotal);

                        //LOW STOCK ALERTTT
                        Console.WriteLine("\nProcessing complete...");
                        ShowLowStock(store);

                        //SAVE TO ORDER HISTORY
                        if (orderCount < orderHistory.Length)
                        {
                            orderHistory[orderCount] = $"Receipt #{receiptCounter:D4} - Final Total: ₱{finalTotal}";
                            orderCount++;
                        }

                        receiptCounter++;


                        return;// exit cart menu → proceed to checkout

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }
            //search for products
            static void SearchProduct(Product[] store)
        {
            Console.Write("Enter product name to search: ");
            string keyword = Console.ReadLine().ToLower();

            bool found = false;

            for (int i = 0; i < store.Length; i++)
            {
                if (store[i].Name.ToLower().Contains(keyword))
                {
                    store[i].DisplayProduct();
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("No matching products found.");
            }
        }
            //search and/or filter by category
            static void FilterByCategory(Product[] store)
        {
            Console.Write("Enter category: ");
            string category = Console.ReadLine().ToLower();

            bool found = false;

            for (int i = 0; i < store.Length; i++)
            {
                if (store[i].Category.ToLower() == category)
                {
                    store[i].DisplayProduct();
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("No products in this category.");
            }
        }
            static void ViewCart(Product[] cart, int[] quantities, int cartCount)
        {
            if (cartCount == 0)
            {
                Console.WriteLine("Cart is empty.");
                return;
            }

            Console.WriteLine("\n--- YOUR CART ---");

            for (int i = 0; i < cartCount; i++)
            {
                Console.WriteLine($"{i + 1}. {cart[i].Name} x{quantities[i]}");
            }
        }
            static void RemoveItem(Product[] cart, int[] quantities, ref int cartCount)
        {
            if (cartCount == 0)
            {
                Console.WriteLine("Cart is empty.");
                return;
            }

            ViewCart(cart, quantities, cartCount);

            Console.Write("Enter item number to remove: ");
            string input = Console.ReadLine();

            if (!int.TryParse(input, out int index) || index < 1 || index > cartCount)
            {
                Console.WriteLine("Invalid selection.");
                return;
            }

            index--; // adjust index

            for (int i = index; i < cartCount - 1; i++)
            {
                cart[i] = cart[i + 1];
                quantities[i] = quantities[i + 1];
            }

            cartCount--;

            Console.WriteLine("Item removed.");
        }
            static void UpdateQuantity(Product[] cart, int[] quantities, int cartCount)
        {
            if (cartCount == 0)
            {
                Console.WriteLine("Cart is empty.");
                return;
            }

            ViewCart(cart, quantities, cartCount);

            Console.Write("Enter item number to update: ");
            string input = Console.ReadLine();

            if (!int.TryParse(input, out int index) || index < 1 || index > cartCount)
            {
                Console.WriteLine("Invalid selection.");
                return;
            }

            Console.Write("Enter new quantity: ");
            string qtyInput = Console.ReadLine();

            if (!int.TryParse(qtyInput, out int newQty) || newQty <= 0)
            {
                Console.WriteLine("Invalid quantity.");
                return;
            }

            quantities[index - 1] = newQty;

            Console.WriteLine("Quantity updated.");
        }
            static void ClearCart(ref int cartCount)
            {
            cartCount = 0;
            Console.WriteLine("Cart cleared.");
            }

            static int receiptCounter = 1; // global counter

            static double ShowReceipt(Product[] cart, int[] quantities, int cartCount)
            {
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

                //RECEIPT NUMBER + DATE
                Console.WriteLine($"\nReceipt No: {receiptCounter:D4}");
                Console.WriteLine($"Date: {DateTime.Now}");

                Console.WriteLine($"Grand Total: ₱{grandTotal}");
                Console.WriteLine($"Discount: ₱{discount}");
                Console.WriteLine($"Final Total: ₱{finalTotal}");

                return finalTotal;
            }

            static double ProcessPayment(double finalTotal)
            {
                while (true)
                {
                    Console.Write("Enter payment: ");
                    string input = Console.ReadLine();

                    if (!double.TryParse(input, out double payment))
                    {
                        Console.WriteLine("Invalid input! Enter a number.");
                        continue;
                    }

                    if (payment < finalTotal)
                    {
                        Console.WriteLine("Insufficient payment.");
                        continue;
                    }

                    double change = payment - finalTotal;

                    Console.WriteLine($"Payment: ₱{payment}");
                    Console.WriteLine($"Change: ₱{change}");

                    return payment;
                }
            }
            static void ShowOrderHistory()
            {
                if (orderCount == 0)
                {
                    Console.WriteLine("No orders yet.");
                    return;
                }

                Console.WriteLine("\n--- ORDER HISTORY ---");

                for (int i = 0; i < orderCount; i++)
                {
                    Console.WriteLine(orderHistory[i]);
                }
            }
            static bool AskYesNo(string message)
            {
                while (true)
                {
                    Console.Write(message);
                    string input = Console.ReadLine().ToUpper();

                    if (input == "Y") return true;
                    if (input == "N") return false;

                    Console.WriteLine("Invalid input. Please enter Y or N only.");
                }
            }
            static void ShowLowStock(Product[] store)
            {
                Console.WriteLine("\n--- LOW STOCK ALERT ---");

                bool hasLowStock = false;

                for (int i = 0; i < store.Length; i++)
                {
                    if (store[i].RemainingStock > 0 && store[i].RemainingStock <= 5)
                    {
                        Console.WriteLine($"{store[i].Name} has only {store[i].RemainingStock} stocks left.");
                        hasLowStock = true;
                    }
                }

                if (!hasLowStock)
                {
                    Console.WriteLine("All products have sufficient stock.");
                }
            }
            static void Checkout(Product[] store, Product[] cart, int[] quantities, ref int cartCount)
            {
                double finalTotal = ShowReceipt(cart, quantities, cartCount);

                double payment = ProcessPayment(finalTotal);

                Console.WriteLine("\nProcessing complete...");
                ShowLowStock(store);

                // SAVE ORDER
                if (orderCount < orderHistory.Length)
                {
                    orderHistory[orderCount] = $"Receipt #{receiptCounter:D4} - Final Total: ₱{finalTotal}";
                    orderCount++;
                }

                receiptCounter++;

                // clear cart after checkout
                cartCount = 0;
            }
}
