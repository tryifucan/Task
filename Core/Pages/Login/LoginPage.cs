using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Core.Pages.Login
{
    public class LoginPage(IWebDriver driver, WebDriverWait wait) : BasePage(driver, wait)
    {
        private readonly By usernameField = By.XPath("//input[@name='username']");
        private readonly By passwordField = By.XPath("//input[@name='password']");
        private readonly By signInBtn = By.XPath("//a//div[contains(., 'Sign In')]");
        private readonly By signUpBtn = By.XPath("//a//div[contains(., 'Sign Up')]");
        private readonly By forgotPswrdBtn = By.XPath("//a[contains(., 'Here')]");

        public void EnterUsername(string username)
        {
            WaitUntilElementVisible(usernameField);
            ClearAndType(usernameField, username);
        }
        public void EnterPassword(string password)
        {
            WaitUntilElementVisible(passwordField);
            ClearAndType(passwordField, password);
        }
        public void ClickLoginButton()
        {
            WaitAndClick(signInBtn);
        }
        public void Login(string username, string password)
        {
            EnterUsername(username);
            EnterPassword(password);
            ClickLoginButton();
            AcceptCookies();
        }
        public void ClickSignUpButton()
        {
            WaitAndClick(signUpBtn);
        }
        public void ClickForgotPasswordButton()
        {
            WaitAndClick(forgotPswrdBtn);
        }
        public bool IsUsernameFieldDisplayed() => IsElementDisplayed(usernameField);
        public bool IsPasswordFieldDisplayed() => IsElementDisplayed(passwordField);
        public bool IsSignInButtonDisplayed() => IsElementDisplayed(signInBtn);
        public bool IsSignUpButtonDisplayed() => IsElementDisplayed(signUpBtn);
        public bool IsForgotPasswordLinkDisplayed() => IsElementDisplayed(forgotPswrdBtn);

        public bool IsLoginSuccessful()
        {
            return Driver.Url.Contains("Users");
        }


    }
}
