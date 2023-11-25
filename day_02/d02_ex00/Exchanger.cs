using d02_ex00.Models;

namespace d02_ex00
{
	public class Exchanger
    {
        private ExchangeSum exSum;
        private ExchangeRate exRate;

        private ExchangeRate.RateData firstRate = new();
        private ExchangeRate.RateData secondRate = new();
        private ExchangeSum.SumData sumData = new();

        public Exchanger(string currency, string path)
		{
            exSum = new(currency);
            sumData = exSum.GetData();
            exRate = new(path);
            ConvertCurrency();
        }

        public void ConvertCurrency()
        {
            if (sumData.id == "RUB")
            {
                firstRate = exRate.GetRate("RUB", "USD");
                secondRate = exRate.GetRate("RUB", "EUR");
            }
            else if (sumData.id == "USD")
            {
                firstRate = exRate.GetRate("USD", "EUR");
                secondRate = exRate.GetRate("USD", "RUB");
            }
            else if (sumData.id == "EUR")
            {
                firstRate = exRate.GetRate("EUR", "USD");
                secondRate = exRate.GetRate("EUR", "RUB");
            }
        }

        public override string ToString()
        {
            return $"{sumData}\nAmount in {firstRate.to}: {firstRate.rate * sumData.amount:F2} {firstRate.to}\nAmount in {secondRate.to}: {secondRate.rate * sumData.amount:F2} {secondRate.to}";
        }
    }
}

