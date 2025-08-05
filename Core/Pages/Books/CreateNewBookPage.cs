using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Core.Pages.Books
{
    public class CreateNewBookPage(IWebDriver driver, WebDriverWait wait) : BasePage(driver, wait)
    {
        private readonly By nameField = By.CssSelector("#Name");
        private readonly By authorField = By.CssSelector("#Author");
        private readonly By genreField = By.CssSelector("#Genre");
        private readonly By quantityField = By.CssSelector("#Quontity");
        private readonly By createBookBtn = By.CssSelector("[type='submit']");
        
        public void EnterName(string name)
        {
            WaitUntilElementVisible(nameField);
            ClearAndType(nameField, name);
        }
        public void EnterAuthor(string author)
        {
            WaitUntilElementVisible(authorField);
            ClearAndType(authorField, author);
        }
        public void EnterGenre(string genre)
        {
            WaitUntilElementVisible(genreField);
            ClearAndType(genreField, genre);
        }
        public void EnterQuantity(string quantity)
        {
            WaitUntilElementVisible(quantityField);
            ClearAndType(quantityField, quantity);
        }
        public void ClickCreateBookButton()
        {
            WaitAndClick(createBookBtn);
        }
        public void CreateNewBook(string name, string author, string genre, string quantity)
        {
            EnterName(name);
            EnterAuthor(author);
            EnterGenre(genre);
            EnterQuantity(quantity);
            ClickCreateBookButton();
        }
    }
}
