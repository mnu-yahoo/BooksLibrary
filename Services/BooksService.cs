using BooksLibrary.Data;
using Microsoft.AspNetCore.Components;
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
    }
}
