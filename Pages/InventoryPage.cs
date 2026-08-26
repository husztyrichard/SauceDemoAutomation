using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SauceDemoAutomation.Pages;

public class InventoryPage
{
    private readonly IWebDriver driver;

    public InventoryPage(IWebDriver driver)
    {
        this.driver = driver;
    }

    private IWebElement SortDropdown =>
        driver.FindElement(By.ClassName("product_sort_container"));

    private IReadOnlyCollection<IWebElement> ProductPrices =>
        driver.FindElements(By.ClassName("inventory_item_price"));

    public void AddProductToCart(string productName)
    {
        string productId = productName
            .ToLower()
            .Replace(" ", "-");

        driver.FindElement(
            By.Id($"add-to-cart-{productId}")
        ).Click();
    }

    public void SortByPriceLowToHigh()
    {
        var select = new SelectElement(SortDropdown);
        select.SelectByValue("lohi");
    }

    public List<decimal> GetProductPrices()
    {
        return ProductPrices
            .Select(element =>
                decimal.Parse(
                    element.Text.Replace("$", ""),
                    System.Globalization.CultureInfo.InvariantCulture
                )
            )
            .ToList();
    }
}