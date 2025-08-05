using Core.Config;
using UI_Tests.Base;

namespace UI_Tests.BooksModuleTests
{
    public class BooksPageButtonFunctionalityTests: BaseTests
    {
        [Test]
        [Category("Books")]
        [Description("Verify that the Books page is displayed when navigating from the main menu.")]
        public void VerifyBooksPageRedirectionFromMainMenu()
        {
            Pages.LoginPage.Login(ConfigurationReader.Username, ConfigurationReader.Password);
            Pages.BooksPage.NavigateToBooksPage();
            Assert.That(Driver.Url, Does.Contain("Books"), "User was not redirected to the Books page from the main menu.");
        }

        [Test]
        [Category("Books")]
        [Description("Verify that the Create Books button redirects to the Create Book page.")]
        public void VerifyCreateBookButtonFunctionality()
        {
            Pages.LoginPage.Login(ConfigurationReader.Username, ConfigurationReader.Password);
            Pages.BooksPage.NavigateToBooksPage();
            Pages.BooksPage.ClickAddBookButton();
            Assert.That(Driver.Url, Does.Contain("Create"), "Create Book button did not redirect to the Create Books page.");
        }

        [Test]
        [Category("Books")]
        [Description("Verify that the Edit button redirects to the Edit page.")]
        public void VerifyEditBookButtonFunctionality()
        {
            Pages.LoginPage.Login(ConfigurationReader.Username, ConfigurationReader.Password);
            Pages.BooksPage.NavigateToBooksPage();
            Pages.BooksPage.ClickEditBookButton();
            Assert.That(Driver.Url, Does.Contain("/Books/Edit"), "Edit User button did not redirect to the Edit Book page.");
        }

        [Test]
        [Category("Books")]
        [Description("Verify that the Details button redirects to the Book Details page.")]
        public void VerifyDetailsButtonFunctionality()
        {
            Pages.LoginPage.Login(ConfigurationReader.Username, ConfigurationReader.Password);
            Pages.BooksPage.NavigateToBooksPage();
            Pages.BooksPage.ClickDetailsButton();
            Assert.That(Driver.Url, Does.Contain("/Books/Details"), "Details button did not redirect to the Book Details page.");
        }

        [Test]
        [Category("Books")]
        [Description("Verify that the Delete button redirects to the Delete Book page.")]
        public void VerifyDeleteBookButtonFunctionality()
        {
            Pages.LoginPage.Login(ConfigurationReader.Username, ConfigurationReader.Password);
            Pages.BooksPage.NavigateToBooksPage();
            Pages.BooksPage.ClickDeleteBookButton();
            Assert.That(Driver.Url, Does.Contain("/Books/Delete"), "Delete Book button did not redirect to the Delete Book page.");
        }
    }
}

