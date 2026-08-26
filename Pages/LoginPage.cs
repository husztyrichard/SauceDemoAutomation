using OpenQA.Selenium;

namespace SauceDemoAutomation.Pages;

public class LoginPage
{
    private readonly IWebDriver driver;

    public LoginPage(IWebDriver driver)
    {
        this.driver = driver;
    }

    private IWebElement UsernameInput =>
        driver.FindElement(By.Id("user-name"));

    private IWebElement PasswordInput =>
        driver.FindElement(By.Id("password"));

    private IWebElement LoginButton =>
        driver.FindElement(By.Id("login-button"));

    public void Login(string username, string password)
    {
        UsernameInput.SendKeys(username);
        PasswordInput.SendKeys(password);
        LoginButton.Click();
    }
}