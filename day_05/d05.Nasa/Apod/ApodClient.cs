using System.Net;
using System.Text.Json;
using d05.Nasa.Apod.Models;

namespace d05.Nasa.Apod
{
    public class ApodClient : INasaClient<int, Task<MediaofToday[]>>
    {
        public string? ApiKey { get; set; }

        public async Task<MediaofToday[]> GetAsync(int count)
        {
            try
            {
                HttpClient httpClient = new();
                string url = $"https://api.nasa.gov/planetary/apod?api_key={ApiKey}&count={count}";
                var response = await httpClient.GetAsync(url);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var mediaofTodayList = JsonSerializer.Deserialize<List<MediaofToday>>(content);
                    return mediaofTodayList!.ToArray();
                }
                else
                {
                    Console.WriteLine($"GET \"{url}\" returned{response.StatusCode}:\n{await response.Content.ReadAsStringAsync()}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null!;
        }
    }
}

