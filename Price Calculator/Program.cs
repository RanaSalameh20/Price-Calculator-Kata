using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Price_Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            //// Requerement 1 
            Console.WriteLine("Requerement 1 ");
            Product product1 = new Product("The Little Prince", 12345, 20.25m);
            Console.WriteLine(product1.ToString());
            product1.ProductWithFlatRateTax();
            product1.ProductWithCalculatedTax(21);
            Console.WriteLine("");

            //// Requerement 2
            Console.WriteLine("Requerement 2 ");
            product1.ProductWithCalculatedDiscount(15);
            product1.ProductWithCalculatedTaxAndDiscount(21, 15);
            Console.WriteLine("");


            //// Requerement 3
            Console.WriteLine("Requerement 3 ");
            product1.Reoprt();
            Console.WriteLine("");
            DiscountInformation.Discount = 15;
            product1.Reoprt();
            Console.WriteLine("");

            //// Requerement 4
            Console.WriteLine("Requerement 4 ");
            DiscountInformation.AddUPCDiscount(12345, 7);
            product1.Reoprt();

            Product product2 = new Product("C# Concepts", 777, 20.25m);
            Product.Tax = 21;
            Console.WriteLine(product1.ToString());
            product2.Reoprt();
            Console.WriteLine("");


            //// Requerement 5
            Console.WriteLine("Requerement 5 ");
            Product product3 = new Product("The Little Prince", 12345, 20.25m);

            Product.Tax = 20;
            DiscountInformation.Discount = 15;
            DiscountInformation.AddUPCDiscount(12345, 7);
            product3.Reoprt();
            Console.WriteLine("");
            product3.Reoprt(DiscountInformation.DiscountType.BeforeTax);
            Console.WriteLine("");

            // Requerement 6 & 7 & 8
            Product product4 = new Product("The Little Prince", 12345, 20.25m);
            Console.WriteLine("Requerement 6 ");
            Product.Tax = 21;
            DiscountInformation.Discount = 15;
            DiscountInformation.AddUPCDiscount(12345, 7);
            product4.AddExpense("Packaging", 1, true);
            product4.AddExpense("Transport", 2.2m);
            product4.ReoprtWithCosts();
            Console.WriteLine("");

            Console.WriteLine("Requerement 7 ");
            product4.ReoprtWithCosts(DiscountInformation.DiscountMethod.Multiplicative);
            Console.WriteLine("");


            Console.WriteLine("Requerement 8 ");
            DiscountInformation.SetCap(30 , true);
            product4.ReoprtWithCosts();


        }
    }
}
