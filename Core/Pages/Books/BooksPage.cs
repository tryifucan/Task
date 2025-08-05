using Core.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Core.Pages.Books
{
    public class BooksPage(IWebDriver driver, WebDriverWait wait) : BasePage(driver, wait)
    {
        private readonly By booksTable = By.CssSelector(".table");
        private readonly By addBookButton = By.XPath("//a[contains(., 'Create New')]");
        private readonly By firstEditButton = By.XPath("(//a[contains(text(), 'Edit')])[1]");
        private By EditBookByTitle(string title) => By.XPath($"(//td[contains(text(), '{title}')]/parent::tr//a[contains(text(), 'Edit')])[1]");
        private readonly By firstDetailsButton = By.XPath("(//a[contains(text(), 'Details')])[1]");
        private By BookDetailsByTitle(string title) => By.XPath($"(//td[contains(text(), '{title}')]/parent::tr//a[contains(text(), 'Details')])[1]");
        private readonly By firstDeleteButton = By.XPath("(//a[contains(text(), 'Delete')])[1]");
        private By DeleteBookByTitle(string title) => By.XPath($"(//td[contains(text(), '{title}')]/parent::tr//a[contains(text(), 'Delete')])[1]");
        private By GetBookTitle(string title) => By.XPath($"//td[contains(text(), '{title}')]");

        public void ClickAddBookButton()
        {
            WaitAndClick(addBookButton);
        }
        public bool IsBooksTableDisplayed()
        {
            return IsElementDisplayed(booksTable);
        }

        public void ClickEditBookButton(string? title = null)
        {
            if (title == null)
            {
                WaitAndClick(firstEditButton);
            }
            else
            {
                var editButton = EditBookByTitle(title);
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
                var detailsButton = BookDetailsByTitle(title);
                WaitAndClick(detailsButton);
            }
        }
        public void ClickDeleteBookButton(string? title = null)
        {
            if (title == null)
            {
                WaitAndClick(firstDeleteButton);
            }
            else
            {
                var deleteButton = DeleteBookByTitle(title);
                WaitAndClick(deleteButton);
            }
        }
        public bool IsBookTitleDisplayed(string title)
        {
            var bookTitle = GetBookTitle(title);
            return IsElementDisplayed(bookTitle);
        }
        public void NavigateToBooksPage()
        {
            NavigateToMainMenu(MainMenuOptions.Books);
        }
    }
}
