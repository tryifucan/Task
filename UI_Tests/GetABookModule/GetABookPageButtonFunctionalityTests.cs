using UI_Tests.Base;

namespace UI_Tests.GetABookModule
{
    public class GetABookPageButtonFunctionalityTests : BaseTests
    {
        [Test]
        [Category("GetABook")]
        [Description("Verify that the Books page is displayed when navigating from the main menu.")]
        public void VerifyGetABookPageRedirectionFromMainMenu()
        {
            LoginWithDefaultCredentials();
            NavigateToGetABookPage();
            Assert.That(Driver.Url, Does.Contain("GetBook"), "User was not redirected to the Books page from the main menu.");
        }

        [Test]
        [Category("GetABook")]
        [Description("Verify that the Create Books Request button redirects to the Create Book Request page.")]
        public void VerifyCreateBookRequestButtonFunctionality()
        {
            LoginWithDefaultCredentials();
            NavigateToGetABookPage();
            Pages.GetABookPage.ClickAddBookRequestButton();
            Assert.That(Driver.Url, Does.Contain("Create"), "Create Book button did not redirect to the Create Books page.");
        }

        [Test]
        [Category("GetABook")]
        [Description("Verify that the Edit button redirects to the Edit page.")]
        public void VerifyEditBookButtonFunctionality()
        {
            LoginWithDefaultCredentials();
            NavigateToGetABookPage();
            Pages.GetABookPage.ClickEditBookRequestButton();
            Assert.That(Driver.Url, Does.Contain("/GetBook/Edit"), "Edit User button did not redirect to the Edit Book page.");
        }

        [Test]
        [Category("GetABook")]
        [Description("Verify that the Details button redirects to the Book Request Details page.")]
        public void VerifyDetailsButtonFunctionality()
        {
            LoginWithDefaultCredentials();
            NavigateToGetABookPage();
            Pages.GetABookPage.ClickDetailsButton();
            Assert.That(Driver.Url, Does.Contain("/GetBook/Details"), "Details button did not redirect to the Book Details page.");
        }

        [Test]
        [Category("GetABook")]
        [Description("Verify that the Delete button redirects to the Delete Book Request page.")]
        public void VerifyDeleteBookButtonFunctionality()
        {
            LoginWithDefaultCredentials();
            NavigateToGetABookPage();
            Pages.GetABookPage.ClickDeleteBookRequestButton();
            Assert.That(Driver.Url, Does.Contain("/GetBook/Delete"), "Delete Book button did not redirect to the Delete Book page.");
        }
    }
}

