using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SauceDemoAutomation.Pages;

public class CheckoutPage
{
    private readonly IWebDriver driver;

    public CheckoutPage(IWebDriver driver)
    {
        this.driver = driver;
    }

    private IWebElement FirstNameInput =>
        driver.FindElement(By.Id("first-name"));

    private IWebElement LastNameInput =>
        driver.FindElement(By.Id("last-name"));

    private IWebElement PostalCodeInput =>
        driver.FindElement(By.Id("postal-code"));

    private IWebElement ContinueButton =>
        driver.FindElement(By.Id("continue"));

    private IWebElement FinishButton =>
        driver.FindElement(By.Id("finish"));

    private IWebElement ErrorMessage =>
        driver.FindElement(By.CssSelector("[data-test='error']"));

    private IWebElement ConfirmationMessage =>
        driver.FindElement(By.ClassName("complete-header"));

    private IWebElement ItemTotalLabel =>
        driver.FindElement(By.ClassName("summary_subtotal_label"));

    public void FillCheckoutInformation(
        string firstName,
        string lastName,
        string postalCode)
    {
        FirstNameInput.SendKeys(firstName);
        LastNameInput.SendKeys(lastName);
        PostalCodeInput.SendKeys(postalCode);
    }

    public void Continue()
    {
        ContinueButton.Click();
    }

    public void FinishOrder()
    {
        FinishButton.Click();
    }

    public string GetErrorMessage()
    {
        return ErrorMessage.Text;
    }

    public string GetItemTotal()
    {
        return ItemTotalLabel.Text;
    }

    public string GetConfirmationMessage()
    {
        var wait = new WebDriverWait(
            driver,
            TimeSpan.FromSeconds(10)
        );

        wait.Until(d => ConfirmationMessage.Displayed);

        return ConfirmationMessage.Text;
    }
}