using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SauceDemoAutomation.Pages;

namespace SauceDemoAutomation.Tests;

public class ProductTests
{
    private IWebDriver driver;
    private LoginPage loginPage;
    private InventoryPage inventoryPage;

    [SetUp]
    public void Setup()
    {
        driver = new ChromeDriver();

        loginPage = new LoginPage(driver);
        inventoryPage = new InventoryPage(driver);

        driver.Navigate().GoToUrl("https://www.saucedemo.com/");
        loginPage.Login("standard_user", "secret_sauce");
    }

    [Test]
    public void AddProductToCart()
    {
        inventoryPage.AddProductToCart("Sauce Labs Backpack");

        var cartBadge = driver.FindElement(
            By.ClassName("shopping_cart_badge")
        );

        Assert.That(
            cartBadge.Text,
            Is.EqualTo("1")
        );
    }

    [Test]
    public void SortProductsByPriceLowToHigh()
    {
        inventoryPage.SortByPriceLowToHigh();

        var actualPrices = inventoryPage.GetProductPrices();

        var expectedPrices = actualPrices
            .OrderBy(price => price)
            .ToList();

        Assert.That(
            actualPrices,
            Is.EqualTo(expectedPrices)
        );
    }

    [TearDown]
    public void TearDown()
    {
        driver.Quit();
        driver.Dispose();
    }
}