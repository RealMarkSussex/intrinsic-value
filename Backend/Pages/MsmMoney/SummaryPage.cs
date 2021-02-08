using OpenQA.Selenium;

namespace Backend.Pages.MsmMoney
{
    public class SummaryPage
    {
        private readonly IWebDriver _driver;
        public SummaryPage(IWebDriver driver)
        {
            _driver = driver;
        }
        public void ClickAnalysisLink() => AnalysisLink.Click();
        private IWebElement AnalysisLink => _driver.FindElement(By.CssSelector("#analysis > a"));
    }
}
