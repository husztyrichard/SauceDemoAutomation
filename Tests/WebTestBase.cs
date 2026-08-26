using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Events;
using SauceDemoAutomation.Pages;

namespace SauceDemoAutomation.Tests;

public class WebTestBase
{
    protected const string BaseUrl = "https://www.saucedemo.com/";
    protected const string Username = "standard_user";
    protected const string Password = "secret_sauce";

    private const bool SlowMode = false;
    private const int SlowDelayMs = 600;

    protected IWebDriver driver = null!;

    protected LoginPage loginPage = null!;
    protected InventoryPage inventoryPage = null!;
    protected CartPage cartPage = null!;
    protected CheckoutPage checkoutPage = null!;

    [SetUp]
    public void Setup()
    {
        var options = new ChromeOptions();

        options.AddUserProfilePreference("credentials_enable_service", false);
        options.AddUserProfilePreference("profile.password_manager_leak_detection", false);

        var innerDriver = new ChromeDriver(options);
        innerDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);

        driver = SlowMode
            ? WrapWithSlowDriver(innerDriver)
            : innerDriver;

        loginPage = new LoginPage(driver);
        inventoryPage = new InventoryPage(driver);
        cartPage = new CartPage(driver);
        checkoutPage = new CheckoutPage(driver);
    }

    private static IWebDriver WrapWithSlowDriver(IWebDriver innerDriver)
    {
        var slowDriver = new EventFiringWebDriver(innerDriver);

        slowDriver.ElementClicking += (_, _) => Thread.Sleep(SlowDelayMs);
        slowDriver.ElementValueChanging += (_, _) => Thread.Sleep(SlowDelayMs);
        slowDriver.Navigating += (_, _) => Thread.Sleep(SlowDelayMs);

        return slowDriver;
    }

    [TearDown]
    public void TearDown()
    {
        driver.Quit();
    }

    protected void LoginAndGoToInventory()
    {
        driver.Navigate().GoToUrl(BaseUrl);
        loginPage.Login(Username, Password);
    }

    protected void LoginAndOpenCart()
    {
        LoginAndGoToInventory();
        inventoryPage.AddProductToCart("Sauce Labs Backpack");
        inventoryPage.OpenCart();
    }
}