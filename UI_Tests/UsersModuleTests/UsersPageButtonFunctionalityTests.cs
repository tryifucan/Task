using Core.Config;
using UI_Tests.Base;

namespace UI_Tests.UsersModuleTests
{
    public class UsersPageButtonFunctionalityTests : BaseTests
    {
        [Test]
        [Category("Users")]
        [Description("Verify that the Create User button redirects to the Create User page.")]
        public void VerifyCreateUserButtonFunctionality()
        {
            LoginWithDefaultCredentials();
            Pages.UsersPage.ClickAddUserButton();
            Assert.That(Driver.Url, Does.Contain("Create"), "Create User button did not redirect to the Create User page.");
        }

        [Test]
        [Category("Users")]
        [Description("Verify that the Edit button redirects to the Edit page.")]
        public void VerifyEditUserButtonFunctionality()
        {
            LoginWithDefaultCredentials();
            Pages.UsersPage.ClickEditUserButton();
            Assert.That(Driver.Url, Does.Contain("/Users/Edit"), "Edit User button did not redirect to the Edit User page.");
        }

        [Test]
        [Category("Users")]
        [Description("Verify that the Details button redirects to the User Details page.")]
        public void VerifyDetailsButtonFunctionality()
        {
            LoginWithDefaultCredentials();
            Pages.UsersPage.ClickDetailsButton();
            Assert.That(Driver.Url, Does.Contain("/Users/Details"), "Details button did not redirect to the User Details page.");
        }

        [Test]
        [Category("Users")]
        [Description("Verify that the Delete button redirects to the Delete User page.")]
        public void VerifyDeleteUserButtonFunctionality()
        {
            LoginWithDefaultCredentials();
            Pages.UsersPage.ClickDeleteUserButton();
            Assert.That(Driver.Url, Does.Contain("/Users/Delete"), "Delete User button did not redirect to the Delete User page.");
        }

        [Test]
        [Category("Users")]
        [Description("Verify that the Users page is displayed when navigating from the main menu.")]
        public void VerifyUsersPageRedirectionFromMainMenu()
        {
            LoginWithDefaultCredentials();
            Pages.UsersPage.NavigateToUsersPage();
            Assert.That(Driver.Url, Does.Contain("Users"), "User was not redirected to the Users page from the main menu.");
        }
    }
}

