using d04.Model;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
namespace d04
{
    public static class SearchExtensions
    {
        public static IEnumerable<T> Search<T>(this IEnumerable<T> list, string search)
            where T : ISearchable
        {
            var query = list.Where(item => item.Title != null &&
                                           item.Title.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0);

            return query;
        }

        public static void DisplaySearchResults<T>(this IEnumerable<T> results, string mediaType)
        {
            Console.WriteLine($"{mediaType} search result [{results.Count()}]:");
            foreach (var result in results)
            {
                Console.WriteLine(result);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .Build();
            string? filePathBooks = configuration["FilePaths:BookReviews"];
            string? filePathMovies = configuration["FilePaths:MovieReviews"];
            BookReview? BookReviews = new();
            MovieReview? MovieReviews = new();
            try
            {
                if (string.IsNullOrWhiteSpace(filePathBooks))
                {
                    throw new ArgumentNullException(nameof(filePathBooks), "File path cannot be null or empty.");
                }

                if (!File.Exists(filePathBooks))
                {
                    throw new FileNotFoundException($"The file '{filePathBooks}' does not exist.");
                }
                string jsonBooks = File.ReadAllText(filePathBooks);
                BookReviews = JsonSerializer.Deserialize<BookReview>(jsonBooks);
                if (string.IsNullOrWhiteSpace(filePathMovies))
                {
                    throw new ArgumentNullException(nameof(filePathMovies), "File path cannot be null or empty.");
                }

                if (!File.Exists(filePathMovies))
                {
                    throw new FileNotFoundException($"The file '{filePathMovies}' does not exist.");
                }
                string jsonMovies = File.ReadAllText(filePathMovies);
                MovieReviews = JsonSerializer.Deserialize<MovieReview>(jsonMovies);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return;
            }
            if (args.Length > 0 && args[0].ToLower() == "best")
            {
                var bestBook = BookReviews?.results?.OrderBy(result => result.rank).FirstOrDefault();
                if (bestBook != null)
                {
                    Console.WriteLine("Best in books:");
                    Console.WriteLine(bestBook);
                }
                else
                {
                    Console.WriteLine("No best book found.");
                }

                var bestMovie = MovieReviews?.results?.FirstOrDefault(result => result.critics_pick == 1);
                if (bestMovie != null)
                {
                    Console.WriteLine("\nBest in movie reviews:");
                    Console.WriteLine(bestMovie);
                }
                else
                {
                    Console.WriteLine("\nNo best movie review found.");
                }
            }
            else
            {
                Console.Write("Input search text: ");
                string? searchTerm = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    var bookSearchResults = BookReviews?.results?.Search(searchTerm) ?? Enumerable.Empty<BookResult>();
                    var movieSearchResults = MovieReviews?.results?.Search(searchTerm) ?? Enumerable.Empty<MovieResult>();
                    Console.WriteLine($"\nItems found: {bookSearchResults.Count() + movieSearchResults.Count()}\n");
                    bookSearchResults.DisplaySearchResults("Book");
                    Console.Write("\n");
                    movieSearchResults.DisplaySearchResults("Movie");
                }
            }
        }
    }
}