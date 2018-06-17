using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LanguageFeatures.Models;

namespace LanguageFeatures.Controllers
{
    public class HomeController : Controller
    {
        #region Data
        Product[] productArray =
            {
                new Product { Name = "Kayak", Price = 275M },
                new Product { Name = "Lifejacket", Price = 48.95M },
                new Product { Name = "Soccer ball", Price = 19.50M },
                new Product { Name = "Corner flag", Price = 34.95M }
            };
        #endregion
        public ViewResult Index()
        {
            List<string> results = new List<string>();
            foreach (Product product in Product.GetProducts())
            {
                string name = product?.Name ?? "No Name";
                decimal? price = product?.Price ?? 0;
                // The null conditional operator can be applied to each part of a chain of properties, like this:
                string relatedName = product?.Related?.Name ?? "None";
                // using string interpolation 
                results.Add($"Name: {name}, Price: {price}, Related: {relatedName}");
            }
            return View(results);
        }
        public ViewResult UsingDictionary()
        {
            // Old way of initialization
            //Dictionary<string, Product> products = new Dictionary<string, Product>
            //{
            //    { "Kayak", new Product { Name = "Kayak", Price = 275M } },
            //    { "Lifejacket", new Product { Name = "Lifejacket", Price = 48.95M } }
            //};
            // New way of initialization
            Dictionary<string, Product> products = new Dictionary<string, Product>
            {
                ["Kayak"] = new Product { Name = "Kayak", Price = 275M },
                ["Lifejacket"] = new Product { Name = "Lifejacket", Price = 48.95M }
            };
            return View("Index", products.Keys);
        }
        public ViewResult UsingPatternMatching()
        {
            object[] data = new object[] { 275M, 29.9M, "apple", "orange", 100, 10 };
            decimal total = 0;
            for (int i = 0; i < data.Length; i++)
            {
                // В переменную total попадет только 2 заначения из массива data
                // Таке можно отметить что изпользование такого подхода избавляет нас от 
                // дополнительных преобразований 
                if (data[i] is decimal d)
                    total += d;
            }
            return View("Index", new string[] { $"Total: {total:C2}" });
        }
        public ViewResult UsingPatternMatchingWithSwitch()
        {
            object[] data = new object[] { 275M, 29.9M, "apple", "orange", 100, 10 };
            decimal total = 0;
            for (int i = 0; i < data.Length; i++)
            {
                switch (data[i])
                {
                    case decimal decimalVal:
                        total += decimalVal;
                        break;
                    case int intVal when intVal > 50:
                        total += intVal;
                        break;
                }
            }
            return View("Index", new string[] { $"Total: {total:C2}" });
        }
        public ViewResult UsingExtensionMethods()
        {
            ShoppingCart cart = new ShoppingCart { Products = Product.GetProducts() };
            decimal cartTotal = cart.TotalPrices();
            return View("Index", new string[] { $"Total: {cartTotal:C2}" });
        }
        public ViewResult UsingExtensionMethodsWithInterface()
        {
            ShoppingCart cart = new ShoppingCart { Products = Product.GetProducts() };

            decimal cartTotal = cart.TotalPrices();
            decimal arrayTotal = productArray.TotalPrices();
            return View("Index", new string[] {
                $"Total card: {cartTotal}",
                $"Total array: {arrayTotal}"
            });
        }
        public ViewResult UsingExtensionMethodsFiltering()
        {

            decimal arrayTotal = productArray.FilterByPirce(35).TotalPrices();

            List<Product> products = new List<Product>();
            products.AddRange(productArray.FilterByPirce(35));

            return View("Index", new string[] { $"Total: {arrayTotal:C2}" });
        }
        public ViewResult UsingLambdaExpressions()
        {
            decimal priceFilterTotal = productArray.FilterByPirce(20).TotalPrices();
            decimal nameFilterTotal = productArray.FilterByName('S').TotalPrices();

            return View("Index", new string[]
            {
                $"Price Total: {priceFilterTotal:C2}",
                $"Name Total: {nameFilterTotal:C2}"
            });
        }
        private bool FilterByPirce(Product product)
        {
            return (product?.Price ?? 0) >= 20;
        }
        // не удобно и не практично! Громоздко 
        public ViewResult UsingDefiningFunctions()
        {
            Func<Product, bool> nameFilter = delegate (Product product)
            {
                return product?.Name?[0] == 'S';
            };
            decimal priceFilterTotal = productArray.Filter(FilterByPirce)
                .TotalPrices();
            decimal nameFilterTotal = productArray.Filter(nameFilter)
                .TotalPrices();
            return View("Index", new string[]
           {
                $"Price Total: {priceFilterTotal:C2}",
                $"Name Total: {nameFilterTotal:C2}"
           });
        }
        // лучше делать так 
        public ViewResult UsingDefiningFunctionsWithLambda()
        {

            decimal priceFilterTotal = productArray.Filter(p => (p?.Price ?? 0) >= 20)
                .TotalPrices();
            decimal nameFilterTotal = productArray.Filter(p => p?.Name?[0] == 'S')
                .TotalPrices();
            return View("Index", new string[]
           {
                $"Price Total: {priceFilterTotal:C2}",
                $"Name Total: {nameFilterTotal:C2}"
           });
        }
    }
}
