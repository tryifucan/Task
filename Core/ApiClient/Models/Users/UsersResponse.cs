using Newtonsoft.Json;

namespace Core.ApiClient.Models.Users
{
    public class UserResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("booksTaken")]
        public List<BookTaken> BooksTaken { get; set; } = new List<BookTaken>();
    }

    public class BookTaken
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
    }

    public class UsersListResponse : List<UserResponse>
    {}

    public class ApiErrorResponse
    {
        [JsonProperty("error")]
        public string Error { get; set; }

        [JsonProperty("id")]
        public string[] Id { get; set; }

        [JsonProperty("name")]
        public string[] Name { get; set; }
    }
}
