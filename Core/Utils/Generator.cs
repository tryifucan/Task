using System;
using System.Linq;

namespace Core.Utils
{
    public static class Generator
    {
        private static readonly Random _random = new Random();
        
        public static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => chars[_random.Next(chars.Length)]).ToArray());
        }

        public static string GenerateRandomQty()
        {
            return _random.Next(1, 50).ToString();
        }

        public static string GenerateRandomName(int length = 10)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            return "TestUser_" + new string(Enumerable.Repeat(chars, length)
                .Select(s => chars[_random.Next(chars.Length)]).ToArray());
        }

        public static string GenerateRandomBookName(int length = 15)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz ";
            return "TestBook_" + new string(Enumerable.Repeat(chars, length)
                .Select(s => chars[_random.Next(chars.Length)]).ToArray()).Trim();
        }

        public static string GenerateRandomAuthor(int length = 12)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz ";
            return "Author_" + new string(Enumerable.Repeat(chars, length)
                .Select(s => chars[_random.Next(chars.Length)]).ToArray()).Trim();
        }

        public static string GenerateRandomGenre(int length = 8)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            return "Genre_" + new string(Enumerable.Repeat(chars, length)
                .Select(s => chars[_random.Next(chars.Length)]).ToArray());
        }

        public static int GenerateRandomQuantity(int min = 1, int max = 50)
        {
            return _random.Next(min, max + 1);
        }

        public static int GenerateRandomId(int min = 100000, int max = 999999)
        {
            return _random.Next(min, max + 1);
        }
    }
}
