# SauceDemo Selenium Automation

Automated end-to-end tests for the SauceDemo web application using C#, Selenium WebDriver and NUnit.

## Technologies

- C#
- .NET 8
- Selenium WebDriver
- NUnit
- Page Object Model (POM)
- Google Chrome

## Application Under Test

https://www.saucedemo.com/

Test credentials:

- Username: `standard_user`
- Password: `secret_sauce`

## Automated Test Scenarios

The following scenarios are covered:

1. Login with valid credentials
2. Add a product to the cart
3. Sort products by price from low to high
4. Remove a product from the cart
5. Validate that First Name is required during checkout
6. Validate that Last Name is required during checkout
7. Validate that Postal Code is required during checkout
8. Complete an order successfully

## Project Structure

```
SauceDemoAutomation/
├── Pages/
│   ├── LoginPage.cs
│   ├── InventoryPage.cs
│   ├── CartPage.cs
│   └── CheckoutPage.cs
├── Tests/
│   ├── LoginTests.cs
│   ├── ProductTests.cs
│   ├── CartTests.cs
│   ├── CheckoutTests.cs
│   └── WebTestBase.cs
├── LegacyTests.cs
├── SauceDemoAutomation.csproj
├── README.md
└── .gitignore
```

## Page Object Model

The tests use the Page Object Model to keep test scenarios separate from page-specific Selenium interactions.

- `LoginPage` handles login-related interactions.
- `InventoryPage` handles product and sorting interactions.
- `CartPage` handles cart-related interactions.
- `CheckoutPage` handles checkout and order completion.

This structure keeps the test code readable and makes page interactions easier to maintain and reuse.

## Running the Tests

Make sure the .NET 8 SDK is installed.

From the project directory, run:

dotnet restore

dotnet test

The tests use Google Chrome through Selenium WebDriver.

## Test Result

The final test suite contains 8 active automated tests, all of which pass successfully.

`LegacyTests.cs` contains the initial implementation created during the development of the solution. It is kept for reference and marked with NUnit's `Ignore` attribute, so it is not executed during the test run.

## AI Usage

AI tools were used during the implementation of this assignment.

- ChatGPT was used for initial project setup, test scenario implementation, Selenium/NUnit guidance, troubleshooting, and refactoring the tests into a Page Object Model structure.
- DeepSeek was used for an independent review of the implementation, identifying potential reliability and code quality improvements, and reviewing the final project structure and README.

The generated suggestions were reviewed, adapted where necessary, and executed locally. The final test suite was validated using `dotnet test`.

## Slow Debug Mode

For local debugging and demonstration purposes, the test suite includes an optional slow mode that allows the entire Selenium test flow to be observed in Chrome.

Slow mode is disabled by default and does not affect normal test execution.

To enable it temporarily, change the following setting in Tests/WebTestBase.cs:

private const bool SlowMode = true;

The slow mode adds a short delay before Selenium actions such as clicks, typing, and navigation. It is intended only for local debugging and visual demonstration.