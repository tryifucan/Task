using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Core.Pages
{
    public class BasePage(IWebDriver driver, WebDriverWait wait)
    {
        private By mainMenu(string menu) => By.XPath($"//a[@href='{menu}']");
        private readonly By acceptCookies = By.XPath("//button[contains(., 'Accept')]");

        protected IWebDriver Driver { get; private set; } = driver;
        protected WebDriverWait Wait { get; private set; } = wait;

        protected void WaitUntilElementClickable(By locator, int timeoutInSeconds = 10)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeoutInSeconds));
            wait.Until(ExpectedConditions.ElementToBeClickable(locator));
        }

        protected void WaitUntilElementVisible(By locator, int timeoutInSeconds = 10)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeoutInSeconds));
            wait.Until(ExpectedConditions.ElementIsVisible(locator));
        }

        protected void WaitAndClick(By locator, int timeoutInSeconds = 10)
        {
            WaitUntilElementClickable(locator, timeoutInSeconds);
            Driver.FindElement(locator).Click();
        }

        protected void ClearAndType(By locator, string text, int timeout = 10)
        {
            WaitUntilElementVisible(locator, timeout);
            var element = Driver.FindElement(locator);
            element.Clear();
            element.SendKeys(text);
        }
        protected bool IsElementDisplayed(By locator, int timeoutInSeconds = 5)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeoutInSeconds));
                return wait.Until(driver =>
                {
                    var element = driver.FindElement(locator);
                    return element.Displayed;
                });
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
            catch (StaleElementReferenceException)
            {
                return false;
            }
        }

        protected void NavigateToMainMenu(string menu)
        {
            By menuLocator = mainMenu(menu);
            WaitUntilElementClickable(menuLocator);
            Driver.FindElement(menuLocator).Click();
        }

        public string? GetElementText(By locator)
        {
            try
            {
                var element = driver.FindElement(locator);
                return element.Text;
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        }

        public void AcceptCookies(int timeoutSeconds = 5)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds));
            var button = wait.Until(ExpectedConditions.ElementToBeClickable(acceptCookies));
            button.Click();

        }

        public List<string> GetAllNamesFromTable(By tableLocator)
        {
            var names = new List<string>();
            var table = Driver.FindElement(tableLocator);
            var rows = table.FindElements(By.CssSelector("tbody tr"));

            foreach (var row in rows)
            {
                var nameCell = row.FindElement(By.CssSelector("td"));
                names.Add(nameCell.Text.Trim());
            }

            return names;
        }
    }
}
