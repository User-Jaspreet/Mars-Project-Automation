using OpenQA.Selenium;

public class CommonDriver
{
    private readonly IWebDriver _driver;

    public CommonDriver(IWebDriver driver)
    {
        _driver = driver;
    }

    public IWebDriver GetDriver()
    {
        return _driver;
    }
}
