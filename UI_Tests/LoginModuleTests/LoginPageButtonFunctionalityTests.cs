using Core.Pages;
using UI_Tests.Base;

namespace UI_Tests.LoginModuleTests
{
    public class LoginPageButtonFunctionalityTests : BaseTests
    {
        [Test]
        [Category("Login")]
        public void VerifySignUpButtonFunctionality()
        {
            Pages.LoginPage.ClickSignUpButton();
            Assert.That(Driver.Url, Does.Contain("Registration"), "Sign-up button did not redirect to the Registration page.");
        }

        [Test]
        [Category("Login")]
        public void VerifyForgotPasswordLinkFunctionality()
        {
            Pages.LoginPage.ClickForgotPasswordButton();
            Assert.That(Driver.Url, Does.Contain("ForgottenPassword"), "Forgot password link did not redirect to the Forgot Password page.");
        }
    }
}

