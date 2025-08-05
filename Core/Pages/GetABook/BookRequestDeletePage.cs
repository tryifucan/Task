using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Core.Pages.GetABook
{
    public class BookRequestDeletePage(IWebDriver driver, WebDriverWait wait) : BasePage(driver, wait)
    {
        protected readonly By deleteBtn = By.CssSelector("[type='submit']");
        protected readonly By confirmationMsg = By.XPath("//div//h3");

        public void ClickDeleteButton()
        {
            WaitAndClick(deleteBtn);
        }
        public string GetConfirmationMessage()
        {
            return GetElementText(confirmationMsg);
        }
    }
}
