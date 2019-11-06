using ShopDatabaseAdvanced.Model;
using ShopDatabaseAdvanced.ShopAdvancedDbContext;
using ShopDatabaseAdvanced.ShopAdvancedDbContext.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDatabaseAdvanced
{
    class Program
    {
        static void Main(string[] args)
        {
            //List<Food> groceries = new List<Food>
            //{
            //    new Food("apple", 1.7),
            //    new Food("bread", 1.2),
            //    new Food("cheese", 2),
            //    new Food("milk", 1),
            //    new Food("icecream", 1.5)
            //};

            using (var db = new AdvancedShopDatabaseContext())
            {
                //db.Foods.AddRange(groceries);
                //db.SaveChanges();

                var customers = db.Customers;

                Console.WriteLine("Sisestage oma nimi");
                string name = Console.ReadLine();

                
                if (customers.FirstOrDefault(x => x.Name == name) != null)
                {
                    Console.WriteLine("Tere " + name);
                    Customer customer = customers.FirstOrDefault(x => x.Name == name);
                    customer.addVisit();
                } else
                {
                    Customer customer = new Customer(name);
                    Console.WriteLine("Tere " + name);
                    db.Customers.Add(customer);
                    db.SaveChanges();
                }
                

                ShoppingCart newCart = new ShoppingCart();
                db.ShoppingCarts.Add(newCart);

                ChooseFood(db, newCart);
                while (Console.ReadLine() == "j")
                {
                    ChooseFood(db, newCart);
                }

                db.SaveChanges();

                var shoppingCarts = db.ShoppingCarts.Include("Items");
                foreach(var item in newCart.Items)
                {
                    
                    Console.WriteLine($"{item.Name} hind: {item.Price}");
                    
                }
                Console.WriteLine($"Ostukorvi summa: {newCart.Sum}");
                //db.Customers.FirstOrDefault(x => x.Name == name).ShoppingCarts.Add(newCart);
                Console.WriteLine((db.Customers.FirstOrDefault(x => x.Name == name)).ShoppingCarts );
                //customer1.ShoppingCarts.Add(newCart);
                db.SaveChanges();
            }
            Console.ReadKey();
        }

        private static void ChooseFood(AdvancedShopDatabaseContext db, ShoppingCart newCart)
        {
            Console.WriteLine();
            Console.WriteLine("Mida soovite osta?");
            string foodName = Console.ReadLine();
            Food chosenFood = db.Foods.FirstOrDefault(x => x.Name == foodName);
            newCart.AddToCart(chosenFood);
            Console.WriteLine();
            Console.WriteLine("Midagi veel? j/e");
        }
    }
}
