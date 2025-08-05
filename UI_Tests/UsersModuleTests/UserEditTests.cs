using Core.Config;
using Core.Utils;
using UI_Tests.Base;

namespace UI_Tests.UsersModuleTests
{
    public class UserEditTests : BaseTests
    {
        [Test]
        [Category("Users")]
        [Description("Verify that a user can be edited successfully with valid data.")]
        public void VerifyUserEditWithValidData()
        {
            LoginWithDefaultCredentials();

            var userName = CreateAndVerifyUser();
            var updatedUserName = Generator.GenerateRandomString(10);

            EditAndVerifyUser(userName, updatedUserName);
        }

        [Test]
        [Category("Users")]
        [Description("Verify that an error message is displayed when trying to edit a user with an empty name.")]
        public void VerifyUserEditWithEmptyName()
        {
            LoginWithDefaultCredentials();

            var userName = CreateAndVerifyUser();

            EditUserAndVerifyError(userName, string.Empty);
        }

        [Test]
        [Category("Users")]
        [Description("Verify that an error message is displayed when trying to edit a user with a name exceeding the maximum length of 100.")]
        public void VerifyUserEditWithNameExceedingMaxLength()
        {
            LoginWithDefaultCredentials();

            var userName = CreateAndVerifyUser();
            var updatedUserName = Generator.GenerateRandomString(105);

            EditUserAndVerifyError(userName, updatedUserName);
        }
    }
}

