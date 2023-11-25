using System;
namespace d04.Model
{
    public class Link
    {
        public string? url { get; set; }
        public override string ToString()
        {
            return url is not null ? url : string.Empty;
        }
    }

    public class MovieResult : ISearchable
    {
        public string? title { get; set; }
        public int critics_pick { get; set; }
        public string? summary_short { get; set; }
        public Link? link { get; set; }
        public string Title => title ?? string.Empty;
        public override string ToString()
        {
            string output = string.Empty;
            output += $"- {title} ";
            if (critics_pick == 1)
            {
                output += "[NYT critic’s pick]";
            }
            output += $"\n{summary_short}\n{link}";
            return output;
        }
    }

    public class MovieReview
    {
        public List<MovieResult>? results { get; set; } = new();

        public MovieReview()
		{
		}

        public override string ToString()
        {
            string output = string.Empty;
            if (results is not null)
            {
                foreach (var item in results)
                {
                    output += item.ToString() + "\n";
                }
            }
            return output;
        }
    }
}

