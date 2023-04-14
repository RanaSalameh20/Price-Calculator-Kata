using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Price_Calculator
{
    public class Product
    {
        public string Name { get; set; }
        public int UPC { get; set; }

        private decimal _price;

        public decimal Price
        {
            get => _price;
            set => _price = decimal.Round(value, 2);
        }

        public decimal TAX { get; set; }

        public Product(string name, int uPC, decimal price)
        {
            Name = name;
            UPC = uPC;
            Price = price;
            TAX = 20;
        }

        public Product(string name, int uPC, decimal price, decimal tAX)
        {
            Name = name;
            UPC = uPC;
            Price = price;
            TAX = tAX;
        }

        public override string ToString()
        {
            return $"Product: {Name}, (UPC: {UPC}) , Base price: {Price:C2}";
        }

        public decimal PriceWithTax(decimal taxPercentage)
        {

            decimal taxAmount = Price * (taxPercentage / 100);
            decimal totalPrice = Price + taxAmount;
            return decimal.Round(totalPrice, 2);

        }

        public void ProductWithFlatRateTax()
        {
            ProductWithCalculatedTax(TAX);
        }
        public void ProductWithCalculatedTax(decimal taxPercentage)
        {
            decimal totalPrice = PriceWithTax(taxPercentage);
            Console.WriteLine($" Product price reported as ${Price }  before tax " +
                         $"and ${totalPrice} after {taxPercentage}% tax.");
        }

    }
}
