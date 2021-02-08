using OpenQA.Selenium;
using System;

namespace Backend.Pages.Yahoo
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

        public void ClickAgreeToCookies()
        {
            try
            {
                AgreeToCookiesButton.Click();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        private IWebElement AgreeToCookiesButton => _driver.FindElement(By.CssSelector("#consent-page > div > div > div > div.wizard-body > div.actions.couple > form > button"));
        private IWebElement SearchBar => _driver.FindElement(By.Id("yfin-usr-qry"));
        private IWebElement SubmitSearchButton => _driver.FindElement(By.Id("search-button"));
    }
}
