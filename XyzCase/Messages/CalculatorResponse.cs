namespace XyzCase.Actors
{
    public class CalculatorResponse
    {
        public CalculatorResponse(string symbolName, decimal avgPrice)
        {
            Symbol = symbolName;
            AvgPrice = avgPrice;
        }

        public string Symbol { get; }
        public decimal AvgPrice { get; }
    }

}
