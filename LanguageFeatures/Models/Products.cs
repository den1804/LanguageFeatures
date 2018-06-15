using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageFeatures.Models
{
    public class Product
    {
        public string Name { get; set; }
        // Using Auto-Implemented Property Initializers
        public string Category { get; set; } = "Watersports";
        public decimal? Price { get; set; }
        public Product Related { get; set; }
        // Creating Read-Only Automatically Implemented Properties
        // Данное свойство не возможно изменить, разве что только в конструкторе
        public bool InStock { get; } = true;  

        public static Product[] GetProducts()
        {
            Product kayak = new Product
            {
                Name = "Kayak",
                Price = 275M, 
                Category = "Water Craft"
            };
            Product lifejacket = new Product
            {
                Name = "Lifejacket",
                Price = 48.95M
            };
            kayak.Related = lifejacket;
            return new Product[] { kayak, lifejacket, null };
        }
    }
}
