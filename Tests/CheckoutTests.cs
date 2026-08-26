using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SauceDemoAutomation.Pages;

namespace SauceDemoAutomation.Tests;

public class CheckoutTests
{
    private IWebDriver driver;
    private LoginPage loginPage;
    private InventoryPage inventoryPage;
    private CartPage cartPage;
    private CheckoutPage checkoutPage;

    [SetUp]
    public void Setup()
    {
        driver = new ChromeDriver();

        loginPage = new LoginPage(driver);
        inventoryPage = new InventoryPage(driver);
        cartPage = new CartPage(driver);
        checkoutPage = new CheckoutPage(driver);

        driver.Navigate().GoToUrl("https://www.saucedemo.com/");
        loginPage.Login("standard_user", "secret_sauce");

        inventoryPage.AddProductToCart("Sauce Labs Backpack");

        driver.FindElement(
            By.ClassName("shopping_cart_link")
        ).Click();

        cartPage.GoToCheckout();
    }

    [TestCase("", "Test", "1234", "First Name is required")]
    [TestCase("Test", "", "1234", "Last Name is required")]
    [TestCase("Test", "User", "", "Postal Code is required")]
    public void CheckoutRequiredFieldsValidation(
        string firstName,
        string lastName,
        string postalCode,
        string expectedError)
    {
        checkoutPage.FillCheckoutInformation(
            firstName,
            lastName,
            postalCode
        );

        checkoutPage.Continue();

        Assert.That(
            checkoutPage.GetErrorMessage(),
            Does.Contain(expectedError)
        );
    }

    [Test]
    public void CompleteOrderSuccessfully()
    {
        checkoutPage.FillCheckoutInformation(
            "Test",
            "User",
            "1234"
        );

        checkoutPage.Continue();
        checkoutPage.FinishOrder();

        Assert.That(
            checkoutPage.GetConfirmationMessage(),
            Is.EqualTo("Thank you for your order!")
        );
    }

    [TearDown]
    public void TearDown()
    {
        driver.Quit();
        driver.Dispose();
    }
}