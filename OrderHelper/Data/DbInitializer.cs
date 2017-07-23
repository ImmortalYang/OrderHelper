using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OrderHelper.Models;
using OrderHelper.Data;

namespace OrderHelper.Data
{

    public static class DbInitializer
    {
        public static void Initialize(OrderHelperContext context)
        {
            context.Database.EnsureCreated();

            //Look for any Products
            if (context.Products.Any())
            {
                return; //DB has been seeded
            }

            var categories = new Category[]
            {
                new Category { CategoryName = "Main", },
                new Category { CategoryName = "Wrap", },
                new Category { CategoryName = "Side", },
            };
            foreach (Category category in categories)
            {
                context.Categories.Add(category);
            }
            context.SaveChanges();

            var products = new Product[]
            {
                new Product {
                    CategoryID = context.Categories.Where(c => c.CategoryName=="Main").FirstOrDefault().ID,
                    ProductName = "Pork and Shrimps",
                    UnitPrice = 11.5M,
                },
                new Product {
                    CategoryID = context.Categories.Where(c => c.CategoryName=="Main").FirstOrDefault().ID,
                    ProductName = "Pork and Vege",
                    UnitPrice = 11.5M,
                },
                new Product {
                    CategoryID = context.Categories.Where(c => c.CategoryName=="Main").FirstOrDefault().ID,
                    ProductName = "Chicken Laksa",
                    UnitPrice = 16.5M,
                },
                new Product {
                    CategoryID = context.Categories.Where(c => c.CategoryName=="Main").FirstOrDefault().ID,
                    ProductName = "Seafood Laksa",
                    UnitPrice = 16.5M,
                },
                new Product {
                    CategoryID = context.Categories.Where(c => c.CategoryName=="Main").FirstOrDefault().ID,
                    ProductName = "Seasonal Vege Laksa",
                    UnitPrice = 15M,
                },
                new Product {
                    CategoryID = context.Categories.Where(c => c.CategoryName=="Main").FirstOrDefault().ID,
                    ProductName = "Curry Chicken",
                    UnitPrice = 15M,
                },
                new Product {
                    CategoryID = context.Categories.Where(c => c.CategoryName=="Main").FirstOrDefault().ID,
                    ProductName = "Beef Kandar",
                    UnitPrice = 15M,
                },
                new Product {
                    CategoryID = context.Categories.Where(c => c.CategoryName=="Main").FirstOrDefault().ID,
                    ProductName = "Pork Belly",
                    UnitPrice = 15M,
                },
                new Product {
                    CategoryID = context.Categories.Where(c => c.CategoryName=="Main").FirstOrDefault().ID,
                    ProductName = "Satay Chicken",
                    UnitPrice = 15M,
                },
                new Product {
                    CategoryID = context.Categories.Where(c => c.CategoryName=="Main").FirstOrDefault().ID,
                    ProductName = "Satay Beef",
                    UnitPrice = 15M,
                },

                new Product {
                    CategoryID = context.Categories.Where(c => c.CategoryName=="Wrap").FirstOrDefault().ID,
                    ProductName = "Beef Kandar S.",
                    UnitPrice = 7.8M,
                },
                new Product {
                    CategoryID = context.Categories.Where(c => c.CategoryName=="Wrap").FirstOrDefault().ID,
                    ProductName = "Beef Kandar L.",
                    UnitPrice = 11M,
                },
                new Product {
                    CategoryID = context.Categories.Where(c => c.CategoryName=="Wrap").FirstOrDefault().ID,
                    ProductName = "Chicken Curry S.",
                    UnitPrice = 7.8M,
                },
                new Product {
                    CategoryID = context.Categories.Where(c => c.CategoryName=="Wrap").FirstOrDefault().ID,
                    ProductName = "Chicken Curry L.",
                    UnitPrice = 11M,
                },
                new Product {
                    CategoryID = context.Categories.Where(c => c.CategoryName=="Wrap").FirstOrDefault().ID,
                    ProductName = "Satay Chicken S.",
                    UnitPrice = 7.8M,
                },
                new Product {
                    CategoryID = context.Categories.Where(c => c.CategoryName=="Wrap").FirstOrDefault().ID,
                    ProductName = "Satay Chicken L.",
                    UnitPrice = 11M,
                },
                new Product {
                    CategoryID = context.Categories.Where(c => c.CategoryName=="Wrap").FirstOrDefault().ID,
                    ProductName = "Satay Beef S.",
                    UnitPrice = 7.8M,
                },
                new Product {
                    CategoryID = context.Categories.Where(c => c.CategoryName=="Wrap").FirstOrDefault().ID,
                    ProductName = "Satay Beef L.",
                    UnitPrice = 11M,
                },

                new Product {
                    CategoryID = context.Categories.Where(c => c.CategoryName=="Side").FirstOrDefault().ID,
                    ProductName = "Curly Fries",
                    UnitPrice = 9M,
                },
                new Product {
                    CategoryID = context.Categories.Where(c => c.CategoryName=="Side").FirstOrDefault().ID,
                    ProductName = "Kumara Chips",
                    UnitPrice = 12M,
                },
                new Product {
                    CategoryID = context.Categories.Where(c => c.CategoryName=="Side").FirstOrDefault().ID,
                    ProductName = "Home Roasted",
                    UnitPrice = 12M,
                },
                new Product {
                    CategoryID = context.Categories.Where(c => c.CategoryName=="Side").FirstOrDefault().ID,
                    ProductName = "Malaysia Style",
                    UnitPrice = 14M,
                },
                new Product {
                    CategoryID = context.Categories.Where(c => c.CategoryName=="Side").FirstOrDefault().ID,
                    ProductName = "Roti Paratha",
                    UnitPrice = 3.5M,
                },
            };
            foreach (Product product in products)
            {
                context.Products.Add(product);
            }
            context.SaveChanges();
        }
    }
}