using UI_Tests.Base;

namespace UI_Tests.BooksModuleTests
{
    public class BookDetailsTests : BaseTests
    {
        [Test]
        [Category("Books")]
        [Description("Verify that the book details page displays the correct information for a created book.")]
        public void VerifyBookDetailsPage()
        {
            LoginWithDefaultCredentials();

            var (bookName, author, genre, quantity) = CreateAndVerifyBookWithData();
            
            Pages.BooksPage.ClickDetailsButton(bookName);

            Assert.That(Pages.BookDetailsPage.GetBookName(), Is.EqualTo(bookName),
                $"The book name on the details page should match the created book name '{bookName}'.");
            Assert.That(Pages.BookDetailsPage.GetBookAuthor(), Is.EqualTo(author),
                $"The book author on the details page should match the created author '{author}'.");
            Assert.That(Pages.BookDetailsPage.GetBookGenre(), Is.EqualTo(genre),
                $"The book genre on the details page should match the created genre '{genre}'.");
            Assert.That(Pages.BookDetailsPage.GetBookQuantity(), Is.EqualTo(quantity),
                $"The book quantity on the details page should match the created quantity '{quantity}'.");
        }
    }
}

