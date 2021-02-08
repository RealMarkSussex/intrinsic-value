using Backend.Models;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Pages.QuickFs
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;
        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
        }
        public void Login(LoginModel loginModel)
        {
            EmailField.SendKeys(loginModel.Email);
            PasswordField.SendKeys(loginModel.Password);
            LoginButton.Click();
        }
        private IWebElement EmailField => _driver.FindElement(By.CssSelector("#loginForm > div > div:nth-child(3) > div > input"));
        private IWebElement PasswordField => _driver.FindElement(By.CssSelector("#loginForm > div > div:nth-child(4) > div > input"));
        private IWebElement LoginButton => _driver.FindElement(By.Id("submitLoginFormBtn"));

    }
}
