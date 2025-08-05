using Core.ApiClient.Models.Books;
using RestSharp;

namespace Core.ApiClient.Endpoints.Books
{
    public class TakeBookEndpoint
    {
        private readonly ApiClient _apiClient;
        private const string BaseResource = "/getbook";

        public TakeBookEndpoint(ApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public RestResponse<TakeBookListResponse> TakeBook(TakeBookRequest takeBookRequest)
        {
            var request = _apiClient.CreateJsonRequest(takeBookRequest, Method.Post, BaseResource);
            return _apiClient.Execute<TakeBookListResponse>(request);
        }

        public RestResponse<TakeBookListResponse> GetAllTakenBooks()
        {
            var request = _apiClient.CreateRequest(Method.Get, BaseResource);
            return _apiClient.Execute<TakeBookListResponse>(request);
        }

        public RestResponse ReturnBook(int takenBookId)
        {
            var request = _apiClient.CreateRequest(Method.Delete, $"{BaseResource}/{takenBookId}");
            return _apiClient.Execute(request);
        }
    }
} 