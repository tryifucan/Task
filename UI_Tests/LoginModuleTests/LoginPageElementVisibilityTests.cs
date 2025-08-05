using NUnit.Framework.Internal;
using UI_Tests.Base;

namespace UI_Tests.LoginModuleTests
{
    public class LoginPageElementVisibilityTests : BaseTests
    {
        [Test]
        [Category("Login")]
        [Description("Verify that the username field is present on the login page.")]
        public void VerifyLoginPageUsernameFieldIsPresent()
        {
            Assert.That(Pages.LoginPage.IsUsernameFieldDisplayed(), Is.True, "Username field is not displayed on the login page.");
        }
        [Test]
        [Category("Login")]
        [Description("Verify that the password field is present on the login page.")]
        public void VerifyLoginPagePasswordFieldIsPresent()
        {
            Assert.That(Pages.LoginPage.IsPasswordFieldDisplayed(), Is.True, "Password field is not displayed on the login page.");
        }
        [Test]
        [Category("Login")]
        [Description("Verify that the sign-in button is present on the login page.")]
        public void VerifyLoginPageSignInButtonIsPresent()
        {
            Assert.That(Pages.LoginPage.IsSignInButtonDisplayed(), Is.True, "Sign-in button is not displayed on the login page.");
        }
        [Test]
        [Category("Login")]
        [Description("Verify that the sign-up button is present on the login page.")]
        public void VerifyLoginPageSignUpButtonIsPresent()
        {
            Assert.That(Pages.LoginPage.IsSignUpButtonDisplayed(), Is.True, "Sign-up button is not displayed on the login page.");
        }
        [Test]
        [Category("Login")]
        [Description("Verify that the forgot password link is present on the login page.")]
        public void VerifyLoginPageForgotPasswordLinkIsPresent()
        {
            Assert.That(Pages.LoginPage.IsForgotPasswordLinkDisplayed(), Is.True, "Forgot password link is not displayed on the login page.");
        }
    }
}

