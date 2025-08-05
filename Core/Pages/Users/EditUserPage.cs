using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Core.Pages.Users
{
    public class EditUserPage(IWebDriver driver, WebDriverWait wait) : BasePage(driver, wait)
    {
        private readonly By nameInput = By.CssSelector("#Name");
        private readonly By saveBtn = By.CssSelector("[type='submit']");

        public void ClickSaveButton()
        {
            WaitAndClick(saveBtn);
        }

        public void FillNameInput(string name)
        {
            ClearAndType(nameInput, name);
        }

        public void EditUser(string name)
        {
            FillNameInput(name);
            ClickSaveButton();
        }
    }
}
