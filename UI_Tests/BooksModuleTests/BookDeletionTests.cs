using Core.Config;
using Core.Utils;
using UI_Tests.Base;

namespace UI_Tests.BooksModuleTests
{
    public class BookDeletionTests : BaseTests
    {
        [Test]
        [Category("Books")]
        [Category("Regression")]
        [Description("Verify that a book can be deleted successfully.")]
        public void VerifyBookDeletion()
        {
            LoginWithDefaultCredentials();

            var bookName = CreateAndVerifyBook();

            DeleteAndVerifyBook(bookName);
        }

        [Test]
        [Category("Books")]
        [Description("Verify that a book can be deleted with canceling the deletion process.")]
        public void VerifyBookDeletionWithCancel()
        {
            LoginWithDefaultCredentials();

            var bookName = CreateAndVerifyBook();

            Pages.BooksPage.ClickDeleteBookButton(bookName);
            Pages.BookDeletePage.ClickBackToListButton();

            Assert.That(Pages.BooksPage.IsBookTitleDisplayed(bookName), Is.True,
                $"Book with title '{bookName}' should still be displayed on the Books page after canceling deletion.");
        }
    }
}

