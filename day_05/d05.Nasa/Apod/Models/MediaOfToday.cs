using System;
using System.Text.Json.Serialization;
namespace d05.Nasa.Apod.Models
{
    public class MediaofToday
    {
        [JsonPropertyName("copyright")]
        public string? Copyright { get; set; }

        [JsonPropertyName("date")]
        public string? Date { get; set; }
        [JsonPropertyName("explanation")]
        public string? Explanation { get; set; }
        public string? Hdurl { get; set; }
        public string? Media_type { get; set; }
        public string? Service_version { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("url")]
        public string? Url { get; set; }

        public override string ToString() => !String.IsNullOrEmpty(Copyright) ? $"{Date}\n'{Title}' by {Copyright}\n{Explanation}\n{Url}\n"
        : $"{Date}\n'{Title}'\n{Explanation}\n{Url}\n";
    }
}

