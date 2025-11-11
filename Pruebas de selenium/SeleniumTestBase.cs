using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

public class SeleniumTestBase : IDisposable
{
    protected IWebDriver driver;

    public SeleniumTestBase()
    {
        var options = new ChromeOptions();
        //options.AddArgument("--headless"); // opcional: sin interfaz gráfica
        driver = new ChromeDriver(options);
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
    }

    public void Dispose()
    {
        driver.Quit();
    }
}
