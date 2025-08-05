using Newtonsoft.Json;

namespace Core.ApiClient.Models.Books
{
    public class BookResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("author")]
        public string Author { get; set; }

        [JsonProperty("genre")]
        public string Genre { get; set; }

        [JsonProperty("quontity")]
        public long Quantity { get; set; }

        [JsonProperty("booksTaken")]
        public List<object> BooksTaken { get; set; } = new List<object>();
    }

    public class BooksListResponse : List<BookResponse>
    {
    }
} 