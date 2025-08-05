using Core.Utils;
using NUnit.Framework;

namespace UI_Tests.Base
{
    public static class TestUtilities
    {
        public static string GenerateTestUserName(string prefix = "TestUser")
        {
            return $"{prefix}_{Generator.GenerateRandomString(8)}";
        }

        public static string GenerateTestBookName(string prefix = "TestBook")
        {
            return $"{prefix}_{Generator.GenerateRandomString(8)}";
        }

        public static (string Name, string Author, string Genre, int Quantity) GenerateCompleteBookData()
        {
            return (
                GenerateTestBookName(),
                Generator.GenerateRandomAuthor(),
                Generator.GenerateRandomGenre(),
                Generator.GenerateRandomQuantity()
            );
        }

        public static void ValidateRequiredField(string value, string fieldName)
        {
            Assert.That(value, Is.Not.Null.And.Not.Empty, $"{fieldName} should not be null or empty");
        }

        public static void ValidateNumberRange(int value, int minValue, int maxValue, string fieldName)
        {
            Assert.That(value, Is.GreaterThanOrEqualTo(minValue).And.LessThanOrEqualTo(maxValue), 
                $"{fieldName} should be between {minValue} and {maxValue}");
        }

        public static void ValidateStringLength(string value, int maxLength, string fieldName)
        {
            Assert.That(value?.Length, Is.LessThanOrEqualTo(maxLength), 
                $"{fieldName} should not exceed {maxLength} characters");
        }

        public static void ExecuteWithRetry(Action action, int maxRetries = 3, int delayMs = 1000)
        {
            var lastException = new Exception();
            
            for (int i = 0; i <= maxRetries; i++)
            {
                try
                {
                    action();
                    return;
                }
                catch (Exception ex)
                {
                    lastException = ex;
                    if (i < maxRetries)
                    {
                        Thread.Sleep(delayMs);
                    }
                }
            }
            
            throw lastException;
        }

        public static T ExecuteWithRetry<T>(Func<T> func, int maxRetries = 3, int delayMs = 1000)
        {
            var lastException = new Exception();
            
            for (int i = 0; i <= maxRetries; i++)
            {
                try
                {
                    return func();
                }
                catch (Exception ex)
                {
                    lastException = ex;
                    if (i < maxRetries)
                    {
                        Thread.Sleep(delayMs);
                    }
                }
            }
            
            throw lastException;
        }

        public static void AssertTrue(bool condition, string message)
        {
            Assert.That(condition, Is.True, message);
        }

        public static void AssertFalse(bool condition, string message)
        {
            Assert.That(condition, Is.False, message);
        }

        public static void AssertEqualIgnoreCase(string expected, string actual, string message)
        {
            Assert.That(actual, Is.EqualTo(expected).IgnoreCase, message);
        }
    }
} 