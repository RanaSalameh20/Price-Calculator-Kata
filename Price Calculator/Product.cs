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


        private decimal CalculatePercentageAmountFromPrice(decimal percentage)
        {
            var amount = (Price * (percentage / 100));
            return decimal.Round(amount, 2);
        }
        public void ProductWithFlatRateTax()
        {
            ProductWithCalculatedTax(Tax); // This TAX Value for All Product
        }
        public void ProductWithCalculatedTax(decimal taxPercentage)
        {
            decimal totalPrice = AddPriceToPercentage(taxPercentage);
            Console.WriteLine($" Product price reported as {Price } {Currency}  before tax " +
                         $"and {totalPrice} {Currency} after {taxPercentage}% tax.");
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
            decimal pricewithTax = AddPriceToPercentage(taxPercentage);
            decimal priceWithDiscount = AddPriceToPercentage(discountPercentage);
            decimal totalPrice = pricewithTax - priceWithDiscount + Price;

            Disply(taxPercentage, discountPercentage, totalPrice);
        }


        private decimal AddPriceToPercentage(decimal percentage)
        {
            decimal amount = CalculatePercentageAmountFromPrice(percentage);
            decimal totalPrice = Price + amount;
            return decimal.Round(totalPrice, 2);

        }

        private void Disply(decimal taxPercentage, decimal discountPercentage, decimal totalPrice)
        {
            Console.WriteLine($" Product price reported as {Price } {Currency} before tax and discount " +
                         $"and {totalPrice} {Currency} after {taxPercentage}% tax and {discountPercentage}% discount.");
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
            decimal uPCDiscountAmount = CalculatePercentageAmountFromPrice(UPCDiscount);

            decimal discountedPrice = Price - uPCDiscountAmount;
            Price = discountedPrice;

            decimal taxAmount = CalculatePercentageAmountFromPrice(Tax);
            decimal universalDiscountAmount = CalculatePercentageAmountFromPrice(Discount);
            Price = originalPrice;
            decimal totalPrice = Price - uPCDiscountAmount + taxAmount - universalDiscountAmount;
            totalPrice = decimal.Round(totalPrice, 2);
            decimal totalDiscountAmount = uPCDiscountAmount + universalDiscountAmount;
            totalDiscountAmount = decimal.Round(totalDiscountAmount, 2);

            Disply(Tax, Discount + UPCDiscount, totalPrice);

            PrintTotalDiscountAmount(totalDiscountAmount);
        }

        private void PrintTotalDiscountAmount(decimal totalDiscountAmount)
        {
            Console.WriteLine($"The Total Discount Amount Is {totalDiscountAmount:0.00} {Currency}");
        }

        private void DisplyPriceWithUPCDiscountAfterTax(decimal uPCDiscount)
        {
            ProductWithCalculatedTaxAndDiscount(Tax, Discount + uPCDiscount);

            decimal totalDiscountAmount = CalculatePercentageAmountFromPrice(Discount + uPCDiscount);
            totalDiscountAmount = decimal.Round(totalDiscountAmount, 2);
            PrintTotalDiscountAmount(totalDiscountAmount);
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

            DisplyPriceInformation(taxAmount, totalDiscountAmount, totalCost);
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

        private void DisplyPriceInformation(decimal taxAmount , decimal discountAmount , decimal totalCost)
        {
            Console.WriteLine($"Cost = {Price} {Currency}");
            Console.WriteLine($"Tax = {taxAmount:0.00} {Currency}");
            Console.WriteLine($"Discounts = {discountAmount:0.00} {Currency}");
            foreach (var expense in Expenses)
            {
                decimal expenseAmount = expense.IsPercentage ? CalculatePercentageAmountFromPrice(expense.AmountValue) : expense.AmountValue;
                Console.WriteLine($"{expense.Description} =  {expenseAmount:0.00} {Currency}");
            }
            Console.WriteLine($"TOTAl Costs = {totalCost:0.00} {Currency}");
          
            PrintTotalDiscountAmount(discountAmount);
        }

        
    }
}
