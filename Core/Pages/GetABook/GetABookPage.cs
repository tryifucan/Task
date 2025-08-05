using Core.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Core.Pages.GetABook
{
    public class GetABookPage(IWebDriver driver, WebDriverWait wait) : BasePage(driver, wait)
    {
        private readonly By booksTable = By.CssSelector(".table");
        private readonly By addBookRequestButton = By.XPath("//a[contains(., 'Create New')]");
        private readonly By firstEditButton = By.XPath("(//a[contains(text(), 'Edit')])[1]");
        private By EditBookRequestByTitle(string title) => By.XPath($"(//td[contains(text(), '{title}')]/parent::tr//a[contains(text(), 'Edit')])[1]");
        private readonly By firstDetailsButton = By.XPath("(//a[contains(text(), 'Details')])[1]");
        private By BookRequestDetailsByTitle(string title) => By.XPath($"(//td[contains(text(), '{title}')]/parent::tr//a[contains(text(), 'Details')])[1]");
        private readonly By firstDeleteButton = By.XPath("(//a[contains(text(), 'Delete')])[1]");
        private By DeleteBookRequestByTitle(string title) => By.XPath($"(//td[contains(text(), '{title}')]/parent::tr//a[contains(text(), 'Delete')])[1]");
        private By GetBookRequestTitle(string title) => By.XPath($"//td[contains(text(), '{title}')]");

        public void NavigateToGetABookPage()
        {
            NavigateToMainMenu(MainMenuOptions.GetaBook);
        }
        public void ClickAddBookRequestButton()
        {
            WaitAndClick(addBookRequestButton);
        }
        public bool IsBooksTableDisplayed()
        {
            return IsElementDisplayed(booksTable);
        }
        public void ClickEditBookRequestButton(string? title = null)
        {
            if (title == null)
            {
                WaitAndClick(firstEditButton);
            }
            else
            {
                var editButton = EditBookRequestByTitle(title);
                WaitAndClick(editButton);
            }
        }
        public void ClickDetailsButton(string? title = null)
        {
            if (title == null)
            {
                WaitAndClick(firstDetailsButton);
            }
            else
            {
                var detailsButton = BookRequestDetailsByTitle(title);
                WaitAndClick(detailsButton);
            }
        }
        public void ClickDeleteBookRequestButton(string? author = null)
        {
            if (author == null)
            {
                WaitAndClick(firstDeleteButton);
            }
            else
            {
                var deleteButton = DeleteBookRequestByTitle(author);
                WaitAndClick(deleteButton);
            }
        }
        public bool IsBookRequestTitleDisplayed(string title)
        {
            var bookRequestTitle = GetBookRequestTitle(title);
            return IsElementDisplayed(bookRequestTitle);
        }
    }
}
