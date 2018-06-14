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
                string name = product?.Name;
                decimal? price = product?.Price;
                results.Add(String.Format("Name: {0}, Price: {1}", name, price));
            }
            return View(results);
        }
    }
}
