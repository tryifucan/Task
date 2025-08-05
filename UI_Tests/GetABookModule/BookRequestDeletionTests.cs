using Core.Config;
using Core.Utils;
using UI_Tests.Base;

namespace UI_Tests.GetABookModule
{
    public class BookRequestDeletionTests : BaseTests
    {
        [Test]
        [Category("GetABook")]
        [Description("Verify that a book request can be deleted successfully.")]
        public void VerifyBookRequestDeletion()
        {
            LoginWithDefaultCredentials();

            var userName = Generator.GenerateRandomString(10);
            var bookName = Generator.GenerateRandomString(10);

            var (bookNameCreated, author, genre, originalQuantity) = CreateAndVerifyBookRequest(userName, bookName);

            DeleteAndVerifyBookRequest(author);

            NavigateToBooksPage();
            Pages.BooksPage.ClickDetailsButton(bookNameCreated);
            
            Assert.That(Pages.BookDetailsPage.GetBookQuantity(), Is.EqualTo(originalQuantity), 
                $"Expected quantity to be {originalQuantity} after book was returned");
        }
    }
}

