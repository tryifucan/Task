using Core.ApiClient.Endpoints.Users;
using Core.ApiClient.Endpoints.Books;
using Core.ApiClient.Models.Users;
using Core.ApiClient.Models.Books;
using API_Tests.Base;

namespace API_Tests.IntegrationTests
{
    [TestFixture]
    [Category("Integration")]
    [Category("BookManagement")]
    public class BookManagementIntegrationTests : BaseAPITests
    {
        [Test]
        [Description("Complete Book Lifecycle: Create -> Read -> Update -> Delete")]
        public void CompleteBookLifecycle_ShouldWorkEndToEnd()
        {
            var createBookRequest = GenerateValidBookRequest();
            var createResponse = BooksEndpoint.CreateBook(createBookRequest);
            
            AssertSuccessfulResponse(createResponse, "Book creation should succeed");

            var createdBook = HandleBookCreationResponse(createResponse, createBookRequest.Name);

            Assert.That(createdBook, Is.Not.Null, "Created book should not be null");
            Assert.That(createdBook.Id, Is.GreaterThan(0), "Book should have a valid ID");
            var bookId = createdBook.Id;

            var getResponse = BooksEndpoint.GetBookById(bookId);

            AssertSuccessfulResponse(getResponse, "Getting created book should succeed");

            var retrievedBook = ApiClient.DeserializeResponse<BookResponse>(getResponse);

            Assert.That(retrievedBook.Name, Is.EqualTo(createBookRequest.Name), "Retrieved book should match created book");
            Assert.That(retrievedBook.Author, Is.EqualTo(createBookRequest.Author), "Retrieved book should match created book");
            Assert.That(retrievedBook.Genre, Is.EqualTo(createBookRequest.Genre), "Retrieved book should match created book");

            var updateBookRequest = GenerateValidUpdateBookRequest();
            var updateResponse = BooksEndpoint.UpdateBook(bookId, updateBookRequest);

            AssertSuccessfulResponse(updateResponse, "Book update should succeed");

            var updatedBook = ApiClient.DeserializeResponse<BookResponse>(updateResponse);

            Assert.That(updatedBook.Name, Is.EqualTo(updateBookRequest.Name), "Updated book should have new name");
            Assert.That(updatedBook.Author, Is.EqualTo(updateBookRequest.Author), "Updated book should have new author");

            var getUpdatedResponse = BooksEndpoint.GetBookById(bookId);

            AssertSuccessfulResponse(getUpdatedResponse, "Getting updated book should succeed");

            var finalBook = ApiClient.DeserializeResponse<BookResponse>(getUpdatedResponse);

            Assert.That(finalBook.Name, Is.EqualTo(updateBookRequest.Name), "Final book should reflect the update");
            Assert.That(finalBook.Author, Is.EqualTo(updateBookRequest.Author), "Final book should reflect the update");

            var deleteResponse = BooksEndpoint.DeleteBook(bookId);

            Assert.That((int)deleteResponse.StatusCode, Is.AnyOf(200, 204), "Book deletion should succeed");

            var getDeletedResponse = BooksEndpoint.GetBookById(bookId);

            Assert.That((int)getDeletedResponse.StatusCode, Is.AnyOf(400, 404), "Deleted book should not be accessible");
        }

        [Test]
        [Description("Book Inventory Management: Create Book -> Take Book -> Verify Availability -> Return Book")]
        public void BookInventoryManagement_ShouldWorkEndToEnd()
        {
            var createBookRequest = GenerateValidBookRequest();
            createBookRequest.Quantity = 2;
            var createBookResponse = BooksEndpoint.CreateBook(createBookRequest);

            AssertSuccessfulResponse(createBookResponse, "Book creation should succeed");

            var book = HandleBookCreationResponse(createBookResponse, createBookRequest.Name);
            var bookId = book.Id;

            var createUserRequest = GenerateValidUserRequest();
            var createUserResponse = UsersEndpoint.CreateUser(createUserRequest);

            AssertSuccessfulResponse(createUserResponse, "User creation should succeed");

            var user = ApiClient.DeserializeResponse<UserResponse>(createUserResponse);
            var userId = user.Id;

            try
            {
                var takeBook1Request = new TakeBookRequest
                {
                    UserId = userId,
                    BookId = bookId
                };
                var takeBook1Response = TakeBookEndpoint.TakeBook(takeBook1Request);

                AssertSuccessfulResponse(takeBook1Response, "Taking first book copy should succeed");

                var takenBook1 = ApiClient.DeserializeResponse<TakeBookResponse>(takeBook1Response);

                var getBookAfterFirstTakeResponse = BooksEndpoint.GetBookById(bookId);

                AssertSuccessfulResponse(getBookAfterFirstTakeResponse, "Getting book after first take should succeed");

                var bookAfterFirstTake = ApiClient.DeserializeResponse<BookResponse>(getBookAfterFirstTakeResponse);

                Assert.That(bookAfterFirstTake.Quantity, Is.GreaterThan(0), "Book should still have copies available");

                var takeBook2Request = new TakeBookRequest
                {
                    UserId = userId,
                    BookId = bookId
                };
                var takeBook2Response = TakeBookEndpoint.TakeBook(takeBook2Request);

                AssertSuccessfulResponse(takeBook2Response, "Taking second book copy should succeed");

                var takenBook2 = ApiClient.DeserializeResponse<TakeBookResponse>(takeBook2Response);

                var getBookAfterSecondTakeResponse = BooksEndpoint.GetBookById(bookId);

                AssertSuccessfulResponse(getBookAfterSecondTakeResponse, "Getting book after second take should succeed");
                
                var bookAfterSecondTake = ApiClient.DeserializeResponse<BookResponse>(getBookAfterSecondTakeResponse);

                var returnBook1Response = TakeBookEndpoint.ReturnBook(takenBook1.Id);

                AssertSuccessfulResponse(returnBook1Response, "Returning first book should succeed");

                var getBookAfterReturnResponse = BooksEndpoint.GetBookById(bookId);

                AssertSuccessfulResponse(getBookAfterReturnResponse, "Getting book after return should succeed");

                var bookAfterReturn = ApiClient.DeserializeResponse<BookResponse>(getBookAfterReturnResponse);

                var returnBook2Response = TakeBookEndpoint.ReturnBook(takenBook2.Id);

                AssertSuccessfulResponse(returnBook2Response, "Returning second book should succeed");

                var getAllTakenBooksResponse = TakeBookEndpoint.GetAllTakenBooks();

                AssertSuccessfulResponse(getAllTakenBooksResponse, "Getting all taken books should succeed");

                var allTakenBooks = ApiClient.DeserializeResponse<TakeBookListResponse>(getAllTakenBooksResponse);
                var userTakenBooks = allTakenBooks.Where(tb => tb.UserId == userId && tb.BookId == bookId);

                Assert.That(userTakenBooks, Is.Empty, "User should have no taken books after returning both");
            }
            finally
            {
                CleanupTestBook(bookId);
                CleanupTestUser(userId);
            }
        }

        [Test]
        [Description("Book Catalog Management: Create Multiple Books -> List All -> Search by Genre")]
        public void BookCatalogManagement_ShouldWorkEndToEnd()
        {
            var fictionBookRequest = GenerateValidBookRequest();
            fictionBookRequest.Genre = "Fiction";
            fictionBookRequest.Name = "The Great Adventure";
            fictionBookRequest.Author = "John Fiction";

            var nonFictionBookRequest = GenerateValidBookRequest();
            nonFictionBookRequest.Genre = "Non-Fiction";
            nonFictionBookRequest.Name = "Science Today";
            nonFictionBookRequest.Author = "Jane Science";

            var createFictionResponse = BooksEndpoint.CreateBook(fictionBookRequest);
            var createNonFictionResponse = BooksEndpoint.CreateBook(nonFictionBookRequest);

            AssertSuccessfulResponse(createFictionResponse, "Fiction book creation should succeed");
            AssertSuccessfulResponse(createNonFictionResponse, "Non-fiction book creation should succeed");

            var fictionBook = HandleBookCreationResponse(createFictionResponse, fictionBookRequest.Name);
            var nonFictionBook = HandleBookCreationResponse(createNonFictionResponse, nonFictionBookRequest.Name);

            try
            {
                var getAllBooksResponse = BooksEndpoint.GetAllBooks();

                AssertSuccessfulResponse(getAllBooksResponse, "Getting all books should succeed");

                var allBooks = ApiClient.DeserializeResponse<BooksListResponse>(getAllBooksResponse);

                var fictionBookInList = allBooks.FirstOrDefault(b => b.Id == fictionBook.Id);
                var nonFictionBookInList = allBooks.FirstOrDefault(b => b.Id == nonFictionBook.Id);

                Assert.That(fictionBookInList, Is.Not.Null, "Fiction book should be in the catalog");
                Assert.That(nonFictionBookInList, Is.Not.Null, "Non-fiction book should be in the catalog");
                Assert.That(fictionBookInList.Genre, Is.EqualTo("Fiction"), "Fiction book should have correct genre");
                Assert.That(nonFictionBookInList.Genre, Is.EqualTo("Non-Fiction"), "Non-fiction book should have correct genre");

                var getFictionResponse = BooksEndpoint.GetBookById(fictionBook.Id);
                var getNonFictionResponse = BooksEndpoint.GetBookById(nonFictionBook.Id);

                AssertSuccessfulResponse(getFictionResponse, "Getting fiction book should succeed");
                AssertSuccessfulResponse(getNonFictionResponse, "Getting non-fiction book should succeed");

                var retrievedFiction = ApiClient.DeserializeResponse<BookResponse>(getFictionResponse);
                var retrievedNonFiction = ApiClient.DeserializeResponse<BookResponse>(getNonFictionResponse);

                Assert.That(retrievedFiction.Name, Is.EqualTo("The Great Adventure"), "Retrieved fiction book should have correct name");
                Assert.That(retrievedNonFiction.Name, Is.EqualTo("Science Today"), "Retrieved non-fiction book should have correct name");

                var updateRequest = GenerateValidUpdateBookRequest();
                updateRequest.Name = "Updated Adventure";
                updateRequest.Author = "John Updated";

                var updateResponse = BooksEndpoint.UpdateBook(fictionBook.Id, updateRequest);

                AssertSuccessfulResponse(updateResponse, "Updating fiction book should succeed");

                var updatedBook = ApiClient.DeserializeResponse<BookResponse>(updateResponse);

                Assert.That(updatedBook.Name, Is.EqualTo("Updated Adventure"), "Updated book should have new name");

                var getUpdatedCatalogResponse = BooksEndpoint.GetAllBooks();

                AssertSuccessfulResponse(getUpdatedCatalogResponse, "Getting updated catalog should succeed");

                var updatedCatalog = ApiClient.DeserializeResponse<BooksListResponse>(getUpdatedCatalogResponse);

                var updatedBookInCatalog = updatedCatalog.FirstOrDefault(b => b.Id == fictionBook.Id);
                Assert.That(updatedBookInCatalog, Is.Not.Null, "Updated book should still be in catalog");
                Assert.That(updatedBookInCatalog.Name, Is.EqualTo("Updated Adventure"), "Catalog should reflect the update");
            }
            finally
            {
                CleanupTestBook(fictionBook.Id);
                CleanupTestBook(nonFictionBook.Id);
            }
        }
    }
} 