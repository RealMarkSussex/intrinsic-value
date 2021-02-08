using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Pages.QuickFs
{
    public class SearchResultsPage
    {
        private readonly IWebDriver _driver;

        public SearchResultsPage(IWebDriver driver)
        {
            _driver = driver;
        }
        public void ClickFirstResult() => FirstResult.Click();
        private IWebElement FirstResult => _driver.FindElement(By.XPath("//a[@class='list-group-item'][1]"));

    }
}
