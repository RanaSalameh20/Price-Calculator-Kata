using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Price_Calculator
{
    public class DiscountInformation
    {

        public enum DiscountType
        {
            AfterTax,
            BeforeTax
        }
        public enum DiscountMethod
        {
            Additive,
            Multiplicative
        }
        public static decimal Discount { get; set; }
        public static decimal UPCDiscount { get; set; } = 0;  // Percentage 
        public static int DiscountedUPC { get; set; } = 0;  // applied for this UPC only

        public static decimal Cap { get; set; }
        public static bool IsCapPercentageValue { get; set; }

        public static void SetCap(decimal capValue, bool isPercentage = false)
        {
            Cap = capValue;
            IsCapPercentageValue = isPercentage;
            
        }

        public static void AddUPCDiscount(int discountedUPC, decimal uPCDiscountPercentage)
        {
            DiscountedUPC = discountedUPC;
            UPCDiscount = uPCDiscountPercentage;
        }

        
    }
}
