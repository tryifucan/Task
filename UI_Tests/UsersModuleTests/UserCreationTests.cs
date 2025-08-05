using Core.Utils;
using UI_Tests.Base;

namespace UI_Tests.UsersModuleTests
{
    public class UserCreationTests : BaseTests
    {
        [Test]
        [Category("Users")]
        [Category("Regression")]
        [Description("Verify that a user can be created successfully.")]
        public void VerifyUserCreation()
        {
            LoginWithDefaultCredentials();
            var userName = CreateAndVerifyUser();
            
            TestUtilities.ValidateRequiredField(userName, "User name");
        }

        [Test]
        [Category("Users")]
        [Description("Verify that an error message is displayed when trying to create a user with an empty name.")]
        public void VerifyUserCreationWithEmptyName()
        {
            LoginWithDefaultCredentials();
            NavigateToUsersPage();
            Pages.UsersPage.ClickAddUserButton();
            Pages.CreateUserPage.FillNameInput(string.Empty);
            Pages.CreateUserPage.ClickCreateUserButton();
            
            VerifyUserValidationError("An error occurred while processing your request.");
        }

        [Test]
        [Category("Users")]
        [Description("Verify that an error message is displayed when trying to create a user with a name exceeding the maximum length of 100.")]
        public void VerifyUserCreationWithNameExceedingMaxLength()
        {
            LoginWithDefaultCredentials();
            NavigateToUsersPage();
            Pages.UsersPage.ClickAddUserButton();
            string longName = Generator.GenerateRandomString(105);
            Pages.CreateUserPage.FillNameInput(longName);
            Pages.CreateUserPage.ClickCreateUserButton();
            
            VerifyUserValidationError("An error occurred while processing your request.");
        }
    }
}
