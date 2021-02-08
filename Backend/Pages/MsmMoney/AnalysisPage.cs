using Backend.Extensions;
using Backend.Models;
using OpenQA.Selenium;

namespace Backend.Pages.MsmMoney
{
    public class AnalysisPage
    {
        private readonly IWebDriver _driver;
        public AnalysisPage(IWebDriver driver)
        {
            _driver = driver;
        }
        public void CollectPEHighLowAverage(InputVariables inputVariables)
        {
            var pE5YearHigh = PE5YearHigh.GetAttribute("innerText");
            var pE5YearHighDouble = pE5YearHigh.ConvertNumberToDouble();

            var pE5YearLow = PE5YearLow.GetAttribute("innerText");
            var pE5YearLowDouble = pE5YearLow.ConvertNumberToDouble();

            inputVariables.PEHighLowAverage = (pE5YearHighDouble + pE5YearLowDouble) / 2;
        }
        public void ClickPriceRatiosTab() => PriceRatiosTab.Click();
        private IWebElement PE5YearHigh => _driver.FindElement(By.CssSelector("#main > div.content-div.fullwidth.loaded > div.main-region.maincontainer.fullwidth.stckdtl > div.dynaloadable > div > div:nth-child(4) > div > div > div.key-stats-area > div:nth-child(2) > div.table-view > div.price_ratios.tab-content.keyratios-tabview > div.keyratioscontainer > div > div > div > ul:nth-child(2) > li.center-align.middle-col.key-ratio-value > span.primary-text > p"));
        private IWebElement PE5YearLow => _driver.FindElement(By.CssSelector("#main > div.content-div.fullwidth.loaded > div.main-region.maincontainer.fullwidth.stckdtl > div.dynaloadable > div > div:nth-child(4) > div > div > div.key-stats-area > div:nth-child(2) > div.table-view > div.price_ratios.tab-content.keyratios-tabview > div.keyratioscontainer > div > div > div > ul:nth-child(3) > li.center-align.middle-col.key-ratio-value > span.primary-text > p"));
        private IWebElement PriceRatiosTab => _driver.FindElement(By.CssSelector("#main > div.content-div.fullwidth.loaded > div.main-region.maincontainer.fullwidth.stckdtl > div.dynaloadable > div > div:nth-child(4) > div > div > div.key-stats-area > ul > li:nth-child(4)"));
    }
}
