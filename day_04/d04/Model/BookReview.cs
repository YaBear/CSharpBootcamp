using System;
namespace d04.Model
{
    public class BookDetail
    {
        public string? title { get; set; }
        public string? description { get; set; }
        public string? author { get; set; }
    }

    public class BookResult : ISearchable
    {
        public string? list_name { get; set; }
        public int rank { get; set; }
        public string? amazon_product_url { get; set; }
        public List<BookDetail> book_details { get; set; } = new();
        public string Title => book_details.Count > 0 ? book_details[0].title ?? string.Empty : string.Empty;
        public override string ToString()
        {
            return $"- {book_details[0].title} by {book_details[0].author} [{rank} on NYT's {list_name}]\n{book_details[0].description}\n{amazon_product_url}";
        }
    }

    public class BookReview
	{
        public List<BookResult>? results { get; set; } = new();
        public BookReview()
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

