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
            Console.WriteLine("Requerement 1 ");
            Product p1 = new Product("The Little Prince", 123, 20.25m);
            Console.WriteLine(p1.ToString());
            p1.ProductWithFlatRateTax();
            p1.ProductWithCalculatedTax(21);
            Console.WriteLine("");

            // Requerement 2
            Console.WriteLine("Requerement 2 ");
            p1.ProductWithCalculatedDiscount(15);
            p1.ProductWithCalculatedTaxAndDiscount(23,20);
            Console.WriteLine("");


            // Requerement 3
            Console.WriteLine("Requerement 3 ");
            p1.Reoprt();
            Console.WriteLine("");
            Product.Discount = 15;
            p1.Reoprt();
            Console.WriteLine("");


        }
    }
}
