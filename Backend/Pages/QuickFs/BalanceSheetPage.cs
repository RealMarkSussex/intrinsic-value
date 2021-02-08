using Backend.Models;
using OpenQA.Selenium;
using System;
using Backend.Extensions;

namespace Backend.Pages.QuickFs
{
    public class BalanceSheetPage
    {
        private readonly IWebDriver _driver;
        public BalanceSheetPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void CollectEquity10Y(InputVariables inputVariables)
        {
            var firstEquity = FirstEquity.GetAttribute("innerText");
            var firstEquityDouble = firstEquity.ConvertNumberToDouble();

            var secondEquity = SecondEquity.GetAttribute("innerText");
            var secondEquityDouble = secondEquity.ConvertNumberToDouble();

            if(firstEquityDouble <= 0 || secondEquityDouble < 0)
            {
                inputVariables.Equity10Y = -1;
            } 
            else
            {
                inputVariables.Equity10Y = (Math.Pow(secondEquityDouble / firstEquityDouble, 1.0 / 9.0) - 1) * 100;
            }
        }

        private IWebElement FirstEquity => _driver.FindElement(By.CssSelector("#bs-table > tbody > tr.total-row-no-skip > td:nth-child(2)"));
        private IWebElement SecondEquity => _driver.FindElement(By.CssSelector("#bs-table > tbody > tr.total-row-no-skip > td:nth-child(12)"));

    }
}
