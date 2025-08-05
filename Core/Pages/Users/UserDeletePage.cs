using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Core.Pages.Users
{
    public class UserDeletePage(IWebDriver driver, WebDriverWait wait) : BasePage(driver, wait)
    {
        protected readonly By deleteBtn = By.CssSelector("[type='submit']");
        protected readonly By confirmationMsg = By.XPath("//div//h3");
        private readonly By backToListBtn = By.XPath("//form//a");


        public void ClickDeleteButton()
        {
            WaitAndClick(deleteBtn);
        }
        public string GetConfirmationMessage()
        {
            return GetElementText(confirmationMsg);
        }

        public void ClickBackToListButton()
        {
            WaitAndClick(backToListBtn);
        }
    }
}
