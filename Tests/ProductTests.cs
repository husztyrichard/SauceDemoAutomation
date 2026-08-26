using NUnit.Framework;

namespace SauceDemoAutomation.Tests;

public class ProductTests : WebTestBase
{
    [Test]
    public void AddProductToCart()
    {
        LoginAndGoToInventory();

        inventoryPage.AddProductToCart("Sauce Labs Backpack");

        Assert.That(
            inventoryPage.GetCartBadgeCount(),
            Is.EqualTo("1")
        );
    }

    [Test]
    public void SortProductsByPriceLowToHigh()
    {
        LoginAndGoToInventory();

        inventoryPage.SortByPriceLowToHigh();

        var actualPrices = inventoryPage.GetProductPrices();

        Assert.That(
            actualPrices,
            Is.Ordered.Ascending
        );
    }
}