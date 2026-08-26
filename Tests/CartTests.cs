using NUnit.Framework;

namespace SauceDemoAutomation.Tests;

public class CartTests : WebTestBase
{
    [Test]
    public void RemoveProductFromCart()
    {
        LoginAndGoToInventory();

        inventoryPage.AddProductToCart("Sauce Labs Backpack");
        inventoryPage.OpenCart();

        cartPage.RemoveProductFromCart("Sauce Labs Backpack");

        Assert.That(
            cartPage.GetCartItemCount(),
            Is.EqualTo(0)
        );
    }
}