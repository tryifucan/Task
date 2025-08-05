using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Core.Pages.Books
{
    public class BookDetailsPage(IWebDriver driver, WebDriverWait wait) : BasePage(driver, wait)
    {
        private readonly By bookName = By.XPath("//dl//dd[1]");
        private readonly By bookAuthor = By.XPath("//dl//dd[2]");
        private readonly By bookGenre = By.XPath("//dl//dd[3]");
        private readonly By bookQuantity = By.XPath("//dl//dd[4]");

        public string GetBookName()
        {
            return GetElementText(bookName);
        }
        public string GetBookAuthor()
        {
            return GetElementText(bookAuthor);
        }
        public string GetBookGenre()
        {
            return GetElementText(bookGenre);
        }
        public int GetBookQuantity()
        {
            return int.Parse(GetElementText(bookQuantity));
        }
    }
}
