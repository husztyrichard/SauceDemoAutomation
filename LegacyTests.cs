// Initial AI-assisted implementation.
// This version was created first to validate the requested test scenarios.
// The tests were later refactored into a Page Object Model structure.
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SauceDemoAutomation;

[Ignore("Initial AI-assisted implementation - kept for reference")]

public class LegacyTests
{
    private IWebDriver driver;

    [SetUp]
    public void Setup()
    {
        driver = new ChromeDriver();
    }

   [Test]
public void LoginWithValidCredentials()
{
    driver.Navigate().GoToUrl("https://www.saucedemo.com/");

    driver.FindElement(By.Id("user-name"))
        .SendKeys("standard_user");

    driver.FindElement(By.Id("password"))
        .SendKeys("secret_sauce");

    driver.FindElement(By.Id("login-button"))
        .Click();

Assert.That(
    driver.FindElement(By.ClassName("title")).Text,
    Is.EqualTo("Products")
);  
}  
[Test]
public void AddProductToCart()
{
    driver.Navigate().GoToUrl("https://www.saucedemo.com/");

    // Login
    driver.FindElement(By.Id("user-name"))
        .SendKeys("standard_user");

    driver.FindElement(By.Id("password"))
        .SendKeys("secret_sauce");

    driver.FindElement(By.Id("login-button"))
        .Click();

    // Add Sauce Labs Backpack to cart
    driver.FindElement(By.Id("add-to-cart-sauce-labs-backpack"))
        .Click();

    // Verify cart contains 1 item
    var cartBadge = driver.FindElement(By.ClassName("shopping_cart_badge"));

    Assert.That(cartBadge.Text, Is.EqualTo("1"));
}
[Test]
public void SortProductsByPriceLowToHigh()
{
    driver.Navigate().GoToUrl("https://www.saucedemo.com/");

    // Login
    driver.FindElement(By.Id("user-name"))
        .SendKeys("standard_user");

    driver.FindElement(By.Id("password"))
        .SendKeys("secret_sauce");

    driver.FindElement(By.Id("login-button"))
        .Click();

    // Select "Price (low to high)"
    var sortDropdown = driver.FindElement(
        By.ClassName("product_sort_container")
    );

    var select = new OpenQA.Selenium.Support.UI.SelectElement(sortDropdown);

    select.SelectByValue("lohi");

    // Get all product prices
    var priceElements = driver.FindElements(
        By.ClassName("inventory_item_price")
    );

    var actualPrices = priceElements
        .Select(element =>
            decimal.Parse(
                element.Text.Replace("$", ""),
                System.Globalization.CultureInfo.InvariantCulture
            )
        )
        .ToList();

    // Create expected sorted list
    var expectedPrices = actualPrices
        .OrderBy(price => price)
        .ToList();

    // Verify sorting
    Assert.That(
        actualPrices,
        Is.EqualTo(expectedPrices)
    );
}
[Test]
public void RemoveProductFromCart()
{
    driver.Navigate().GoToUrl("https://www.saucedemo.com/");

    // Login
    driver.FindElement(By.Id("user-name"))
        .SendKeys("standard_user");

    driver.FindElement(By.Id("password"))
        .SendKeys("secret_sauce");

    driver.FindElement(By.Id("login-button"))
        .Click();

    // Add product to cart
    driver.FindElement(By.Id("add-to-cart-sauce-labs-backpack"))
        .Click();

    // Open cart
    driver.FindElement(By.ClassName("shopping_cart_link"))
        .Click();

    // Remove product from cart
    driver.FindElement(By.Id("remove-sauce-labs-backpack"))
        .Click();

    // Verify that the cart is empty
    var cartItems = driver.FindElements(By.ClassName("cart_item"));

    Assert.That(
        cartItems.Count,
        Is.EqualTo(0)
    );
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
    driver.Navigate().GoToUrl("https://www.saucedemo.com/");

    // Login
    driver.FindElement(By.Id("user-name"))
        .SendKeys("standard_user");

    driver.FindElement(By.Id("password"))
        .SendKeys("secret_sauce");

    driver.FindElement(By.Id("login-button"))
        .Click();

    // Add product to cart
    driver.FindElement(By.Id("add-to-cart-sauce-labs-backpack"))
        .Click();

    // Open cart
    driver.FindElement(By.ClassName("shopping_cart_link"))
        .Click();

    // Go to checkout
    driver.FindElement(By.Id("checkout"))
        .Click();

    // Fill checkout information
    driver.FindElement(By.Id("first-name"))
        .SendKeys(firstName);

    driver.FindElement(By.Id("last-name"))
        .SendKeys(lastName);

    driver.FindElement(By.Id("postal-code"))
        .SendKeys(postalCode);

    // Continue
    driver.FindElement(By.Id("continue"))
        .Click();

    // Verify validation message
    var errorMessage = driver.FindElement(
        By.CssSelector("[data-test='error']")
    );

    Assert.That(
        errorMessage.Text,
        Does.Contain(expectedError)
    );
}
[Test]
public void CompleteOrderSuccessfully()
{
    driver.Navigate().GoToUrl("https://www.saucedemo.com/");

    // Login
    driver.FindElement(By.Id("user-name"))
        .SendKeys("standard_user");

    driver.FindElement(By.Id("password"))
        .SendKeys("secret_sauce");

    driver.FindElement(By.Id("login-button"))
        .Click();

    // Add product to cart
    driver.FindElement(By.Id("add-to-cart-sauce-labs-backpack"))
        .Click();

    // Open cart
    driver.FindElement(By.ClassName("shopping_cart_link"))
        .Click();

    // Go to checkout
    driver.FindElement(By.Id("checkout"))
        .Click();

    // Fill checkout information
    driver.FindElement(By.Id("first-name"))
        .SendKeys("Test");

    driver.FindElement(By.Id("last-name"))
        .SendKeys("User");

    driver.FindElement(By.Id("postal-code"))
        .SendKeys("1234");

    // Continue to order overview
    driver.FindElement(By.Id("continue"))
        .Click();

    // Finish order
    driver.FindElement(By.Id("finish"))
        .Click();

    // Verify successful order
    var confirmationMessage = driver.FindElement(
        By.ClassName("complete-header")
    );

    Assert.That(
        confirmationMessage.Text,
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