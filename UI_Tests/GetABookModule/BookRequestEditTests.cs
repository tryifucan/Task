using Core.Config;
using Core.Utils;
using UI_Tests.Base;

namespace UI_Tests.GetABookModule
{
    public class BookRequestEditTests : BaseTests
    {

        [Test]
        [Category("GetABook")]
        [Description("Verify that a book request can be edited successfully.")]
        public void VerifyBookRequestEdit()
        {
            LoginWithDefaultCredentials();

            var userName = Generator.GenerateRandomString(10);
            var bookName = Generator.GenerateRandomString(10);

            var (bookNameCreated, author, genre, originalQuantity) = CreateAndVerifyBookRequest(userName, bookName);

            EditAndVerifyBookRequest(userName, author);
        }
    }
}

