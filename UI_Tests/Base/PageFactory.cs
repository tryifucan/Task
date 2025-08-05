using Core.Pages.Users;
using Core.Pages.Books;
using Core.Pages.GetABook;
using Core.Pages.Login;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace UI_Tests.Base
{
    public class PageFactory(IWebDriver driver, WebDriverWait wait)
    {
        private readonly IWebDriver _driver = driver;
        private readonly WebDriverWait _wait = wait;

        private LoginPage? _loginPage;
        private UsersPage? _usersPage;
        private CreateNewUserPage? _createUserPage;
        private EditUserPage? _editUserPage;
        private UserDetailsPage? _userDetailsPage;
        private UserDeletePage? _userDeletePage;
        private BooksPage? _booksPage;
        private CreateNewBookPage? _createNewBookPage;
        private EditBookPage? _editBookPage;
        private BookDeletePage? _bookDeletePage;
        private BookDetailsPage? _bookDetailsPage;
        private GetABookPage? _getABookPage;
        private CreateBookRequestPage? _createBookRequestPage;
        private EditBookRequestPage? _editBookRequestPage;
        private BookRequestDeletePage? _bookRequestDeletePage;

        public LoginPage LoginPage => _loginPage ??= new LoginPage(_driver, _wait);
        public UsersPage UsersPage => _usersPage ??= new UsersPage(_driver, _wait);
        public CreateNewUserPage CreateUserPage => _createUserPage ??= new CreateNewUserPage(_driver, _wait);
        public EditUserPage EditUserPage => _editUserPage ??= new EditUserPage(_driver, _wait);
        public UserDetailsPage UserDetailsPage => _userDetailsPage ??= new UserDetailsPage(_driver, _wait);
        public UserDeletePage UserDeletePage => _userDeletePage ??= new UserDeletePage(_driver, _wait);

        public BooksPage BooksPage => _booksPage ??= new BooksPage(_driver, _wait);
        public CreateNewBookPage CreateNewBookPage => _createNewBookPage ??= new CreateNewBookPage(_driver, _wait);
        public EditBookPage EditBookPage => _editBookPage ??= new EditBookPage(_driver, _wait);
        public BookDeletePage BookDeletePage => _bookDeletePage ??= new BookDeletePage(_driver, _wait);
        public BookDetailsPage BookDetailsPage => _bookDetailsPage ??= new BookDetailsPage(_driver, _wait);

        public GetABookPage GetABookPage => _getABookPage ??= new GetABookPage(_driver, _wait);
        public CreateBookRequestPage CreateBookRequestPage => _createBookRequestPage ??= new CreateBookRequestPage(_driver, _wait);
        public EditBookRequestPage EditBookRequestPage => _editBookRequestPage ??= new EditBookRequestPage(_driver, _wait);
        public BookRequestDeletePage BookRequestDeletePage => _bookRequestDeletePage ??= new BookRequestDeletePage(_driver, _wait);

        public void ResetPages()
        {
            _loginPage = null;
            _usersPage = null;
            _createUserPage = null;
            _editUserPage = null;
            _userDetailsPage = null;
            _userDeletePage = null;
            _booksPage = null;
            _createNewBookPage = null;
            _editBookPage = null;
            _bookDeletePage = null;
            _bookDetailsPage = null;
            _getABookPage = null;
            _createBookRequestPage = null;
            _editBookRequestPage = null;
            _bookRequestDeletePage = null;
        }
        public object[] GetUserPages()
        {
            return
            [
                UsersPage,
                CreateUserPage,
                EditUserPage,
                UserDetailsPage,
                UserDeletePage
            ];
        }
        public object[] GetBookPages()
        {
            return
            [
                BooksPage,
                CreateNewBookPage,
                EditBookPage,
                BookDeletePage,
                BookDetailsPage
            ];
        }
        public object[] GetGetABookPages()
        {
            return
            [
                GetABookPage,
                CreateBookRequestPage,
                EditBookRequestPage,
                BookRequestDeletePage
            ];
        }
    }
} 