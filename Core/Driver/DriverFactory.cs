using Core.Config;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Drawing;

namespace Core.Driver
{
    public class DriverFactory
    {
        public IWebDriver Driver { get; private set; }
        public WebDriverWait Wait { get; private set; }

        public IWebDriver GetDriver()
        {
            if (Driver == null)
            {
                string browser = ConfigurationReader.BrowserName.ToLower();
                var browserSettings = ConfigurationReader.GetAppSettings().Browser;

                switch (browser)
                {
                    case "firefox":
                        var firefoxOptions = new FirefoxOptions();
                        if (browserSettings.Headless)
                        {
                            firefoxOptions.AddArgument("--headless");
                        }
                        firefoxOptions.AddArgument("-private");
                        Driver = new FirefoxDriver(firefoxOptions);
                        break;

                    case "chrome":
                    default:
                        var chromeOptions = new ChromeOptions();
                        if (browserSettings.Headless)
                        {
                            chromeOptions.AddArgument("--headless");
                        }
                        chromeOptions.AddArgument("--incognito");
                        Driver = new ChromeDriver(chromeOptions);
                        break;
                }

                Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(browserSettings.ImplicitWait);
                Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(browserSettings.PageLoadTimeout);

                if (browserSettings.StartMaximized)
                {
                    Driver.Manage().Window.Maximize();
                }
                else if (browserSettings.Resolution != null
                         && browserSettings.Resolution.Width > 0
                         && browserSettings.Resolution.Height > 0)
                {
                    Driver.Manage().Window.Size = new Size(
                        browserSettings.Resolution.Width,
                        browserSettings.Resolution.Height);
                }
                else
                {
                    Driver.Manage().Window.Maximize();
                }

                Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(browserSettings.ImplicitWait));
            }

            return Driver;
        }
    }
}
