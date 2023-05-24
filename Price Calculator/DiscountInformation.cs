using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Price_Calculator
{
    public class DiscountInformation
    {

        public static decimal Discount { get; set; }
        public static UPCDiscount UPCDiscount { get; set; }
        public static Cap Cap { get; set; }

        public static void SetCap(decimal capValue, bool isPercentage = false)
        {
            Cap = new Cap(capValue, isPercentage);

        }

        public static void AddUPCDiscount(int discountedUPC, decimal uPCDiscountPercentage)
        {
            UPCDiscount = new UPCDiscount(uPCDiscountPercentage, discountedUPC);
        }

        
    }
}
