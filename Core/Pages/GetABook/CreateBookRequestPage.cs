using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Core.Pages.GetABook
{
    public class CreateBookRequestPage(IWebDriver driver, WebDriverWait wait) : BasePage(driver, wait)
    {
        protected readonly By userIdDropdown = By.CssSelector("#UserId");
        protected readonly By bookIdDropdown = By.CssSelector("#BookId");
        protected readonly By createBtn = By.CssSelector("[type='submit']");

        public void SelectUser(string userName)
        {
            WaitUntilElementVisible(userIdDropdown);
            var userDropdown = new SelectElement(Driver.FindElement(userIdDropdown));
            userDropdown.SelectByText(userName);
        }
        public void SelectBook(string bookAuthor)
        {
            WaitUntilElementVisible(bookIdDropdown);
            var bookDropdown = new SelectElement(Driver.FindElement(bookIdDropdown));
            bookDropdown.SelectByText(bookAuthor);
        }
        public void SubmitBookRequestForm()
        {
            WaitAndClick(createBtn);
        }
        public void CreateNewBookRequest(string userName, string bookAuthor)
        {
            SelectUser(userName);
            SelectBook(bookAuthor);
            SubmitBookRequestForm();
        }
    }
}
