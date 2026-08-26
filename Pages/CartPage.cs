using OpenQA.Selenium;

namespace SauceDemoAutomation.Pages;

public class CartPage
{
    private readonly IWebDriver driver;

    public CartPage(IWebDriver driver)
    {
        this.driver = driver;
    }

    private IWebElement CheckoutButton =>
        driver.FindElement(By.Id("checkout"));

    public void RemoveProductFromCart(string productName)
    {
        string productId = productName
            .ToLower()
            .Replace(" ", "-");

        driver.FindElement(
            By.Id($"remove-{productId}")
        ).Click();
    }

    public int GetCartItemCount()
    {
        return driver.FindElements(
            By.ClassName("cart_item")
        ).Count;
    }

    public void GoToCheckout()
    {
        CheckoutButton.Click();
    }
}