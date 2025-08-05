using Core.Config;
using Core.Pages.GetABook;
using Core.Utils;
using UI_Tests.Base;

namespace UI_Tests.GetABookModule
{
    public class BookRequestCreationTests : BaseTests
    {
        [Test]
        [Category("GetABook")]
        [Category("Regression")]
        [Description("Verify that a book request can be created successfully.")]
        public void VerifyBookRequestCreation()
        {
            LoginWithDefaultCredentials();

            var userName = Generator.GenerateRandomString(10);
            var bookName = Generator.GenerateRandomString(10);

            var (bookNameCreated, author, genre, originalQuantity) = CreateAndVerifyBookRequest(userName, bookName);

            NavigateToBooksPage();
            Pages.BooksPage.ClickDetailsButton(bookNameCreated);
            
            var expectedQuantity = originalQuantity - 1;
            
            Assert.That(Pages.BookDetailsPage.GetBookQuantity(), Is.EqualTo(expectedQuantity), 
                $"Expected quantity to be {expectedQuantity} after book was taken (original: {originalQuantity})");
        }
    }
}

