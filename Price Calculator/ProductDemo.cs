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
            Product product1 = new Product("The Little Prince", 12345, 20.25m);
            Console.WriteLine(product1.ToString());
            product1.ProductWithFlatRateTax();
            product1.ProductWithCalculatedTax(21);
            Console.WriteLine("");

            // Requerement 2
            Console.WriteLine("Requerement 2 ");
            product1.ProductWithCalculatedDiscount(15);
            product1.ProductWithCalculatedTaxAndDiscount(23,20);
            Console.WriteLine("");


            // Requerement 3
            Console.WriteLine("Requerement 3 ");
            product1.Reoprt();
            Console.WriteLine("");
            Product.Discount = 15;
            product1.Reoprt();
            Console.WriteLine("");

            // Requerement 4
            Console.WriteLine("Requerement 4 ");
            Product.AddUPCDiscount(12345, 7);
            product1.Reoprt();

            Product product2 = new Product("C# Concepts", 777, 20.25m);
            Product.Tax = 21;
            product2.Reoprt();


        }
    }
}
