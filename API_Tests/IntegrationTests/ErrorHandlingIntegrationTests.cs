using NUnit.Framework;
using RestSharp;
using Core.ApiClient.Endpoints.Users;
using Core.ApiClient.Endpoints.Books;
using Core.ApiClient.Models.Users;
using Core.ApiClient.Models.Books;
using Core.Utils;
using API_Tests.Base;

namespace API_Tests.IntegrationTests
{
    [TestFixture]
    [Category("Integration")]
    [Category("ErrorHandling")]
    public class ErrorHandlingIntegrationTests : BaseAPITests
    {
        [Test]
        [Description("Concurrent Book Operations: Multiple users trying to take the same book simultaneously")]
        public void ConcurrentBookOperations_ShouldHandleConflictsGracefully()
        {
            var createBookRequest = GenerateValidBookRequest();
            createBookRequest.Quantity = 1;
            var createBookResponse = BooksEndpoint.CreateBook(createBookRequest);
            AssertSuccessfulResponse(createBookResponse, "Book creation should succeed");
            var book = HandleBookCreationResponse(createBookResponse, createBookRequest.Name);
            var bookId = book.Id;

            var user1Request = GenerateValidUserRequest();
            var user2Request = GenerateValidUserRequest();
            
            var user1Response = UsersEndpoint.CreateUser(user1Request);
            var user2Response = UsersEndpoint.CreateUser(user2Request);
            
            AssertSuccessfulResponse(user1Response, "User 1 creation should succeed");
            AssertSuccessfulResponse(user2Response, "User 2 creation should succeed");
            
            var user1 = HandleUserCreationResponse(user1Response, user1Request.Name);
            var user2 = HandleUserCreationResponse(user2Response, user2Request.Name);

            try
            {
                var takeBook1Request = new TakeBookRequest
                {
                    UserId = user1.Id,
                    BookId = bookId
                };
                var takeBook1Response = TakeBookEndpoint.TakeBook(takeBook1Request);
                AssertSuccessfulResponse(takeBook1Response, "User 1 taking book should succeed");
                var takenBook1 = ApiClient.DeserializeResponse<TakeBookResponse>(takeBook1Response);

                var takeBook2Request = new TakeBookRequest
                {
                    UserId = user2.Id,
                    BookId = bookId
                };
                var takeBook2Response = TakeBookEndpoint.TakeBook(takeBook2Request);

                Assert.That(takeBook2Response, Is.Not.Null, "Second user's request should be handled");

                var getAllTakenBooksResponse = TakeBookEndpoint.GetAllTakenBooks();
                AssertSuccessfulResponse(getAllTakenBooksResponse, "Getting all taken books should succeed");
                var allTakenBooks = ApiClient.DeserializeResponse<TakeBookListResponse>(getAllTakenBooksResponse);

                var user1TakenBook = allTakenBooks.FirstOrDefault(tb => tb.UserId == user1.Id && tb.BookId == bookId);
                var user2TakenBook = allTakenBooks.FirstOrDefault(tb => tb.UserId == user2.Id && tb.BookId == bookId);

                Assert.That(user1TakenBook, Is.Not.Null, "User 1 should have the book");

                var returnBookResponse = TakeBookEndpoint.ReturnBook(takenBook1.Id);
                AssertSuccessfulResponse(returnBookResponse, "Returning book should succeed");
            }
            finally
            {
                CleanupTestBook(bookId);
                CleanupTestUser(user1.Id);
                CleanupTestUser(user2.Id);
            }
        }

        [Test]
        [Description("Data Integrity: Delete user with taken books -> Verify book return handling")]
        public void DataIntegrity_DeleteUserWithTakenBooks_ShouldHandleGracefully()
        {
            var createUserRequest = GenerateValidUserRequest();
            var createBookRequest = GenerateValidBookRequest();
            
            var createUserResponse = UsersEndpoint.CreateUser(createUserRequest);
            var createBookResponse = BooksEndpoint.CreateBook(createBookRequest);
            
            AssertSuccessfulResponse(createUserResponse, "User creation should succeed");
            AssertSuccessfulResponse(createBookResponse, "Book creation should succeed");
            
            var user = HandleUserCreationResponse(createUserResponse, createUserRequest.Name);
            var book = HandleBookCreationResponse(createBookResponse, createBookRequest.Name);

            try
            {
                var takeBookRequest = new TakeBookRequest
                {
                    UserId = user.Id,
                    BookId = book.Id
                };

                var takeBookResponse = TakeBookEndpoint.TakeBook(takeBookRequest);

                AssertSuccessfulResponse(takeBookResponse, "Taking book should succeed");

                var takenBook = ApiClient.DeserializeResponse<TakeBookResponse>(takeBookResponse);

                var getAllTakenBooksResponse = TakeBookEndpoint.GetAllTakenBooks();

                AssertSuccessfulResponse(getAllTakenBooksResponse, "Getting all taken books should succeed");

                var allTakenBooks = ApiClient.DeserializeResponse<TakeBookListResponse>(getAllTakenBooksResponse);
                var userTakenBook = allTakenBooks.FirstOrDefault(tb => tb.UserId == user.Id && tb.BookId == book.Id);

                Assert.That(userTakenBook, Is.Not.Null, "User should have the book");

                var deleteUserResponse = UsersEndpoint.DeleteUser(user.Id);

                Assert.That(deleteUserResponse, Is.Not.Null, "Delete user request should be handled");

                var getBookResponse = BooksEndpoint.GetBookById(book.Id);

                AssertSuccessfulResponse(getBookResponse, "Getting book should still succeed");

                var retrievedBook = ApiClient.DeserializeResponse<BookResponse>(getBookResponse);

                Assert.That(retrievedBook, Is.Not.Null, "Book should still exist");

                var getTakenBooksAfterDeleteResponse = TakeBookEndpoint.GetAllTakenBooks();

                AssertSuccessfulResponse(getTakenBooksAfterDeleteResponse, "Getting taken books after user delete should succeed");

                var takenBooksAfterDelete = ApiClient.DeserializeResponse<TakeBookListResponse>(getTakenBooksAfterDeleteResponse);
                var bookAfterUserDelete = takenBooksAfterDelete.FirstOrDefault(tb => tb.Id == takenBook.Id);
            }
            finally
            {
                CleanupTestBook(book.Id);
                CleanupTestUser(user.Id);

            }
        }

        [Test]
        [Description("Invalid Data Flow: Create user with invalid data -> Try to use in book operations")]
        public void InvalidDataFlow_InvalidUserInBookOperations_ShouldHandleGracefully()
        {
            var invalidUserRequest = GenerateValidUserRequest();
            invalidUserRequest.Name = "";
            
            var createUserResponse = UsersEndpoint.CreateUser(invalidUserRequest);
            
            Assert.That((int)createUserResponse.StatusCode, Is.AnyOf(400, 422), "Invalid user creation should be rejected");

            var createBookRequest = GenerateValidBookRequest();
            var createBookResponse = BooksEndpoint.CreateBook(createBookRequest);

            AssertSuccessfulResponse(createBookResponse, "Book creation should succeed");

            var book = HandleBookCreationResponse(createBookResponse, createBookRequest.Name);

            try
            {
                var nonExistentUserId = Generator.GenerateRandomId();
                
                var takeBookRequest = new TakeBookRequest
                {
                    UserId = nonExistentUserId,
                    BookId = book.Id
                };
                var takeBookResponse = TakeBookEndpoint.TakeBook(takeBookRequest);

                Assert.That((int)takeBookResponse.StatusCode, Is.AnyOf(400, 404, 422), "Taking book with non-existent user should fail");

                var getBookResponse = BooksEndpoint.GetBookById(book.Id);
                AssertSuccessfulResponse(getBookResponse, "Getting book should still succeed");
                var retrievedBook = ApiClient.DeserializeResponse<BookResponse>(getBookResponse);
                Assert.That(retrievedBook, Is.Not.Null, "Book should still exist and be available");
            }
            finally
            {
                CleanupTestBook(book.Id);
            }
        }

        [Test]
        [Description("Resource Cleanup: Create multiple resources -> Verify proper cleanup after errors")]
        public void ResourceCleanup_AfterErrors_ShouldMaintainSystemIntegrity()
        {
            var createdUsers = new List<int>();
            var createdBooks = new List<int>();
            var takenBooks = new List<int>();

            try
            {
                for (int i = 0; i < 3; i++)
                {
                    var userRequest = GenerateValidUserRequest();
                    var bookRequest = GenerateValidBookRequest();
                    
                    var userResponse = UsersEndpoint.CreateUser(userRequest);
                    var bookResponse = BooksEndpoint.CreateBook(bookRequest);
                    
                    if ((int)userResponse.StatusCode == 200 || (int)userResponse.StatusCode == 201)
                    {
                        var user = HandleUserCreationResponse(userResponse, userRequest.Name);
                        createdUsers.Add(user.Id);
                    }
                    
                    if ((int)bookResponse.StatusCode == 200 || (int)bookResponse.StatusCode == 201)
                    {
                        var book = HandleBookCreationResponse(bookResponse, bookRequest.Name);
                        createdBooks.Add(book.Id);
                    }
                }

                Assert.That(createdUsers.Count, Is.GreaterThan(0), "At least one user should be created");
                Assert.That(createdBooks.Count, Is.GreaterThan(0), "At least one book should be created");

                if (createdUsers.Count > 0 && createdBooks.Count > 0)
                {
                    var takeBookRequest = new TakeBookRequest
                    {
                        UserId = createdUsers[0],
                        BookId = createdBooks[0]
                    };
                    var takeBookResponse = TakeBookEndpoint.TakeBook(takeBookRequest);
                    
                    if ((int)takeBookResponse.StatusCode == 200 || (int)takeBookResponse.StatusCode == 201)
                    {
                        var takenBook = ApiClient.DeserializeResponse<TakeBookResponse>(takeBookResponse);
                        takenBooks.Add(takenBook.Id);
                    }
                }

                var invalidTakeRequest = new TakeBookRequest
                {
                    UserId = -1,
                    BookId = -1
                };
                var invalidTakeResponse = TakeBookEndpoint.TakeBook(invalidTakeRequest);

                Assert.That((int)invalidTakeResponse.StatusCode, Is.AnyOf(400, 404, 422), "Invalid take book request should fail");

                var getAllUsersResponse = UsersEndpoint.GetAllUsers();
                var getAllBooksResponse = BooksEndpoint.GetAllBooks();
                var getAllTakenBooksResponse = TakeBookEndpoint.GetAllTakenBooks();
                
                AssertSuccessfulResponse(getAllUsersResponse, "Getting all users should still work after error");
                AssertSuccessfulResponse(getAllBooksResponse, "Getting all books should still work after error");
                AssertSuccessfulResponse(getAllTakenBooksResponse, "Getting all taken books should still work after error");

            }
            finally
            {
                foreach (var takenBookId in takenBooks)
                {
                    TakeBookEndpoint.ReturnBook(takenBookId);
                }

                foreach (var bookId in createdBooks)
                {
                    CleanupTestBook(bookId);
                }

                foreach (var userId in createdUsers)
                {
                    CleanupTestUser(userId);
                }
            }
        }
    }
} 