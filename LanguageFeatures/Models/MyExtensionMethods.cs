using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageFeatures.Models
{
    public static class MyExtensionMethods
    {
        public static decimal TotalPrices(this IEnumerable<Product> products)
        {
            decimal total = 0;
            foreach (Product product in products)
            {
                total += product?.Price ?? 0;
            }
            return total;
        }
        public static IEnumerable<Product> FilterByPirce(this IEnumerable<Product> productEnum, 
            decimal minimumPrice)
        {
            foreach (Product product in productEnum)
            {
                if ((product?.Price ?? 0) >= minimumPrice)
                    yield return product;
            }

        }
    }
}
