using Microsoft.Extensions.Configuration;

namespace Core.Config
{
    public static class ConfigurationReader
    {
        private static readonly AppSettings _settings;

        static ConfigurationReader()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            _settings = config.Get<AppSettings>();
        }

        public static string Environment => _settings.Environment;
        public static string BaseUrl => _settings.Environments[_settings.Environment];

        public static string BrowserName => _settings.Browser.Name;
        public static bool IsHeadless => _settings.Browser.Headless;
        public static int ImplicitWait => _settings.Browser.ImplicitWait;
        public static int PageLoadTimeout => _settings.Browser.PageLoadTimeout;
        public static int ScriptTimeout => _settings.Browser.ScriptTimeout;
        public static string Username => _settings.Credentials.Username;
        public static string Password => _settings.Credentials.Password;
        public static string ApiBaseUrl => _settings.API.BaseUrl;
        public static int ApiTimeout => _settings.API.Timeout;

        public static AppSettings GetAppSettings() => _settings;
    }
}
