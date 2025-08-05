using Newtonsoft.Json;

namespace Core.ApiClient.Models.Books
{
    public class CreateBookRequest
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("author")]
        public string Author { get; set; }

        [JsonProperty("genre")]
        public string Genre { get; set; }

        [JsonProperty("quontity")]
        public int Quantity { get; set; }
    }

    public class UpdateBookRequest
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("author")]
        public string Author { get; set; }

        [JsonProperty("genre")]
        public string Genre { get; set; }

        [JsonProperty("quontity")]
        public int Quantity { get; set; }
    }
} 