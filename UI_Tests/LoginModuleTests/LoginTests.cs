using Core.Config;
using Core.Utils;
using UI_Tests.Base;

namespace UI_Tests.LoginModuleTests
{
    public class LoginTests : BaseTests
    {
        [Test]
        [Category("Login")]
        [Category("Regression")]
        [Description("Verify that a user can login successfully with valid credentials.")]
        public void VerifySuccessfulLogin()
        {
            LoginWithDefaultCredentials();
            Assert.That(Pages.LoginPage.IsLoginSuccessful(), Is.True, "Login should be successful with valid credentials.");
        }

        [Test]
        [Category("Login")]
        [Description("Verify that login fails with invalid username.")]
        public void VerifyLoginWithInvalidUsername()
        {
            string invalidUsername = Generator.GenerateRandomString(10);
            Pages.LoginPage.Login(invalidUsername, ConfigurationReader.Password);
            Assert.That(Pages.LoginPage.IsLoginSuccessful(), Is.False, "Login should fail with invalid username.");
        }

        [Test]
        [Category("Login")]
        [Description("Verify that login fails with invalid password.")]
        public void VerifyLoginWithInvalidPassword()
        {
            string invalidPassword = Generator.GenerateRandomString(10);
            Pages.LoginPage.Login(ConfigurationReader.Username, invalidPassword);
            Assert.That(Pages.LoginPage.IsLoginSuccessful(), Is.False, "Login should fail with invalid password.");
        }

        [Test]
        [Category("Login")]
        [Description("Verify that login fails with empty credentials.")]
        public void VerifyLoginWithEmptyCredentials()
        {
            Pages.LoginPage.Login(string.Empty, string.Empty);
            Assert.That(Pages.LoginPage.IsLoginSuccessful(), Is.False, "Login should fail with empty credentials.");
        }
    }
}
