using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Pages.QuickFs
{
    public class HomePage
    {
        private readonly IWebDriver _driver;
        public HomePage(IWebDriver driver)
        {
            _driver = driver;
        }
        public void SearchCompany(string ticker)
        {
            SearchBar.SendKeys(ticker);
            SubmitSearchButton.Click();
        }

        public void ClickLoginLink() => LoginInLink.Click();

        private IWebElement SearchBar => _driver.FindElement(By.CssSelector("body > app-root > user-logged-in-home > app-header-main > header > div > div > div.collapse.navbar-collapse.anonymous-navbar-collapse > div > app-search > div > div.input-group.stylish-input-group.width-100 > input"));
        private IWebElement SubmitSearchButton => _driver.FindElement(By.Id("searchSubmitBtn"));
        private IWebElement LoginInLink => _driver.FindElement(By.Id("top-nav-auth-links-sign-in"));
    }
}
