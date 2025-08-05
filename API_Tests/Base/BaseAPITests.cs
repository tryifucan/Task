using Core.ApiClient;
using Core.ApiClient.Endpoints.Books;
using Core.ApiClient.Endpoints.Users;
using Core.ApiClient.Models.Books;
using Core.ApiClient.Models.Users;
using Core.Utils;
using RestSharp;

namespace API_Tests.Base
{
    public abstract class BaseAPITests
    {
        protected static ApiClient ApiClient;
        protected static UsersEndpoint UsersEndpoint;
        protected static BooksEndpoint BooksEndpoint;
        protected static TakeBookEndpoint TakeBookEndpoint;

        [OneTimeSetUp]
        public static void OneTimeSetUp()
        {
            ApiClient = new ApiClient();
            UsersEndpoint = new UsersEndpoint(ApiClient);
            BooksEndpoint = new BooksEndpoint(ApiClient);
            TakeBookEndpoint = new TakeBookEndpoint(ApiClient);
        }

        [OneTimeTearDown]
        public static void OneTimeTearDown()
        {
            ApiClient?.Dispose();
        }

        protected CreateUserRequest GenerateValidUserRequest()
        {
            return new CreateUserRequest
            {
                Name = Generator.GenerateRandomName()
            };
        }

        protected UpdateUserRequest GenerateValidUpdateUserRequest()
        {
            return new UpdateUserRequest
            {
                Name = Generator.GenerateRandomName()
            };
        }

        protected CreateBookRequest GenerateValidBookRequest()
        {
            return new CreateBookRequest
            {
                Name = Generator.GenerateRandomBookName(),
                Author = Generator.GenerateRandomAuthor(),
                Genre = Generator.GenerateRandomGenre(),
                Quantity = Generator.GenerateRandomQuantity()
            };
        }

        protected UpdateBookRequest GenerateValidUpdateBookRequest()
        {
            return new UpdateBookRequest
            {
                Name = Generator.GenerateRandomBookName(),
                Author = Generator.GenerateRandomAuthor(),
                Genre = Generator.GenerateRandomGenre(),
                Quantity = Generator.GenerateRandomQuantity()
            };
        }

        protected void AssertSuccessfulResponse(RestResponse response, string operation = "API call")
        {
            Assert.That(response, Is.Not.Null, $"Response should not be null for {operation}");
            Assert.That(response.IsSuccessful, Is.True, $"Operation {operation} should succeed");
        }

        protected UserResponse HandleUserCreationResponse(RestResponse createResponse, string expectedUserName)
        {
            AssertSuccessfulResponse(createResponse, "Create user");

            var singleUser = ApiClient.DeserializeResponse<UserResponse>(createResponse);
            if (singleUser != null && singleUser.Name == expectedUserName)
            {
                return singleUser;
            }

            var usersList = ApiClient.DeserializeResponse<UsersListResponse>(createResponse);

            Assert.That(usersList, Is.Not.Null.And.Not.Empty, "Should return created user in list");

            var createdUser = usersList.FirstOrDefault(u => u.Name == expectedUserName);
            if (createdUser != null)
            {
                return createdUser;
            }

            var firstUser = usersList.First();

            return firstUser;
        }

        protected BookResponse HandleBookCreationResponse(RestResponse createResponse, string expectedBookName)
        {
            AssertSuccessfulResponse(createResponse, "Create book");
            
            var booksList = ApiClient.DeserializeResponse<BooksListResponse>(createResponse);

            Assert.That(booksList, Is.Not.Null.And.Not.Empty, "Should return created book in list");
            
            var createdBook = booksList.FirstOrDefault(b => b.Name == expectedBookName);

            Assert.That(createdBook, Is.Not.Null, $"Should find book with name '{expectedBookName}' in the response");
            
            return createdBook;
        }

        protected void CleanupTestUser(int userId)
        {
            var response = UsersEndpoint.DeleteUser(userId);
        }

        protected void CleanupTestBook(int bookId)
        {

            var response = BooksEndpoint.DeleteBook(bookId);
        }
    }
}
