namespace d02_ex00.Models
{
    public class ExchangeRate
    {
        public struct RateData
        {
            public string from { get; set; }
            public string to { get; set; }
            public double rate { get; set; }
        }

        private string? pathToFolder { get; set; }

        List<string> rubRates = new();
        List<string> usdRates = new();
        List<string> eurRates = new();

        public ExchangeRate(string path)
        {
            pathToFolder = path;
            ParseRates();
        }

        private void ParseRates()
        {
            rubRates = ParsePath(pathToFolder + "/RUB.txt");
            usdRates = ParsePath(pathToFolder + "/USD.txt");
            eurRates = ParsePath(pathToFolder + "/EUR.txt");
        }

        private List<string> ParsePath(string path)
        {
            List<string> list = new();

            try
            {
                using StreamReader reader = new(path);
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    list.Add(line);
                }
            }
            catch (FileNotFoundException)
            {
                throw new Exception("File not exist");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while reading file: {ex.Message}");
            }

            return list;
        }

        public RateData GetRate(string from, string to)
        {
            RateData data = new();
            data.from = from;
            data.to = to;
            if (from is "RUB")
            {
                string? rubRateStr = rubRates.FirstOrDefault(str => str.StartsWith(to));
                if (rubRateStr is not null)
                {
                    rubRateStr = rubRateStr.Substring(rubRateStr.IndexOf(":") + 1);
                    rubRateStr = rubRateStr.Replace(',', '.');
                    data.rate = double.Parse(rubRateStr);
                }
            }
            else if (from is "USD")
            {
                string? usdRateStr = usdRates.FirstOrDefault(str => str.StartsWith(to));
                if (usdRateStr is not null)
                {
                    usdRateStr = usdRateStr.Substring(usdRateStr.IndexOf(":") + 1);
                    usdRateStr = usdRateStr.Replace(',', '.');
                    data.rate = double.Parse(usdRateStr);
                }
            }
            else if (from is "EUR")
            {
                string? eurRateStr = eurRates.FirstOrDefault(str => str.StartsWith(to));
                if (eurRateStr is not null)
                {
                    eurRateStr = eurRateStr.Substring(eurRateStr.IndexOf(":") + 1);
                    eurRateStr = eurRateStr.Replace(',', '.');
                    data.rate = double.Parse(eurRateStr);
                }
            }
            return data;
        }
    }
}

