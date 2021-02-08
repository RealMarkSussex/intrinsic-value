using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Pages.MsmMoney
{
    public class HomePage
    {
        private readonly IWebDriver _driver;
        public HomePage(IWebDriver driver)
        {
            _driver = driver;
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
        public void ClickCompaniesAndMarketsLink() => CompaniesAndMarketsLink.Click();
        private IWebElement AgreeToCookiesButton => _driver.FindElement(By.Id("onetrust-accept-btn-handler"));
        private IWebElement CompaniesAndMarketsLink => _driver.FindElement(By.CssSelector("#nav > div > ul.supernav > li:nth-child(2) > a"));
    }
}
