using Core.Config;
using Core.Utils;
using UI_Tests.Base;

namespace UI_Tests.BooksModuleTests
{
    public class BookEditTests : BaseTests
    {
        [Test]
        [Category("Books")]
        [Description("Verify that a book can be edited successfully.")]
        public void EditBook()
        {
            LoginWithDefaultCredentials();
            var (bookName, author, genre, quantity) = CreateAndVerifyBookWithData();
            
            var editBookData = Generator.GenerateRandomString(10);
            var editBookQty = Generator.GenerateRandomQuantity();

            EditAndVerifyBook(bookName, editBookData, editBookData, editBookData, editBookQty);
        }

        [Test]
        [Category("Books")]
        [Description("Verify that an error message is displayed when trying to edit a book with an empty name.")]
        public void EditBookWithEmptyName()
        {
            LoginWithDefaultCredentials();
            var (bookName, author, genre, quantity) = CreateAndVerifyBookWithData();

            EditBookAndVerifyError(bookName, string.Empty, author, genre, quantity);
        }

        [Test]
        [Category("Books")]
        [Description("Verify that an error message is displayed when trying to edit a book with a name exceeding the maximum length of 250.")]
        public void EditBookWithNameExceedingMaxLength()
        {
            LoginWithDefaultCredentials();
            var (bookName, author, genre, quantity) = CreateAndVerifyBookWithData();

            var longName = Generator.GenerateRandomString(255);

            EditBookAndVerifyError(bookName, longName, author, genre, quantity);
        }

        [Test]
        [Category("Books")]
        [Description("Verify that an error message is displayed when trying to edit a book with an author name exceeding the maximum length of 100.")]
        public void EditBookWithAuthorExceedingMaxLength()
        {
            LoginWithDefaultCredentials();
            var (bookName, author, genre, quantity) = CreateAndVerifyBookWithData();
            
            var longAuthorName = Generator.GenerateRandomString(105);

            EditBookAndVerifyError(bookName, bookName, longAuthorName, genre, quantity);
        }

        [Test]
        [Category("Books")]
        [Description("Verify that an error message is displayed when trying to edit a book with a genre name exceeding the maximum length of 50.")]
        public void EditBookWithGenreExceedingMaxLength()
        {
            LoginWithDefaultCredentials();
            var (bookName, author, genre, quantity) = CreateAndVerifyBookWithData();
            
            var longGenreName = Generator.GenerateRandomString(55);

            EditBookAndVerifyError(bookName, bookName, author, longGenreName, quantity);
        }
    }
}

