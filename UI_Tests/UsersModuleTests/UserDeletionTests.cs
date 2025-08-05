using Core.Config;
using Core.Utils;
using UI_Tests.Base;

namespace UI_Tests.UsersModuleTests
{
    public class UserDeletionTests : BaseTests
    {
        [Test]
        [Category("Users")]
        [Category("Regression")]
        [Description("Verify that a user can be deleted successfully.")]
        public void VerifyUserDeletion()
        {
            LoginWithDefaultCredentials();
            var userName = CreateAndVerifyUser();
            
            DeleteAndVerifyUser(userName);
        }

        [Test]
        [Category("Users")]
        [Description("Verify that a user can be deleted with canceling the deletion process.")]
        public void VerifyUserDeletionWithCancel()
        {
            LoginWithDefaultCredentials();
            var userName = CreateAndVerifyUser();
            
            NavigateToUsersPage();
            Pages.UsersPage.ClickDeleteUserButton(userName);
            Pages.UserDeletePage.ClickBackToListButton();

            Assert.That(Pages.UsersPage.IsUserNameDisplayed(userName), Is.True,
                $"User with name '{userName}' should still be displayed on the Users page after canceling deletion.");
        }

        [Test]
        [Category("Users")]
        [Description("Verify that a confirmation message is displayed when deleting a user.")]
        public void VerifyUserDeletionConfirmationMessage()
        {
            LoginWithDefaultCredentials();
            var userName = CreateAndVerifyUser();

            NavigateToUsersPage();
            Pages.UsersPage.ClickDeleteUserButton(userName);
            
            Assert.That(Pages.UserDeletePage.GetConfirmationMessage(), Is.EqualTo("Are you sure you want to delete this user?"),
                "The confirmation message should be displayed when deleting a user.");
        }
    }
}
