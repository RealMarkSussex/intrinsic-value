using Backend.Exceptions;
using Backend.Models;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Pages.Yahoo
{
    public class SummaryPage
    {
        private readonly IWebDriver _driver;
        public SummaryPage(IWebDriver driver)
        {
            _driver = driver;
        }
        public void CollectCurrentTTMEPS(InputVariables inputVariables)
        {
            var currentTTMEPS = CurrentTTMEPS.GetAttribute("innerText");
            var currentTTMEPSDouble = Convert.ToDouble(currentTTMEPS);

            var title = Title.GetAttribute("innerText");
            if(!title.ToLower().Contains($"({inputVariables.Ticker.ToLower()})"))
            {
                throw new YahooIsStupidException("Sorry but Yahoo has searched for a stock that does not match your ticker");
            }
            inputVariables.CurrentTTMEPS = currentTTMEPSDouble;
        }
        public void ClickAnalysisPageLink() => AnalysisPageLink.Click();
        private IWebElement CurrentTTMEPS => _driver.FindElement(By.CssSelector("#quote-summary > div.D\\(ib\\).W\\(1\\/2\\).Bxz\\(bb\\).Pstart\\(12px\\).Va\\(t\\).ie-7_D\\(i\\).ie-7_Pos\\(a\\).smartphone_D\\(b\\).smartphone_W\\(100\\%\\).smartphone_Pstart\\(0px\\).smartphone_BdB.smartphone_Bdc\\(\\$seperatorColor\\) > table > tbody > tr:nth-child(4) > td.Ta\\(end\\).Fw\\(600\\).Lh\\(14px\\) > span"));
        private IWebElement Title => _driver.FindElement(By.CssSelector("#quote-header-info > div.Mt\\(15px\\) > div.D\\(ib\\).Mt\\(-5px\\).Mend\\(20px\\).Maw\\(56\\%\\)--tab768.Maw\\(52\\%\\).Ov\\(h\\).smartphone_Maw\\(85\\%\\).smartphone_Mend\\(0px\\) > div.D\\(ib\\) > h1"));
        private IWebElement AnalysisPageLink => _driver.FindElement(By.CssSelector("#quote-nav > ul > li:nth-child(8) > a"));
    }
}
