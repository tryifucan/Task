using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Core.Pages.GetABook
{
    public class EditBookRequestPage(IWebDriver driver, WebDriverWait wait) : BasePage(driver, wait)
    {
        protected readonly By userIdDropdown = By.CssSelector("#UserId");
        protected readonly By bookIdDropdown = By.CssSelector("#BookId");
        protected readonly By saveBtn = By.CssSelector("[type='submit']");

        public void SelectUser(string userName)
        {
            WaitUntilElementVisible(userIdDropdown);
            var userDropdown = new SelectElement(Driver.FindElement(userIdDropdown));
            userDropdown.SelectByText(userName);
        }

        public void SelectBook(string bookTitle)
        {
            WaitUntilElementVisible(bookIdDropdown);
            var bookDropdown = new SelectElement(Driver.FindElement(bookIdDropdown));
            bookDropdown.SelectByText(bookTitle);
        }

        public void SaveChanges()
        {
            WaitAndClick(saveBtn);
        }

        public void EditExistingBookRequest(string userName, string bookTitle)
        {
            SelectUser(userName);
            SelectBook(bookTitle);
            SaveChanges();
        }
    }
}
