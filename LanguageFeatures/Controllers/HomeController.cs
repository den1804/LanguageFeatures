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
                results.Add(String.Format("Name: {0}, Price: {1}, Related: {2}", name, price, relatedName));
            }
            return View(results);
        }
    }
}
