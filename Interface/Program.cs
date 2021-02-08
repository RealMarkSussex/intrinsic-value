using Backend.Mappers;
using Backend.Models;
using Backend.Pages.QuickFs;
using Backend.Pages.Yahoo;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.IO;
using System.Text.Json;

namespace Interface
{
    class Program
    {
        private static IWebDriver _driver;
        private static Backend.Pages.QuickFs.HomePage _homePageQuickFs;
        private static SearchResultsPage _searchResultsPage;
        private static CompanyPage _companyPage;
        private static LoginPage _loginPage;
        private static BalanceSheetPage _balanceSheetPage;
        private static Backend.Pages.Yahoo.HomePage _homePageYahoo;
        private static SummaryPage _summaryPage;
        private static int _shortPause;

        // TODO More error handling
        static void Main(string[] args)
        {
            Console.WriteLine("Input your quickfs email: ");
            var email = Console.ReadLine();

            Console.WriteLine("Input your quickfs password: ");
            var password = Console.ReadLine();

            Console.WriteLine("Enter a comma seperated list of tickers: ");
            var commaSeperatedTickers = Console.ReadLine().Replace(" ", "");

            List<string> tickers = commaSeperatedTickers.Split(",").ToList();
            var loginModel = new LoginModel
            {
                Email = email,
                Password = password
            };

            _shortPause = 3 * 1000;
            _driver = new ChromeDriver();

            //quickfs
            _homePageQuickFs = new Backend.Pages.QuickFs.HomePage(_driver);
            _searchResultsPage = new SearchResultsPage(_driver);
            _companyPage = new CompanyPage(_driver);
            _loginPage = new LoginPage(_driver);
            _balanceSheetPage = new BalanceSheetPage(_driver);

            //yahoo
            _homePageYahoo = new Backend.Pages.Yahoo.HomePage(_driver);
            _summaryPage = new SummaryPage(_driver);

            TickersToStickers(tickers, loginModel);

            _driver.Dispose();
        }

        private static void Login(LoginModel loginModel)
        {
            Thread.Sleep(_shortPause);
            _homePageQuickFs.ClickLoginLink();
            _loginPage.Login(loginModel);
        }

        private static void TickersToStickers(List<string> tickers, LoginModel loginModel)
        {
            _driver.Navigate().GoToUrl("https://quickfs.net/");

            Login(loginModel);
            var outputVariables = (tickers.Select(ticker => TickerToSticker(ticker))).ToList();

            // TODO Write to json file
            var json = JsonSerializer.Serialize(outputVariables);
            var fileName = $"{DateTime.Today:yyyyMMdd}.json";
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            File.WriteAllText(fileName, json);
        }

        private static OutputVariables TickerToSticker(string ticker)
        {
            try
            {
                Thread.Sleep(_shortPause);
                _homePageQuickFs.SearchCompany(ticker);
                Thread.Sleep(_shortPause);
                _searchResultsPage.ClickFirstResult();
                Thread.Sleep(_shortPause);

                var inputVariables = _companyPage.CollectInputVariables();
                inputVariables.Ticker = ticker;

                _companyPage.GoToBalanceSheet();
                Thread.Sleep(_shortPause);
                _balanceSheetPage.CollectEquity10Y(inputVariables);

                _driver.Navigate().GoToUrl("https://uk.finance.yahoo.com/");

                Thread.Sleep(_shortPause);
                _homePageYahoo.ClickAgreeToCookies();
                Thread.Sleep(_shortPause);
                _homePageYahoo.SearchCompany(ticker);
                Thread.Sleep(_shortPause);

                _summaryPage.CollectCurrentTTMEPS(inputVariables);
                var inputToOutputMapper = new InputToOutput();
                _driver.Navigate().GoToUrl("https://quickfs.net/home");
                return inputToOutputMapper.inputToOutput(inputVariables);
            }
            catch(Exception exception)
            {
                Console.WriteLine("Something has gone wrong while trying to analyse data for ticker: " + ticker);
                Console.WriteLine(exception.Message);
                _driver.Navigate().GoToUrl("https://quickfs.net/home");
                return null;
            }

        }
    }
}
