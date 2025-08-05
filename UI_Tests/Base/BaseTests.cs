using Core.Driver;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Core.Config;
using Core.Utils;

namespace UI_Tests.Base
{
    public class BaseTests
    {
        protected IWebDriver Driver { get; private set; }
        protected WebDriverWait Wait { get; private set; }
        protected DriverFactory DriverFactory { get; private set; }
        protected string BaseUrl => ConfigurationReader.BaseUrl;

        protected PageFactory Pages { get; private set; }

        protected List<string> CreatedUserNames { get; private set; } = new List<string>();
        protected List<string> CreatedBookNames { get; private set; } = new List<string>();

        [SetUp]
        public virtual void Setup()
        {
            DriverFactory = new DriverFactory();
            Driver = DriverFactory.GetDriver();

            Driver.Navigate().GoToUrl(BaseUrl);
            Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(ConfigurationReader.ImplicitWait));
            
            Pages = new PageFactory(Driver, Wait);
            
            CreatedUserNames.Clear();
            CreatedBookNames.Clear();
        }

        [TearDown]
        public virtual void TearDown()
        {
            CleanupTestData();

            Pages?.ResetPages();
            
            Driver.Quit();
            Driver.Dispose();
        }

        protected virtual void CleanupTestData()
        {
            try
            {
                foreach (var userName in CreatedUserNames)
                {
                    CleanupUser(userName);
                }

                foreach (var bookName in CreatedBookNames)
                {
                    CleanupBook(bookName);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Cleanup error: {ex.Message}");
            }
        }

        private void CleanupUser(string userName)
        {
            if (Pages.UsersPage.IsUserNameDisplayed(userName))
            {
                Pages.UsersPage.ClickDeleteUserButton(userName);
                Pages.UserDeletePage.ClickDeleteButton();
            }
        }

        private void CleanupBook(string bookName)
        {
            if (Pages.BooksPage.IsBookTitleDisplayed(bookName))
            {
                Pages.BooksPage.ClickDeleteBookButton(bookName);
                Pages.BookDeletePage.ClickDeleteButton();
            }

        }

        protected void LoginWithDefaultCredentials()
        {
            Pages.LoginPage.Login(ConfigurationReader.Username, ConfigurationReader.Password);
        }

        protected void NavigateToUsersPage()
        {
            Pages.UsersPage.NavigateToUsersPage();
            Assert.That(Pages.UsersPage.IsUsersTableDisplayed(), Is.True, "Users table should be displayed");
        }

        protected void NavigateToBooksPage()
        {
            Pages.BooksPage.NavigateToBooksPage();
            Assert.That(Pages.BooksPage.IsBooksTableDisplayed(), Is.True, "Books table should be displayed");
        }

        protected string CreateAndVerifyUser(string? userName = null)
        {
            userName ??= Generator.GenerateRandomName();
            
            NavigateToUsersPage();
            Pages.UsersPage.ClickAddUserButton();
            Pages.CreateUserPage.CreateNewUser(userName);
            
            Assert.That(Pages.UsersPage.IsUserNameDisplayed(userName), Is.True, 
                $"User '{userName}' should be displayed after creation");
            
            CreatedUserNames.Add(userName);
            return userName;
        }

        protected void DeleteAndVerifyUser(string userName)
        {
            NavigateToUsersPage();
            Pages.UsersPage.ClickDeleteUserButton(userName);
            Pages.UserDeletePage.ClickDeleteButton();
            
            Assert.That(Pages.UsersPage.IsUserNameDisplayed(userName), Is.False, 
                $"User '{userName}' should not be displayed after deletion");
        }

        protected string CreateAndVerifyBook(string? bookName = null)
        {
            bookName ??= Generator.GenerateRandomBookName();
            var author = Generator.GenerateRandomAuthor();
            var genre = Generator.GenerateRandomGenre();
            var quantity = Generator.GenerateRandomQuantity();
            
            NavigateToBooksPage();
            Pages.BooksPage.ClickAddBookButton();
            Pages.CreateNewBookPage.CreateNewBook(bookName, author, genre, quantity.ToString());
            
            Assert.That(Pages.BooksPage.IsBookTitleDisplayed(bookName), Is.True, 
                $"Book '{bookName}' should be displayed after creation");
            
            CreatedBookNames.Add(bookName);
            return bookName;
        }

        protected (string Name, string Author, string Genre, int Quantity) CreateAndVerifyBookWithData(string? bookName = null)
        {
            bookName ??= Generator.GenerateRandomBookName();
            var author = Generator.GenerateRandomAuthor();
            var genre = Generator.GenerateRandomGenre();
            var quantity = Generator.GenerateRandomQuantity();
            
            NavigateToBooksPage();
            Pages.BooksPage.ClickAddBookButton();
            Pages.CreateNewBookPage.CreateNewBook(bookName, author, genre, quantity.ToString());
            
            Assert.That(Pages.BooksPage.IsBookTitleDisplayed(bookName), Is.True, 
                $"Book '{bookName}' should be displayed after creation");
            
            CreatedBookNames.Add(bookName);
            return (bookName, author, genre, quantity);
        }

        protected void DeleteAndVerifyBook(string bookName)
        {
            NavigateToBooksPage();
            Pages.BooksPage.ClickDeleteBookButton(bookName);
            Pages.BookDeletePage.ClickDeleteButton();
            
            Assert.That(Pages.BooksPage.IsBookTitleDisplayed(bookName), Is.False, 
                $"Book '{bookName}' should not be displayed after deletion");
        }

        protected void NavigateToEditBookPage(string bookName)
        {
            NavigateToBooksPage();
            Pages.BooksPage.ClickEditBookButton(bookName);
        }

        protected void EditAndVerifyBook(string bookName, string newName, string newAuthor, string newGenre, int newQuantity)
        {
            NavigateToEditBookPage(bookName);
            Pages.EditBookPage.EditBook(newName, newAuthor, newGenre, newQuantity.ToString());
            
            Assert.That(Pages.BooksPage.IsBookTitleDisplayed(newName), Is.True,
                $"Book with name '{newName}' should be displayed on the Books page after editing.");
        }

        protected void EditBookAndVerifyError(string bookName, string newName, string newAuthor, string newGenre, int newQuantity)
        {
            NavigateToEditBookPage(bookName);
            Pages.EditBookPage.EditBook(newName, newAuthor, newGenre, newQuantity.ToString());
            
            VerifyValidationErrorOnEditPage();
        }

        protected void NavigateToGetABookPage()
        {
            Pages.GetABookPage.NavigateToGetABookPage();
            Assert.That(Pages.GetABookPage.IsBooksTableDisplayed(), Is.True, "Get A Book table should be displayed");
        }

        protected (string BookName, string Author, string Genre, int OriginalQuantity) CreateAndVerifyBookRequest(string userName, string bookName)
        {
            CreateAndVerifyUser(userName);
            var (_, author, genre, quantity) = CreateAndVerifyBookWithData(bookName);

            NavigateToGetABookPage();
            Pages.GetABookPage.ClickAddBookRequestButton();
            Pages.CreateBookRequestPage.CreateNewBookRequest(userName, author);
            
            Assert.That(Pages.GetABookPage.IsBooksTableDisplayed(), Is.True, "Taken Books table should be displayed after creating request");
            
            return (bookName, author, genre, quantity);
        }

        protected void DeleteAndVerifyBookRequest(string bookAuthor)
        {
            NavigateToGetABookPage();
            Pages.GetABookPage.ClickDeleteBookRequestButton(bookAuthor);
            
            Assert.That(Pages.BookRequestDeletePage.GetConfirmationMessage(), Is.EqualTo("Are you sure you want to delete this?"),
                "Confirmation message should be displayed when deleting book request");
            
            Pages.BookRequestDeletePage.ClickDeleteButton();
        }

        protected void EditAndVerifyBookRequest(string userName, string bookName)
        {
            NavigateToGetABookPage();
            Pages.GetABookPage.ClickEditBookRequestButton(bookName);
            Pages.EditBookRequestPage.EditExistingBookRequest(userName, bookName);
            
            Assert.That(Pages.GetABookPage.IsBookRequestTitleDisplayed(bookName), Is.True, 
                "Book request should be displayed after editing");
        }

        protected void NavigateToEditUserPage(string userName)
        {
            NavigateToUsersPage();
            Pages.UsersPage.ClickEditUserButton(userName);
        }

        protected void EditAndVerifyUser(string userName, string newUserName)
        {
            NavigateToEditUserPage(userName);
            Pages.EditUserPage.EditUser(newUserName);
            
            Assert.That(Pages.UsersPage.IsUserNameDisplayed(newUserName), Is.True,
                $"User with name '{newUserName}' should be displayed on the Users page after edit.");
        }

        protected void EditUserAndVerifyError(string userName, string newUserName)
        {
            NavigateToEditUserPage(userName);
            Pages.EditUserPage.EditUser(newUserName);

            VerifyValidationErrorOnEditPage();
        }

        protected void NavigateToUserDetailsPage(string userName)
        {
            NavigateToUsersPage();
            Pages.UsersPage.ClickDetailsButton(userName);
        }

        protected void VerifyValidationErrorOnCreatePage()
        {
            Assert.That(Driver.Url, Does.Contain("Create"), "Should remain on create page when validation fails");
        }

        protected void VerifyValidationErrorOnEditPage()
        {
            Assert.That(Driver.Url, Does.Contain("Edit"), "Should remain on edit page when validation fails");
        }

        protected void VerifyUserValidationError(string expectedMessage)
        {
            TestUtilities.AssertEqualIgnoreCase(expectedMessage, 
                Pages.CreateUserPage.GetErrorMessage(), "Error message should match expected value");
        }
    }
}
