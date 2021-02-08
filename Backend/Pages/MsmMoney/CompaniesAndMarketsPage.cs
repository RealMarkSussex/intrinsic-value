using OpenQA.Selenium;

namespace Backend.Pages.MsmMoney
{
    public class CompaniesAndMarketsPage
    {
        private readonly IWebDriver _driver;
        public CompaniesAndMarketsPage(IWebDriver driver)
        {
            _driver = driver;
        }
        public void SearchCompany(string ticker)
        {
            SearchBar.SendKeys(ticker);
            SearchBar.SendKeys(Keys.Enter);
        }
        private IWebElement SearchBar => _driver.FindElement(By.Id("finance-autosuggest"));
        private IWebElement SubmitSearchButton => _driver.FindElement(By.CssSelector("#fi-module1 > div > span"));
    }
}
