using Core.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Core.Pages.Users
{
    public class UsersPage(IWebDriver driver, WebDriverWait wait) : BasePage(driver, wait)
    {
        private readonly By usersTable = By.CssSelector(".table");
        private readonly By addUserButton = By.XPath("//a[contains(., 'Create New')]");
        private readonly By firstEditButton = By.XPath("(//a[contains(text(), 'Edit')])[1]");
        private By EditUserByName(string name) => By.XPath($"(//td[contains(text(), '{name}')]/parent::tr//a[contains(text(), 'Edit')])[1]");

        private readonly By firstDetailsButton = By.XPath("(//a[contains(text(), 'Details')])[1]");
        private By UserDetailsByName(string name) => By.XPath($"(//td[contains(text(), '{name}')]/parent::tr//a[contains(text(), 'Details')])[1]");
        private readonly By firstDeleteButton = By.XPath("(//a[contains(text(), 'Delete')])[1]");
        private By DeleteUserByName(string name) => By.XPath($"(//td[contains(text(), '{name}')]/parent::tr//a[contains(text(), 'Delete')])[1]");
        private By GetUserName(string name) => By.XPath($"//td[contains(text(), '{name}')]");

        public void ClickAddUserButton()
        {
            WaitAndClick(addUserButton);
        }

        public bool IsUsersTableDisplayed()
        {
            return IsElementDisplayed(usersTable);
        }

        public void NavigateToUsersPage()
        {
            NavigateToMainMenu(MainMenuOptions.Users);
        }

        public void ClickEditUserButton(string? name = null)
        {
            if (name == null)
            {
                WaitAndClick(firstEditButton);
            }
            else
            {
                var editButton = EditUserByName(name);
                WaitAndClick(editButton);
            }

        }

        public void ClickDetailsButton(string? name = null)
        {
            if (name == null)
            {
                WaitAndClick(firstDetailsButton);
            }
            else
            {
                var detailsButton = UserDetailsByName(name);
                WaitAndClick(detailsButton);
            }
        }
        public void ClickDeleteUserButton(string? name = null)
        {
            if (name == null)
            {
                WaitAndClick(firstDeleteButton);
            }
            else
            {
                var deleteButton = DeleteUserByName(name);
                WaitAndClick(deleteButton);
            }
        }

        public bool IsUserNameDisplayed(string name)
        {
            var userNameElement = GetUserName(name);

            if (IsElementDisplayed(userNameElement))
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
    }
}
