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
    }
}
