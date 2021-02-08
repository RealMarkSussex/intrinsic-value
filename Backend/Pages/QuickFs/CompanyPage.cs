using Backend.Extensions;
using Backend.Models;
using OpenQA.Selenium;

namespace Backend.Pages.QuickFs
{
    public class CompanyPage
    {
        private readonly IWebDriver _driver;
        public CompanyPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public InputVariables CollectInputVariables()
        {
            var roic10y = ROIC10Y.GetAttribute("innerText");
            double roiyDouble = roic10y.ConvertPercentageToDouble();

            var revenue10y = Revenue10Y.GetAttribute("innerText");
            double revenue10yDouble = revenue10y.ConvertPercentageToDouble();

            var freeCashFlow10Y = FreeCashFlow10Y.GetAttribute("innerText");
            var freeCashFlow10YDouble = freeCashFlow10Y.ConvertPercentageToDouble();

            var earningsPerShare10Y = EarningsPerShare10Y.GetAttribute("innerText");
            var earningsPerShare10YDouble = earningsPerShare10Y.ConvertPercentageToDouble();

            return new InputVariables()
            {
                ROIC10Y = roiyDouble,
                Revenue10Y = revenue10yDouble,
                FreeCashFlow10Y = freeCashFlow10YDouble,
                EarningsPerShare10Y = earningsPerShare10YDouble
            };
        }

        public void GoToBalanceSheet()
        {
            DropDown.Click();
            BalanceSheet.Click();
        }
        private IWebElement ROIC10Y => _driver.FindElement(By.CssSelector("#ksTableContainer > div > table > tbody > tr:nth-child(4) > td:nth-child(4)"));
        private IWebElement Revenue10Y => _driver.FindElement(By.CssSelector("#ksTableContainer > div > table > tbody > tr:nth-child(6) > td:nth-child(4)"));
        private IWebElement FreeCashFlow10Y => _driver.FindElement(By.CssSelector("#ksTableContainer > div > table > tbody > tr:nth-child(8) > td:nth-child(4)"));
        private IWebElement EarningsPerShare10Y => _driver.FindElement(By.CssSelector("#ksTableContainer > div > table > tbody > tr:nth-child(9) > td:nth-child(4)"));
        private IWebElement DropDown => _driver.FindElement(By.CssSelector("body > app-root > app-company > div > div > div.pageHead > div > div:nth-child(3) > div.col-xs-offset-3.col-xs-2 > select-fs-dropdown > div > button"));
        private IWebElement BalanceSheet => _driver.FindElement(By.Id("bs"));
    }
}
