using Core.ApiClient.Models.Users;
using RestSharp;

namespace Core.ApiClient.Endpoints.Users
{
    public class UsersEndpoint
    {
        private readonly ApiClient _apiClient;
        private const string BaseResource = "/users";

        public UsersEndpoint(ApiClient apiClient)
        {
            _apiClient = apiClient;
        }
        public RestResponse<UserResponse> GetUserById(int userId)
        {
            var request = _apiClient.CreateRequest(Method.Get, $"{BaseResource}/{userId}");
            return _apiClient.Execute<UserResponse>(request);
        }

        public RestResponse<UsersListResponse> GetAllUsers()
        {
            var request = _apiClient.CreateRequest(Method.Get, BaseResource);
            return _apiClient.Execute<UsersListResponse>(request);
        }

        public RestResponse<UserResponse> CreateUser(CreateUserRequest createRequest)
        {
            var request = _apiClient.CreateJsonRequest(createRequest, Method.Post, BaseResource);
            return _apiClient.Execute<UserResponse>(request);
        }

        public RestResponse<UserResponse> UpdateUser(int userId, UpdateUserRequest updateRequest)
        {
            var request = _apiClient.CreateJsonRequest(updateRequest, Method.Put, $"{BaseResource}/{userId}");
            return _apiClient.Execute<UserResponse>(request);
        }

        public RestResponse DeleteUser(int userId)
        {
            var request = _apiClient.CreateRequest(Method.Delete, $"{BaseResource}/{userId}");
            return _apiClient.Execute(request);
        }
    }
}
