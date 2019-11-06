namespace ShopDatabaseAdvanced.Migrations
{
    using ShopDatabaseAdvanced.Model;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ShopDatabaseAdvanced.ShopAdvancedDbContext.AdvancedShopDatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ShopDatabaseAdvanced.ShopAdvancedDbContext.AdvancedShopDatabaseContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            

            context.Foods.AddOrUpdate(food => food.Name, new Food
            {
                Name = "õun",
                Price = 1.2
            }, new Food
            {
                Name = "leib",
                Price = 1.7
            }, new Food
            {
                Name = "juust",
                Price = 2
            }, new Food
            {
                Name = "piim",
                Price = 1
            }, new Food
            {
                Name = "jäätis",
                Price = 2.3
            });
            context.SaveChanges();
        }
    }
}
