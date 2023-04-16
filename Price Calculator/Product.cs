using System;
using System.Collections.Generic;

namespace Price_Calculator
{
    public class Product
    {
        public enum DiscountType
        {
            AfterTax,
            BeforeTax
        }

        public string Name { get; set; }
        public int UPC { get; set; }

        public decimal Price { get; set; }
        

        public static decimal Tax { get; set; } = 20;
        public static decimal Discount { get; set; } = 0;

        private static decimal UPCDiscount { get; set; } = 0;  // Percentage 
        private static int DiscountedUPC { get; set; } = 0;  // applied for this UPC only

        public static List<Expense> Expenses { get; set; } = new List<Expense>();

        public Product(string name, int uPC, decimal price)
        {
            Name = name;
            UPC = uPC;
            Price = decimal.Round(price, 2);
        }


        public override string ToString()
        {
            return $"Product: {Name}, (UPC: {UPC}) , Base price: {Price:C2}";
        }

     
        private decimal Amount(decimal percentage)
        {
            return (Price * (percentage / 100));
        }
        public void ProductWithFlatRateTax()
        {
            ProductWithCalculatedTax(Tax); // This TAX Value for All Product
        }
        public void ProductWithCalculatedTax(decimal taxPercentage)
        {
            decimal totalPrice = PriceCalculation(taxPercentage);
            Console.WriteLine($" Product price reported as ${Price }  before tax " +
                         $"and ${totalPrice} after {taxPercentage}% tax.");
        }
        public void ProductWithFlatRateTaxAndDiscount()
        {
            ProductWithCalculatedTaxAndDiscount(Tax, Discount);  // these Values for All Products
        }
        public void ProductWithCalculatedDiscount(decimal discountPercentage)
        {
            ProductWithCalculatedTaxAndDiscount(Tax, discountPercentage);
        }
        public void ProductWithCalculatedTaxAndDiscount(decimal taxPercentage, decimal discountPercentage)
        {
            decimal pricewithTax = PriceCalculation(taxPercentage);
            decimal priceWithDiscount = PriceCalculation(discountPercentage);
            decimal totalPrice = pricewithTax - priceWithDiscount + Price;

            Disply(taxPercentage, discountPercentage, totalPrice);
        }


        private decimal PriceCalculation(decimal percentage)
        {
            decimal amount = Amount(percentage);
            decimal totalPrice = Price + amount;
            return decimal.Round(totalPrice, 2);

        }

        private void Disply(decimal taxPercentage, decimal discountPercentage, decimal totalPrice)
        {
            Console.WriteLine($" Product price reported as ${Price }  before tax and discount " +
                         $"and ${totalPrice} after {taxPercentage}% tax and {discountPercentage}% discount.");
        }
  
        public void Reoprt(DiscountType discountType = DiscountType.AfterTax)
        {

            if (Discount == 0)
            {
                ProductWithCalculatedTax(Tax);
                return;
            }


            if (UPC == DiscountedUPC && UPCDiscount > 0 && discountType == DiscountType.AfterTax)
            {
                DisplyPriceWithUPCDiscountAfterTax(UPCDiscount);
                return;

            }
            else if (UPC == DiscountedUPC && UPCDiscount > 0 && discountType == DiscountType.BeforeTax)
            {
                DisplyPricwithUPCDiscountBeforeTax();
                return;
            }

            DisplyPriceWithUPCDiscountAfterTax(0);

        }

        private void DisplyPricwithUPCDiscountBeforeTax()
        {
            decimal originalPrice = Price;
            decimal uPCDiscountAmount = Amount(UPCDiscount); // 1.42

            decimal discountedPrice = Price - uPCDiscountAmount;
            Price = discountedPrice;

            decimal taxAmount = Amount(Tax); // 3.77
            decimal universalDiscountAmount = Amount(Discount); // 2.82
            Price = originalPrice;
            decimal totalPrice = Price - uPCDiscountAmount + taxAmount - universalDiscountAmount;
            totalPrice = decimal.Round(totalPrice, 2);
            decimal totalDiscountAmount = uPCDiscountAmount + universalDiscountAmount;
            totalDiscountAmount = decimal.Round(totalDiscountAmount, 2);

            Console.WriteLine($" Product price reported as ${Price }  before tax and discounts " +
                     $"and ${totalPrice} after.");

            Console.WriteLine($" The total discount amount is ${totalDiscountAmount}");
        }

        private void DisplyPriceWithUPCDiscountAfterTax(decimal uPCDiscount)
        {
            ProductWithCalculatedTaxAndDiscount(Tax, Discount + uPCDiscount);

            decimal totalDiscountAmount = Amount(Discount + uPCDiscount);
            totalDiscountAmount = decimal.Round(totalDiscountAmount, 2);
            Console.WriteLine($" The total discount amount is ${totalDiscountAmount}");
        }

        public static void AddUPCDiscount(int discountedUPC, decimal uPCDiscountPercentage)
        {
            DiscountedUPC = discountedUPC;
            UPCDiscount = uPCDiscountPercentage;
        }

        public void AddExpense(string description, decimal amount, bool isPercentage = false)
        {
            var expense = new Expense(description, amount, isPercentage);
            Expenses.Add(expense);
        }

        public void ReoprtWithCosts()
        {
            decimal totalCost = Price;
            Console.WriteLine($"Cost = {Price}");
            decimal taxAmount = Amount(Tax);
            Console.WriteLine($"Tax = {taxAmount:C2}");
            totalCost += taxAmount;

            decimal discountAmount = 0;
            if (UPC == DiscountedUPC && UPCDiscount > 0)
            {
                discountAmount = Amount(Discount + UPCDiscount);
                Console.WriteLine($"Discounts = {discountAmount:C2}");
            }
            else if (Discount > 0)
            {
                discountAmount = Amount(Discount);
                Console.WriteLine($"Discounts = {discountAmount:C2}");
            }
            totalCost -= discountAmount;

            totalCost = CalculateExpenses(totalCost);

            Console.WriteLine($"TOTAl Costs = {totalCost:C2}");
            Console.WriteLine($"TOTAl Discounts = {discountAmount:C2}");
        }

        private decimal CalculateExpenses(decimal totalCost)
        {
            foreach (var expense in Expenses)
            {
                decimal expenseAmount = expense.IsPercentage ? Amount(expense.AmountValue) : expense.AmountValue;
                Console.WriteLine($"{expense.Description} =  {expenseAmount:C2}");
                totalCost += expenseAmount;
            }

            return totalCost;
        }
    }
}
