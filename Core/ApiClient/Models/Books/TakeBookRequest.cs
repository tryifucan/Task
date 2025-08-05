using Newtonsoft.Json;

namespace Core.ApiClient.Models.Books
{
    public class TakeBookRequest
    {
        [JsonProperty("userid")]
        public int UserId { get; set; }

        [JsonProperty("bookid")]
        public int BookId { get; set; }
    }

    public class TakeBookResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("userId")]
        public int UserId { get; set; }

        [JsonProperty("bookId")]
        public int BookId { get; set; }

        [JsonProperty("dateTaken")]
        public string DateTaken { get; set; }
    }

    public class TakeBookListResponse : List<TakeBookResponse> { }

    public class TakeBookValidationError
    {
        [JsonProperty("userid")]
        public string[] UserId { get; set; }

        [JsonProperty("bookid")]
        public string[] BookId { get; set; }
    }
} 