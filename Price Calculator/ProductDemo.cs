using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Price_Calculator
{
    class ProductDemo
    {
        static void Main(string[] args)
        {
            // Requerement 1 
            Product p1 = new Product("The Little Prince", 123, 20.25m);
            Console.WriteLine(p1.ToString());
            p1.ProductWithFlatRateTax();
            p1.ProductWithCalculatedTax(21);

            // Requerement 2
            p1.ProductWithCalculatedDiscount(15);
            p1.ProductWithCalculatedTaxAndDiscount(23,20);

            // Requerement 3

        }
    }
}
