using ShopDatabaseAdvanced.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDatabaseAdvanced.ShopAdvancedDbContext.Model
{
    public class Customer
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; }

        public Customer()
        {

        }

        public Customer(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
            ShoppingCarts = new List<ShoppingCart>();
        }
        
    }
}
