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

            using (var db = new AdvancedShopDatabaseContext())
            {
                //db.Foods.AddRange(groceries);
                //db.SaveChanges();

                var customers = db.Customers;
                var foods = db.Foods;

                Console.WriteLine("Sisestage oma nimi");
                string name = Console.ReadLine();

                
                if (customers.FirstOrDefault(x => x.Name == name) != null)
                {
                    Console.WriteLine("Tere " + name);
                    Customer customer = customers.FirstOrDefault(x => x.Name == name);
                } else
                {
                    Customer customer = new Customer(name);
                    Console.WriteLine("Tere " + name);
                    customers.Add(customer);
                    db.SaveChanges();
                }
                

                ShoppingCart newCart = new ShoppingCart();
                db.ShoppingCarts.Add(newCart);

                Console.WriteLine("Kaubavalik:");
                foreach (var item in foods)
                {
                    Console.WriteLine(item.Name);
                }

                ChooseFood(db, newCart);
                while (Console.ReadLine() == "j")
                {
                    ChooseFood(db, newCart);
                }

                db.SaveChanges();

                var shoppingCarts = db.ShoppingCarts.Include("Items");
                Console.WriteLine("Ostukorvi sisu:");
                foreach(var item in newCart.Items)
                {
                    
                    Console.WriteLine($"{item.Name} hind: {item.Price}");
                    
                }
                Console.WriteLine($"Ostukorvi summa: {newCart.Sum}");
                customers.FirstOrDefault(x => x.Name == name).ShoppingCarts.Add(newCart);
                db.SaveChanges();

                var shoppingCount = customers.FirstOrDefault(x => x.Name == name).ShoppingCarts.Count();

                Console.WriteLine("Külastuste arv: " + shoppingCount);
                if (shoppingCount > 1)
                {
                    Console.WriteLine("Kas soovite näha oma ostude ajalugu? (j/e)");
                    string answer = Console.ReadLine();
                    if (answer == "j")
                    {
                        var shoppingHistory = customers.FirstOrDefault(x => x.Name == name).ShoppingCarts.OrderByDescending(y => y.DateCreated);
                        foreach (var item in shoppingHistory)
                        {
                            Console.WriteLine("Kuupäev: " + item.DateCreated);
                            foreach (var contents in item.Items)
                            {
                                Console.WriteLine(contents.Name);
                            }
                            Console.WriteLine("Ostukorvi summa: " + item.Sum);
                        }
                    }
                }
            }
            Console.WriteLine("Täname külastamast, head päeva jätku!");
            Console.ReadKey();
        }

        private static void ChooseFood(AdvancedShopDatabaseContext db, ShoppingCart newCart)
        {
            Console.WriteLine("Mida soovite osta?");
            string foodName = Console.ReadLine();
            Food chosenFood = db.Foods.FirstOrDefault(x => x.Name == foodName);
            if (chosenFood != null)
            {
                newCart.AddToCart(chosenFood);
            }
            else
            {
                Console.WriteLine("Sellist toitu ei ole valikus");
            }
            
            Console.WriteLine("Midagi veel? j/e");
        }
    }
}
