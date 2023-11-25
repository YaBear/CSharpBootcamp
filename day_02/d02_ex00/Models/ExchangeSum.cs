namespace d02_ex00.Models
{
    public class ExchangeSum
	{
        public struct SumData
        {
            public double amount { get; set; }
            public string id { get; set; }

            public override string ToString()
            {
                return $"Amount in the original currency: {amount:F2} {id}";
            }
        }

        private string? input { get; set; }
        private string[] substringsToCheck = { "RUB", "EUR", "USD" };

        private ExchangeSum.SumData sumData = new();

        public ExchangeSum(string currency)
        {
            input = currency;
            ParseCurrency();
        }

        public SumData GetData()
        {
            return sumData;
        }

        private void ParseCurrency()
        {
            if (input is null)
            {
                throw new Exception("Input error. Check the input data and repeat the request.");
            }
            int index = input.IndexOf(" ");
            if (index != -1 && double.TryParse(input[..index], out double result))
            {
                sumData.amount = result;
            }
            else
            {
                throw new Exception("Input error. Check the input data and repeat the request.");
            }
            sumData.id = input[index..];
            sumData.id = sumData.id.Trim();
            if (!substringsToCheck.Contains(sumData.id))
            {
                throw new Exception("Input error. Check the input data and repeat the request.");
            }
        }
    }
}

