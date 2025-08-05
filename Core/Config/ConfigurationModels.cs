namespace Core.Config
{
    public class AppSettings
    {
        public string Environment { get; set; }
        public Dictionary<string, string> Environments { get; set; }
        public BrowserSettings Browser { get; set; }
        public Credentials Credentials { get; set; }
        public ApiSettings API { get; set; }
    }

    public class BrowserSettings
    {
        public string Name { get; set; }
        public bool Headless { get; set; }
        public int ImplicitWait { get; set; }
        public int PageLoadTimeout { get; set; }
        public int ScriptTimeout { get; set; }
        public bool StartMaximized { get; set; }
        public ResolutionSettings Resolution { get; set; }
    }

    public class Credentials
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class ResolutionSettings
    {
        public int Width { get; set; }
        public int Height { get; set; }
    }

    public class ApiSettings
    {
        public string BaseUrl { get; set; }
        public int Timeout { get; set; }
    }
}
