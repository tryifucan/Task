using Core.ApiClient.Models.Books;
using RestSharp;

namespace Core.ApiClient.Endpoints.Books
{
    public class BooksEndpoint
    {
        private readonly ApiClient _apiClient;
        private const string BaseResource = "/books";

        public BooksEndpoint(ApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public RestResponse<BooksListResponse> GetAllBooks()
        {
            var request = _apiClient.CreateRequest(Method.Get, BaseResource);
            return _apiClient.Execute<BooksListResponse>(request);
        }

        public RestResponse<BookResponse> GetBookById(int bookId)
        {
            var request = _apiClient.CreateRequest(Method.Get, $"{BaseResource}/{bookId}");
            return _apiClient.Execute<BookResponse>(request);
        }

        public RestResponse<BooksListResponse> CreateBook(CreateBookRequest createRequest)
        {
            var request = _apiClient.CreateJsonRequest(createRequest, Method.Post, BaseResource);
            return _apiClient.Execute<BooksListResponse>(request);
        }

        public RestResponse<BookResponse> UpdateBook(int bookId, UpdateBookRequest updateRequest)
        {
            var request = _apiClient.CreateJsonRequest(updateRequest, Method.Put, $"{BaseResource}/{bookId}");
            return _apiClient.Execute<BookResponse>(request);
        }

        public RestResponse DeleteBook(int bookId)
        {
            var request = _apiClient.CreateRequest(Method.Delete, $"{BaseResource}/{bookId}");
            return _apiClient.Execute(request);
        }
    }
} 