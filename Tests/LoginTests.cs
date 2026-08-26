using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SauceDemoAutomation.Pages;

namespace SauceDemoAutomation.Tests;

public class LoginTests
{
    private IWebDriver driver;
    private LoginPage loginPage;

    [SetUp]
    public void Setup()
    {
        driver = new ChromeDriver();
        loginPage = new LoginPage(driver);
    }

    [Test]
    public void LoginWithValidCredentials()
    {
        driver.Navigate().GoToUrl("https://www.saucedemo.com/");

        loginPage.Login("standard_user", "secret_sauce");

        Assert.That(
            driver.FindElement(By.ClassName("title")).Text,
            Is.EqualTo("Products")
        );
    }

    [TearDown]
    public void TearDown()
    {
        driver.Quit();
        driver.Dispose();
    }
}