using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SauceDemoAutomation.Pages;

namespace SauceDemoAutomation.Tests;

public class CartTests
{
    private IWebDriver driver;
    private LoginPage loginPage;
    private InventoryPage inventoryPage;
    private CartPage cartPage;

    [SetUp]
    public void Setup()
    {
        driver = new ChromeDriver();

        loginPage = new LoginPage(driver);
        inventoryPage = new InventoryPage(driver);
        cartPage = new CartPage(driver);

        driver.Navigate().GoToUrl("https://www.saucedemo.com/");
        loginPage.Login("standard_user", "secret_sauce");
    }

    [Test]
    public void RemoveProductFromCart()
    {
        inventoryPage.AddProductToCart("Sauce Labs Backpack");

        driver.FindElement(
            By.ClassName("shopping_cart_link")
        ).Click();

        cartPage.RemoveProductFromCart("Sauce Labs Backpack");

        Assert.That(
            cartPage.GetCartItemCount(),
            Is.EqualTo(0)
        );
    }

    [TearDown]
    public void TearDown()
    {
        driver.Quit();
        driver.Dispose();
    }
}