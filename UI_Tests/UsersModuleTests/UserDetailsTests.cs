using Core.Config;
using Core.Utils;
using UI_Tests.Base;

namespace UI_Tests.UsersModuleTests
{
    public class UserDetailsTests : BaseTests
    {
        [Test]
        [Category("Users")]
        [Description("Verify that a user can be viewed on the User Details page after creation.")]
        public void VerifyUserDetailsPage()
        {
            LoginWithDefaultCredentials();

            var userName = CreateAndVerifyUser();

            NavigateToUserDetailsPage(userName);
            
            Assert.That(Pages.UserDetailsPage.GetUserName(), Is.EqualTo(userName),
                $"The user name on the details page should match the created user name '{userName}'.");
        }
    }
}

