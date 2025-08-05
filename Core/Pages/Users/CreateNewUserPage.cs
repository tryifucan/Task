using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Core.Pages.Users
{
    public class CreateNewUserPage(IWebDriver driver, WebDriverWait wait) : BasePage(driver, wait)
    {
        private readonly By nameInput = By.CssSelector("#Name");
        private readonly By createUserButton = By.CssSelector("[type='submit']");
        private readonly By errorMessage = By.XPath("//div//h2");

        public void ClickCreateUserButton()
        {
            WaitAndClick(createUserButton);
        }

        public void FillNameInput(string name)
        {
            ClearAndType(nameInput, name);
        }

        public void CreateNewUser(string name)
        {
            FillNameInput(name);
            ClickCreateUserButton();
        }

        public string GetErrorMessage()
        {
            WaitUntilElementVisible(errorMessage);
            return GetElementText(errorMessage);
        }
    }
}
