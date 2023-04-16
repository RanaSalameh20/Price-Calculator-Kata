namespace Price_Calculator
{
    public class Expense
    {
        public string Description { get; set; }
        public decimal AmountValue { get; set; }
        public bool IsPercentage { get; set; }

        public Expense(string description, decimal amountValue, bool isPercentage = false)
        {
            Description = description;
            AmountValue = decimal.Round(amountValue, 2);
            IsPercentage = isPercentage;
        }

       

        public string AmountDisplay()
        {
            if (IsPercentage)
            {
                return $"{AmountValue}%";
            }
            else
            {
                return $"{AmountValue:C2}";
            }
        }
    }
}