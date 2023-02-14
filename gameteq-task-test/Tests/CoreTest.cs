using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;

namespace gameteq_task_test.Tests;

[Ignore("Core Test")]
[TestFixture]
public class CoreTest
{
    protected IWebDriver driver;
    private string _url = "https://test-task.gameteq.com/";
    
    [SetUp]
    public void SetUp()
    {
        driver = new ChromeDriver();
        var options = new ChromeOptions();
        options.AddArgument("no-sandbox");
        driver.Manage().Timeouts().PageLoad.Add(System.TimeSpan.FromSeconds(30));
        driver.Navigate().GoToUrl(_url);
    }

    [TearDown]
    public void TearDown()
    {
        driver.Quit();
    }
}
