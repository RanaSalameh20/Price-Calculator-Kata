using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Price_Calculator
{
    public class UPCDiscount
    {
        public decimal Value { get; set; }  // Percentage
        public int Code { get; set; }

        public UPCDiscount()
        {

        }
        public UPCDiscount(decimal value, int code)
        {
            Value = value;
            Code = code;
        }
    }
}
