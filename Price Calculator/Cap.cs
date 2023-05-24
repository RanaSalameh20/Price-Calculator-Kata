using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Price_Calculator
{
    public class Cap
    {
        public  decimal Value { get; set; }
        public  bool IsCapPercentageValue { get; set; }

        public Cap(decimal value , bool isCapPercentageValue)
        {
            Value = value;
            IsCapPercentageValue = isCapPercentageValue;
        }
      
    }
}
