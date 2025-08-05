using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Core.Pages.Books
{
    public class BookDeletePage(IWebDriver driver, WebDriverWait wait) : BasePage(driver, wait)
    {
        protected readonly By deleteBtn = By.CssSelector("[type='submit']");
        protected readonly By confirmationMsg = By.XPath("//div//h3");
        private readonly By backToListBtn = By.XPath("//form//a");


        public void ClickDeleteButton()
        {
            WaitAndClick(deleteBtn);
        }

        public void ClickBackToListButton()
        {
            WaitAndClick(backToListBtn);
        }
    }
}
