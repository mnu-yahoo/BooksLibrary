using BooksLibrary.Data;
using Microsoft.AspNetCore.Components;
using System.IO;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace BooksLibrary.Services
{
    public class BooksService
    {
        private List<Book>? books;

        private HttpClient http { get; set; }

        public BooksService(HttpClient httpClient) 
        {
            http = httpClient;
        }

        public async Task<List<Book>> GetBooks()
        {
            var result = await http.GetFromJsonAsync<List<Book>>("sample-data/books.json");
            return result;
        }

        public async Task<List<Book>> GetBooks(string filter, string searchContext)
        {
            var result = await http.GetFromJsonAsync<List<Book>>("sample-data/books.json");
            if (result != null)
            {
                var query = GetQuery(searchContext, result, filter);
                if (query.Any())
                    result = query.ToList();
                else
                    result = new List<Book>();
            }

            return result;
        }

        private IEnumerable<Book> GetQuery(string searchContext, List<Book> books, string filter)
        {
            var result = books.Where(x =>  x.Title.ToUpper().Contains(filter.ToUpper()));
            if (searchContext == "subject")
                result = books.Where(x => x.Subject.ToUpper().Contains(filter.ToUpper()));
            else if (searchContext == "publisher")
                result = books.Where(x => x.Publisher.ToUpper().Contains(filter.ToUpper()));

            return result;
        }
    }
}
