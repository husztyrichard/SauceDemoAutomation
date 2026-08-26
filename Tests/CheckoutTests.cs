using NUnit.Framework;

namespace SauceDemoAutomation.Tests;

public class CheckoutTests : WebTestBase
{
    [SetUp]
    public void CheckoutSetup()
    {
        LoginAndOpenCart();
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

        Assert.That(
            checkoutPage.GetItemTotal(),
            Does.Contain("Item total")
        );

        checkoutPage.FinishOrder();

        Assert.That(
            checkoutPage.GetConfirmationMessage(),
            Is.EqualTo("Thank you for your order!")
        );
    }
}