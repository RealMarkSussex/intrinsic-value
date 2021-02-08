using Backend.Extensions;
using Backend.Models;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Pages.Yahoo
{
    public class AnalysisPage
    {
        private readonly IWebDriver _driver;
        public AnalysisPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void CollectGrowthEstimate(InputVariables inputVariables)
        {
            var growthEstimate = Next5YearGrowth.GetAttribute("innerText");
            double growthEstimateDouble = growthEstimate.ConvertPercentageToDouble();

            inputVariables.GrowthEstimate = growthEstimateDouble;
        }
        private IWebElement Next5YearGrowth => _driver.FindElement(By.CssSelector("#Col1-0-AnalystLeafPage-Proxy > section > table:nth-child(7) > tbody > tr:nth-child(5) > td:nth-child(2)"));
    }
}
