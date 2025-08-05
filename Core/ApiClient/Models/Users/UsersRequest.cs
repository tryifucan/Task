using Newtonsoft.Json;

namespace Core.ApiClient.Models.Users
{
    public class CreateUserRequest
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class UpdateUserRequest
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class UserValidationError
    {
        [JsonProperty("name")]
        public string[] Name { get; set; }
    }
}
