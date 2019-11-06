using ShopDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Food> groceries = new List<Food>
            {
                new Food("apple", 1.7),
                new Food("bread", 1.2),
                new Food("cheese", 2),
                new Food("milk", 1.5),
                new Food("beer", 1.5),
                new Food("steak", 3),
                new Food("e", 0)
            };

            ShoppingCart newCart = new ShoppingCart();

            //var num = 1;
            //foreach (var food in groceries)
            //{
            //    Console.WriteLine($"{num}. {food.Name} - {food.Price}eur");
            //    num++;

            //}
            //ChooseFood(groceries, newCart);

            //while (Console.ReadLine() == "Y")
            //{
            //    ChooseFood(groceries, newCart);
            //}

            using (var db = new ShopDbContext())
            {
                var cartsWithZeroSum = db.ShoppingCarts.Where(x => x.Sum == 0);
                foreach(var cart in cartsWithZeroSum)
                {
                    db.ShoppingCarts.Remove(cart);
                    
                }
                db.SaveChanges();

                db.ShoppingCarts.Add(newCart);
                db.SaveChanges();

                //var carts = db.ShoppingCarts.Include("Items").OrderByDescending(x => x.DateCreated).ToList();
                //foreach (var cart in carts)
                //{
                //    Console.WriteLine($"Shopping cart created on {cart.DateCreated}");
                //    foreach (var food in cart.Items)
                //    {
                //        Console.WriteLine($"Name: {food.Name},  Price: {food.Price}");
                //    }
                //    Console.WriteLine($"Total sum is: {cart.Sum}");
                //    Console.WriteLine("...................................................................");
                //}

                var carts = db.ShoppingCarts;
                var cartsWithItems = db.ShoppingCarts.Include("Items");
                var foods = db.Foods;

                // Task1: Show Only last cart with all its items
                Console.WriteLine("Task 1:!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                var task = cartsWithItems.OrderByDescending(x => x.DateCreated).ToList().First();
                var task1 = cartsWithItems.OrderBy(cart => cart.DateCreated).ToList().Last();
                Console.WriteLine(task.DateCreated);
                Console.WriteLine(task1.DateCreated);



                // Task2: Show Only carts with Sum > 5
                Console.WriteLine("Task 2:!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                var task2 = carts.Where(cart => cart.Sum > 5).ToList();
                foreach(var cart in task2)
                {
                    Console.WriteLine($"Shopping cart created on {cart.DateCreated}");
                    foreach (var food in cart.Items)
                    {
                        Console.WriteLine($"Name: {food.Name},  Price: {food.Price}");
                    }
                    Console.WriteLine($"Total sum is: {cart.Sum}");
                    Console.WriteLine("..................................................");
                }

                // Task3: Show Only carts with more than one item in it (and how many items in cart)
                Console.WriteLine("Task 3:!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                var task3 = cartsWithItems.Where(cart => cart.Items.Count() > 1).ToList();
                foreach (var cart in task3)
                {
                    Console.WriteLine($"Shopping cart created on {cart.DateCreated}");
                    foreach (var food in cart.Items)
                    {
                        Console.WriteLine($"Name: {food.Name},  Price: {food.Price}");
                    }
                    Console.WriteLine("........................");
                    Console.WriteLine($"Total sum is: {cart.Sum}");
                    Console.WriteLine("Total items in cart: " + cart.Items.Count());
                    Console.WriteLine("---------------------------------------------");
                }


                //query example
                var cartsMoreThan1_query = from cart in cartsWithItems where cart.Items.Count() > 1 select cart;
                foreach (var cart in cartsMoreThan1_query)
                {
                    Console.WriteLine($"Shopping cart created: {cart.DateCreated}, Items count: {cart.Items.Count()}");
                }

                // Task4: Show Only carts that contain apples
                Console.WriteLine("Task 4:!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                var task4 = cartsWithItems.Where(x => x.Items.Any(y => y.Name == "apple")).ToList();
                foreach (var cart in task4)
                {
                    Console.WriteLine($"Shopping cart created: {cart.DateCreated}");
                    foreach (var food in cart.Items)
                    {
                        Console.WriteLine($"Name: {food.Name},  Price: {food.Price}");
                    }
                }


                // Task5: Show Total number of shopping carts
                Console.WriteLine("Task 5:!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                Console.WriteLine("Total number of shopping carts:" + carts.Count());
                
                // Task6: Show the cart with maximum sum
                Console.WriteLine("Task 6:!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                var cartMaxSum = carts.OrderByDescending(x => x.Sum).FirstOrDefault();
                Console.WriteLine($"Most expensive cart cost: {cartMaxSum.Sum}, Created on: {cartMaxSum.DateCreated}");

                // Task7: Show the cheapest food
                var cheapestFood = foods.OrderBy(z => z.Price).FirstOrDefault();
                Console.WriteLine($"Most cheapest food is: {cheapestFood.Name}, With price of: {cheapestFood.Price}");

            }

            Console.ReadKey();
        }

        private static void ChooseFood(List<Food> groceries, ShoppingCart newCart)
        {
            
            Console.WriteLine();
            Console.WriteLine("What ya want?");
            string foodName = Console.ReadLine();
            Food chosenFood = groceries.FirstOrDefault(x => x.Name == foodName);
            newCart.AddToCart(chosenFood);
            Console.WriteLine();
            Console.WriteLine("More stuff? Y/N");
        }
    }
}
