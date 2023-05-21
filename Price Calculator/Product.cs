using System;
using System.Collections.Generic;
using static Price_Calculator.DiscountInformation;


namespace Price_Calculator
{
    public class Product
    {
        public string Name { get; set; }
        public int UPC { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; } = "USD";
        public static decimal Tax { get; set; } = 20;
        public List<Expense> Expenses { get; set; } = new List<Expense>();

        public Product(string name, int uPC, decimal price)
        {
            Name = name;
            UPC = uPC;
            Price = price;
        }
        public Product(string name, int uPC, decimal price, string currency)
        {
            Name = name;
            UPC = uPC;
            Price = price;
            Currency = currency;
        }

        public override string ToString()
        {
            return $"Product: {Name}, (UPC: {UPC}) , Base price: {Price:C2}";
        }
   
        public void CalculateProducPricetWithFlatRateTax()
        {
            CalculateProductPriceWithTax(Tax); // This TAX Value for All Product
        }
        public void CalculateProductPriceWithTax(decimal taxPercentage)
        {
            decimal taxAmount = CalculatePercentageAmountFromPrice(taxPercentage);
            decimal totalPrice = Price + taxAmount;
            totalPrice = decimal.Round(totalPrice, 2);
            DisplayProductPriceBeforeTaxAndDiscount();
            DisplayProductPriceAfterTax(totalPrice, taxPercentage);
        }

        private void DisplayProductPriceBeforeTaxAndDiscount()
        {
            Console.WriteLine($"Product price reported as {Price} {Currency} before tax and Discount.");
        }

        private void DisplayProductPriceAfterTax(decimal totalPrice, decimal taxPercentage)
        {
            Console.WriteLine($"Product price reported as {totalPrice} {Currency} after {taxPercentage}% tax.");
        }

        public void CalculateProductPriceWithFlatRateTaxAndDiscount()
        {
            CalculateProductPriceWithCalculatedTaxAndDiscount(Tax, Discount);  // these Values for All Products
        }
        public void CalculateProducPricetWithCalculatedDiscount(decimal discountPercentage)
        {
            CalculateProductPriceWithCalculatedTaxAndDiscount(Tax, discountPercentage);  // flat tax but specific discount
        }
        public void CalculateProductPriceWithCalculatedTaxAndDiscount(decimal taxPercentage, decimal discountPercentage)
        {
            decimal taxAmount = CalculatePercentageAmountFromPrice(taxPercentage);
            decimal discountAmount = CalculatePercentageAmountFromPrice(discountPercentage);
            decimal totalPrice = Price + taxAmount - discountAmount;
            totalPrice = decimal.Round(totalPrice, 2);
            DisplayProductPriceBeforeTaxAndDiscount();
            DisplayProductPriceAfterTaxAndDiscount(totalPrice , taxPercentage, discountPercentage);
        }

        private decimal CalculatePercentageAmountFromPrice(decimal percentage)
        {
            var amount = Price * (percentage / 100);
            return Math.Floor(amount * 10000) / 10000;

        }

        private void DisplayProductPriceAfterTaxAndDiscount(decimal totalPrice , decimal taxPercentage, decimal discountPercentage)
        {
            Console.WriteLine($"Product price reported as {totalPrice} {Currency} after {taxPercentage}% tax and {discountPercentage}% discount.");
        }

        public void Reoprt(DiscountType discountType = DiscountType.AfterTax)
        {

            if (Discount == 0)
            {
                CalculateProductPriceWithTax(Tax);
                return;
            }

            if (UPC == DiscountedUPC && UPCDiscount > 0)
            {
                if (discountType == DiscountType.AfterTax)
                {
                    CalculateProductPriceWithCalculatedTaxAndDiscount(Tax, Discount + UPCDiscount);
                }
                else if (discountType == DiscountType.BeforeTax)
                {
                    CalculateProductPricwithUPCDiscountBeforeTax();
                }
            }
            else
            {
                CalculateProductPriceWithCalculatedTaxAndDiscount(Tax, Discount);
                
            }
            var totalDiscountAmount = CalculateTotalDiscountAmount();
            DisplyTotalDiscountAmount(totalDiscountAmount);
        }

        private decimal CalculateTotalDiscountAmount()
        {
            decimal totalDiscountPercentage = Discount;
            if(UPC == DiscountedUPC)
            {
                totalDiscountPercentage += UPCDiscount;
            }
            
            decimal totalDiscountAmount = CalculatePercentageAmountFromPrice(totalDiscountPercentage);
            totalDiscountAmount = decimal.Round(totalDiscountAmount, 2);
            return totalDiscountAmount;
        }

        private void CalculateProductPricwithUPCDiscountBeforeTax()
        {
            decimal originalPrice = Price;
            decimal uPCDiscountAmount = CalculatePercentageAmountFromPrice(UPCDiscount);

            decimal discountedPrice = Price - uPCDiscountAmount;
            Price = discountedPrice;

            decimal taxAmount = CalculatePercentageAmountFromPrice(Tax);
            decimal universalDiscountAmount = CalculatePercentageAmountFromPrice(Discount);
            Price = originalPrice;

            decimal totalPrice = Price - uPCDiscountAmount + taxAmount - universalDiscountAmount;
            totalPrice = decimal.Round(totalPrice, 2);
       

            DisplayProductPriceBeforeTaxAndDiscount();
            DisplayProductPriceAfterTaxAndDiscount(totalPrice, Tax, Discount);
        }

        private void DisplyTotalDiscountAmount(decimal totalDiscountAmount)
        {
            Console.WriteLine($"The Total Discount Amount Is {totalDiscountAmount:0.00} {Currency}");
        }
        
        public void AddExpense(string description, decimal amount, bool isPercentage = false)
        {
            var expense = new Expense(description, amount, isPercentage);
            Expenses.Add(expense);
        }

        public void ReoprtWithCosts(DiscountMethod discountMethod = DiscountMethod.Additive)
        {
            decimal totalCost = Price;
            decimal taxAmount = CalculatePercentageAmountFromPrice(Tax);
            totalCost += taxAmount;

            
            decimal discountAmount1 = CalculatePercentageAmountFromPrice(Discount);
            decimal discountAmount2 = 0;

            if (UPC == DiscountedUPC && UPCDiscount > 0)
            {
                if (discountMethod == DiscountMethod.Multiplicative)
                {
                    discountAmount2 = (Price - discountAmount1) * UPCDiscount / 100;
                    discountAmount2 = decimal.Round(discountAmount2, 2);
                }
                else
                {
                     discountAmount2= CalculatePercentageAmountFromPrice(UPCDiscount);

                }

            }
            decimal totalDiscountAmount = discountAmount1 + discountAmount2;

            if(discountMethod == DiscountMethod.Additive && Cap > 0 )
            {
                decimal capAmount = DiscountInformation.Cap;
                if (DiscountInformation.IsCapPercentageValue)
                {
                    capAmount = CalculatePercentageAmountFromPrice(DiscountInformation.Cap);
                    capAmount = decimal.Round(capAmount, 2);
                }
                
                if(totalDiscountAmount > capAmount)
                {
                    totalDiscountAmount = capAmount;
                }

            }
            totalCost -= totalDiscountAmount;
            totalCost = CalculateExpenses(totalCost);

            PrintDetailedPriceInformation(taxAmount, totalDiscountAmount, totalCost);
        }

        private decimal CalculateExpenses(decimal totalCost)
        {
            foreach (var expense in Expenses)
            {
                decimal expenseAmount = expense.IsPercentage ? CalculatePercentageAmountFromPrice(expense.AmountValue) : expense.AmountValue;
                totalCost += expenseAmount;
            }

            return totalCost;
        }

        private void PrintDetailedPriceInformation(decimal taxAmount , decimal discountAmount , decimal totalCost)
        {
            Console.WriteLine($"Cost = {Price} {Currency}");
            Console.WriteLine($"Tax = {taxAmount} {Currency}");
            Console.WriteLine($"Discounts = {discountAmount} {Currency}");
            foreach (var expense in Expenses)
            {
                decimal expenseAmount = expense.IsPercentage ? CalculatePercentageAmountFromPrice(expense.AmountValue) : expense.AmountValue;
                expenseAmount = decimal.Round(expenseAmount, 2);
                Console.WriteLine($"{expense.Description} =  {expenseAmount} {Currency}");
            }
            totalCost = decimal.Round(totalCost, 2);
            Console.WriteLine($"TOTAl Costs = {totalCost} {Currency}");
          
            DisplyTotalDiscountAmount(discountAmount);
        }

        
    }
}
