using Core.Config;
using Core.Utils;
using UI_Tests.Base;

namespace UI_Tests.BooksModuleTests
{
    public class BookCreationTests : BaseTests
    {
        [Test]
        [Category("Books")]
        [Category("Regression")]
        [Description("Verify that a book can be created successfully.")]
        public void CreateNewBook()
        {
            LoginWithDefaultCredentials();

            var bookName = CreateAndVerifyBook();
            
            TestUtilities.ValidateRequiredField(bookName, "Book name");
        }

        [Test]
        [Category("Books")]
        [Description("Verify that an error message is displayed when trying to create a book with an empty name.")]
        public void CreateNewBookWithEmptyName()
        {
            string newBookData = Generator.GenerateRandomString(10);
            string newBookQty = Generator.GenerateRandomQty();

            LoginWithDefaultCredentials();
            NavigateToBooksPage();
            Pages.BooksPage.ClickAddBookButton();
            Pages.CreateNewBookPage.CreateNewBook(string.Empty, newBookData, newBookData, newBookQty);

            VerifyValidationErrorOnCreatePage();
        }

        [Test]
        [Category("Books")]
        [Description("Verify that an error message is displayed when trying to create a book with a name exceeding the maximum length of 250.")]
        public void CreateNewBookWithNameExceedingMaxLength()
        {
            string newBookData = Generator.GenerateRandomString(10);
            string longName = Generator.GenerateRandomString(255);
            string newBookQty = Generator.GenerateRandomQty();
            
            LoginWithDefaultCredentials();
            NavigateToBooksPage();
            Pages.BooksPage.ClickAddBookButton();
            Pages.CreateNewBookPage.CreateNewBook(longName, newBookData, newBookData, newBookQty);

            VerifyValidationErrorOnCreatePage();
        }

        [Test]
        [Category("Books")]
        [Description("Verify that an error message is displayed when trying to create a book with a author exceeding the maximum length of 100.")]
        public void CreateNewBookWithAuthorExceedingMaxLength()
        {
            string newBookData = Generator.GenerateRandomString(10);
            string longAuthor = Generator.GenerateRandomString(105);
            string newBookQty = Generator.GenerateRandomQty();
            
            LoginWithDefaultCredentials();
            NavigateToBooksPage();
            Pages.BooksPage.ClickAddBookButton();
            Pages.CreateNewBookPage.CreateNewBook(newBookData, longAuthor, newBookData, newBookQty);

            VerifyValidationErrorOnCreatePage();
        }

        [Test]
        [Category("Books")]
        [Description("Verify that an error message is displayed when trying to create a book with a genre exceeding the maximum length of 50.")]
        public void CreateNewBookWithGenreExceedingMaxLength()
        {
            string newBookData = Generator.GenerateRandomString(10);
            string longGenre = Generator.GenerateRandomString(55);
            string newBookQty = Generator.GenerateRandomQty();
            
            LoginWithDefaultCredentials();
            NavigateToBooksPage();
            Pages.BooksPage.ClickAddBookButton();
            Pages.CreateNewBookPage.CreateNewBook(newBookData, newBookData, longGenre, newBookQty);

            VerifyValidationErrorOnCreatePage();
        }
    }
}

