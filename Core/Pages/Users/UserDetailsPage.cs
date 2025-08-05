using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Core.Pages.Users
{
    public class UserDetailsPage(IWebDriver driver, WebDriverWait wait) : BasePage(driver, wait)
    {
        private readonly By userName = By.XPath("//dl//dd");
    
        public string GetUserName()
        {
            return GetElementText(userName);
        }
    }
}
