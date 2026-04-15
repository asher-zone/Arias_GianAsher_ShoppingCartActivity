# ShoppingCartActivity
Quiz 2 and 3 for Computer Programming 2

# Project Description
A console-based Shopping Cart System in C# using classes, objects, and an array of objects.

# Features
- Display store menu
- User input and validation
- Prevents invalid product selection and quantity
- Checks stock availability before adding items
- Handles out-of-stock products
- Prevents duplicate cart entries
- Limits cart size (up to three (3) unique products only) and shows "Cart is full" message
- Allows continuous shopping until user chooses to exit or "N"
- Calculates:
  - Item totals
  - Grand total
  - 10% discount if total ≥ 5000
  - Final total
- Displays updated stock after checkout

# How to Run the Program
1. Open the project
2. Run the program
3. Follow the on-screen instructions:
   - Enter product number
   - Enter quantity
   - Choose whether to continue shopping
4. View the receipt after checkout

# AI Usage in this project
1. First, I asked AI to break the project instructions down into the simplest it can be
  - Prompt used: i need you to break everything down as simple as it can get and explain to me how i'm gonna go about coding this as i'm only a beginner.

2. I asked AI again to help me fix the structure of my code since I initially put the user input and validation outside the loop.
  - Prompt: how do i add a loop to this part of the code

3. Asked to help me debug my code since the "Cart is full." message was not showing.
  - Prompt: here's all my code, it all works aside from the "Cart is full." message not showing, help me debug it.

4. After fixing problem 3, I encountered another problem where when the program says the cart is full, it keeps on asking for user input(products ID) instead of asking if the user wants to keep adding more products("Add more?" code block).
  - Prompt: i updated the code, now my only problem is that when it says the cart is full, it just keeps on looping from the start asking to input the product ID. how do i fix it to where when the cart is full it'll ask if i want to add more products or finish my purchase

5. After trying to fix the code multiple times I still couldn't fix it, so I asked for help again regarding the same problem on number 4.
  - Prompt: i still don't get where to put the add more function within the code? could you help me out

7. As it turned out, I had to put two "Add more?" code blocks within the code, after the user adds a product to the cart and after the cart is full.
  - Prompt: ohhh okay so i have to include two of those, i removed the code after the successful add and put it in inside the cart is full block lol

8. In summary, I asked questions about debugging issues, improving logic flow, and organizing the program properly. After receiving guidance, I modified and simplified the code to better match my understanding and coding style.
