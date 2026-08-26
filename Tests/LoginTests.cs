using NUnit.Framework;

namespace SauceDemoAutomation.Tests;

public class LoginTests : WebTestBase
{
    [Test]
    public void LoginWithValidCredentials()
    {
        LoginAndGoToInventory();

        Assert.That(
            loginPage.GetProductsTitle(),
            Is.EqualTo("Products")
        );
    }
}