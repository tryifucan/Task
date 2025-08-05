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
    [Category("UserManagement")]
    public class UserManagementIntegrationTests : BaseAPITests
    {
        [Test]
        [Description("Complete User Lifecycle: Create -> Read -> Update -> Delete")]
        public void CompleteUserLifecycle_ShouldWorkEndToEnd()
        {
            var createUserRequest = GenerateValidUserRequest();
            var createResponse = UsersEndpoint.CreateUser(createUserRequest);
            
            AssertSuccessfulResponse(createResponse, "User creation should succeed");

            var createdUser = HandleUserCreationResponse(createResponse, createUserRequest.Name);

            Assert.That(createdUser, Is.Not.Null, "Created user should not be null");
            Assert.That(createdUser.Id, Is.GreaterThan(0), "User should have a valid ID");

            var userId = createdUser.Id;

            var getResponse = UsersEndpoint.GetUserById(userId);

            AssertSuccessfulResponse(getResponse, "Getting created user should succeed");

            var retrievedUser = ApiClient.DeserializeResponse<UserResponse>(getResponse);

            Assert.That(retrievedUser.Name, Is.EqualTo(createUserRequest.Name), "Retrieved user should match created user");

            var updateUserRequest = GenerateValidUpdateUserRequest();
            var updateResponse = UsersEndpoint.UpdateUser(userId, updateUserRequest);

            AssertSuccessfulResponse(updateResponse, "User update should succeed");

            var updatedUser = ApiClient.DeserializeResponse<UserResponse>(updateResponse);

            Assert.That(updatedUser.Name, Is.EqualTo(updateUserRequest.Name), "Updated user should have new name");

            var getUpdatedResponse = UsersEndpoint.GetUserById(userId);

            AssertSuccessfulResponse(getUpdatedResponse, "Getting updated user should succeed");

            var finalUser = ApiClient.DeserializeResponse<UserResponse>(getUpdatedResponse);

            Assert.That(finalUser.Name, Is.EqualTo(updateUserRequest.Name), "Final user should reflect the update");

            var deleteResponse = UsersEndpoint.DeleteUser(userId);

            Assert.That((int)deleteResponse.StatusCode, Is.AnyOf(200, 204), "User deletion should succeed");

            var getDeletedResponse = UsersEndpoint.GetUserById(userId);

            Assert.That((int)getDeletedResponse.StatusCode, Is.AnyOf(400, 404), "Deleted user should not be accessible");
        }

        [Test]
        [Description("User Book Management: Create User -> Take Book -> Return Book")]
        public void UserBookManagement_ShouldWorkEndToEnd()
        {
            var createUserRequest = GenerateValidUserRequest();
            var createUserResponse = UsersEndpoint.CreateUser(createUserRequest);

            AssertSuccessfulResponse(createUserResponse, "User creation should succeed");

            var user = HandleUserCreationResponse(createUserResponse, createUserRequest.Name);
            var userId = user.Id;

            var createBookRequest = GenerateValidBookRequest();
            var createBookResponse = BooksEndpoint.CreateBook(createBookRequest);

            AssertSuccessfulResponse(createBookResponse, "Book creation should succeed");

            var book = HandleBookCreationResponse(createBookResponse, createBookRequest.Name);
            var bookId = book.Id;

            try
            {
                var takeBookRequest = new TakeBookRequest
                {
                    UserId = userId,
                    BookId = bookId
                };
                var takeBookResponse = TakeBookEndpoint.TakeBook(takeBookRequest);

                AssertSuccessfulResponse(takeBookResponse, "Taking book should succeed");

                var takenBook = ApiClient.DeserializeResponse<TakeBookResponse>(takeBookResponse);

                Assert.That(takenBook.UserId, Is.EqualTo(userId), "Taken book should be associated with the user");
                Assert.That(takenBook.BookId, Is.EqualTo(bookId), "Taken book should reference the correct book");

                var getAllTakenBooksResponse = TakeBookEndpoint.GetAllTakenBooks();

                AssertSuccessfulResponse(getAllTakenBooksResponse, "Getting all taken books should succeed");

                var allTakenBooks = ApiClient.DeserializeResponse<TakeBookListResponse>(getAllTakenBooksResponse);
                var userTakenBook = allTakenBooks.FirstOrDefault(tb => tb.UserId == userId && tb.BookId == bookId);

                Assert.That(userTakenBook, Is.Not.Null, "User's taken book should appear in the list");

                var returnBookResponse = TakeBookEndpoint.ReturnBook(takenBook.Id);

                AssertSuccessfulResponse(returnBookResponse, "Returning book should succeed");

                var getTakenBooksAfterReturnResponse = TakeBookEndpoint.GetAllTakenBooks();

                AssertSuccessfulResponse(getTakenBooksAfterReturnResponse, "Getting taken books after return should succeed");
                
                var takenBooksAfterReturn = ApiClient.DeserializeResponse<TakeBookListResponse>(getTakenBooksAfterReturnResponse);
                var returnedBook = takenBooksAfterReturn.FirstOrDefault(tb => tb.Id == takenBook.Id);
                
                Assert.That(returnedBook, Is.Null, "Returned book should no longer appear in taken books list");
            }
            finally
            {
                CleanupTestBook(bookId);
                CleanupTestUser(userId);
            }
        }

        [Test]
        [Description("Multi-User Book Sharing: Multiple users taking and returning the same book")]
        public void MultiUserBookSharing_ShouldWorkEndToEnd()
        {
            var user1Request = GenerateValidUserRequest();
            var user2Request = GenerateValidUserRequest();
            
            var user1Response = UsersEndpoint.CreateUser(user1Request);
            var user2Response = UsersEndpoint.CreateUser(user2Request);
            
            AssertSuccessfulResponse(user1Response, "User 1 creation should succeed");
            AssertSuccessfulResponse(user2Response, "User 2 creation should succeed");
            
            var user1 = HandleUserCreationResponse(user1Response, user1Request.Name);
            var user2 = HandleUserCreationResponse(user2Response, user2Request.Name);

            var createBookRequest = GenerateValidBookRequest();
            createBookRequest.Quantity = 2;
            var createBookResponse = BooksEndpoint.CreateBook(createBookRequest);

            AssertSuccessfulResponse(createBookResponse, "Book creation should succeed");

            var book = HandleBookCreationResponse(createBookResponse, createBookRequest.Name);

            try
            {
                var takeBook1Request = new TakeBookRequest { UserId = user1.Id, BookId = book.Id };
                var takeBook2Request = new TakeBookRequest { UserId = user2.Id, BookId = book.Id };

                var takeBook1Response = TakeBookEndpoint.TakeBook(takeBook1Request);
                var takeBook2Response = TakeBookEndpoint.TakeBook(takeBook2Request);

                AssertSuccessfulResponse(takeBook1Response, "User 1 taking book should succeed");
                AssertSuccessfulResponse(takeBook2Response, "User 2 taking book should succeed");

                var getAllTakenBooksResponse = TakeBookEndpoint.GetAllTakenBooks();

                AssertSuccessfulResponse(getAllTakenBooksResponse, "Getting all taken books should succeed");

                var allTakenBooks = ApiClient.DeserializeResponse<TakeBookListResponse>(getAllTakenBooksResponse);

                var user1TakenBook = allTakenBooks.FirstOrDefault(tb => tb.UserId == user1.Id && tb.BookId == book.Id);
                var user2TakenBook = allTakenBooks.FirstOrDefault(tb => tb.UserId == user2.Id && tb.BookId == book.Id);

                Assert.That(user1TakenBook, Is.Not.Null, "User 1 should have the book");
                Assert.That(user2TakenBook, Is.Not.Null, "User 2 should have the book");

                var returnBook1Response = TakeBookEndpoint.ReturnBook(user1TakenBook.Id);
                AssertSuccessfulResponse(returnBook1Response, "User 1 returning book should succeed");

                var getTakenBooksAfterReturn1Response = TakeBookEndpoint.GetAllTakenBooks();

                AssertSuccessfulResponse(getTakenBooksAfterReturn1Response, "Getting taken books after first return should succeed");
                
                var takenBooksAfterReturn1 = ApiClient.DeserializeResponse<TakeBookListResponse>(getTakenBooksAfterReturn1Response);

                var user1BookAfterReturn = takenBooksAfterReturn1.FirstOrDefault(tb => tb.UserId == user1.Id && tb.BookId == book.Id);
                var user2BookAfterReturn = takenBooksAfterReturn1.FirstOrDefault(tb => tb.UserId == user2.Id && tb.BookId == book.Id);

                Assert.That(user1BookAfterReturn, Is.Null, "User 1 should no longer have the book");
                Assert.That(user2BookAfterReturn, Is.Not.Null, "User 2 should still have the book");

                var returnBook2Response = TakeBookEndpoint.ReturnBook(user2TakenBook.Id);
                
                AssertSuccessfulResponse(returnBook2Response, "User 2 returning book should succeed");

                var getTakenBooksAfterReturn2Response = TakeBookEndpoint.GetAllTakenBooks();
                
                AssertSuccessfulResponse(getTakenBooksAfterReturn2Response, "Getting taken books after second return should succeed");
                
                var takenBooksAfterReturn2 = ApiClient.DeserializeResponse<TakeBookListResponse>(getTakenBooksAfterReturn2Response);
                var anyUserWithBook = takenBooksAfterReturn2.Any(tb => tb.BookId == book.Id);
                
                Assert.That(anyUserWithBook, Is.False, "No users should have the book after both return it");
            }
            finally
            {
                CleanupTestBook(book.Id);
                CleanupTestUser(user1.Id);
                CleanupTestUser(user2.Id);
            }
        }
    }
} 