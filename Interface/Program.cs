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
using Backend.Pages.MsmMoney;
using Interface.Models;

namespace Interface
{
    class Program
    {
        // quickfs
        private static IWebDriver _driver;
        private static Backend.Pages.QuickFs.HomePage _homePageQuickFs;
        private static SearchResultsPage _searchResultsPage;
        private static CompanyPage _companyPage;
        private static LoginPage _loginPage;
        private static BalanceSheetPage _balanceSheetPage;

        //yahoo
        private static Backend.Pages.Yahoo.HomePage _homePageYahoo;
        private static Backend.Pages.Yahoo.SummaryPage _summaryPageYahoo;
        private static Backend.Pages.Yahoo.AnalysisPage _analysisPageYahoo;

        //MSM
        private static Backend.Pages.MsmMoney.HomePage _homePageMsmMoney;
        private static CompaniesAndMarketsPage _companiesAndMarketsPage;
        private static Backend.Pages.MsmMoney.SummaryPage _summaryPageMsmMoney;
        private static Backend.Pages.MsmMoney.AnalysisPage _analysisPageMsmMoney;

        //Other variables
        private static int _shortPause;

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
            _driver.Manage().Window.Maximize();
            //quickfs
            _homePageQuickFs = new Backend.Pages.QuickFs.HomePage(_driver);
            _searchResultsPage = new SearchResultsPage(_driver);
            _companyPage = new CompanyPage(_driver);
            _loginPage = new LoginPage(_driver);
            _balanceSheetPage = new BalanceSheetPage(_driver);

            //yahoo
            _homePageYahoo = new Backend.Pages.Yahoo.HomePage(_driver);
            _summaryPageYahoo = new Backend.Pages.Yahoo.SummaryPage(_driver);
            _analysisPageYahoo = new Backend.Pages.Yahoo.AnalysisPage(_driver);

            //msm money
            _homePageMsmMoney = new Backend.Pages.MsmMoney.HomePage(_driver);
            _companiesAndMarketsPage = new CompaniesAndMarketsPage(_driver);
            _summaryPageMsmMoney = new Backend.Pages.MsmMoney.SummaryPage(_driver);
            _analysisPageMsmMoney = new Backend.Pages.MsmMoney.AnalysisPage(_driver);

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
            var date = DateTime.Today.ToString("yyyyMMdd");
            var uniqueId = Guid.NewGuid();

            var jsonData = new JsonData
            {
                OutputNumbers = outputVariables,
                Date = date,
                Tickers = tickers
            };
            var fileName = $"{uniqueId}{date}.json";
            var json = JsonSerializer.Serialize(jsonData);
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

                _summaryPageYahoo.CollectCurrentTTMEPS(inputVariables);
                Thread.Sleep(_shortPause);
                _summaryPageYahoo.ClickAnalysisPageLink();
                Thread.Sleep(_shortPause);
                _analysisPageYahoo.CollectGrowthEstimate(inputVariables);
                Thread.Sleep(_shortPause);

                _driver.Navigate().GoToUrl("https://www.msn.com/en-gb/money");
                Thread.Sleep(_shortPause);
                _homePageMsmMoney.ClickAgreeToCookies();
                Thread.Sleep(_shortPause);
                _homePageMsmMoney.ClickCompaniesAndMarketsLink();
                Thread.Sleep(_shortPause);
                _companiesAndMarketsPage.SearchCompany(ticker);
                Thread.Sleep(10 * 1000);
                _summaryPageMsmMoney.ClickAnalysisLink();
                Thread.Sleep(_shortPause);
                _analysisPageMsmMoney.ClickPriceRatiosTab();
                Thread.Sleep(_shortPause);
                _analysisPageMsmMoney.CollectPEHighLowAverage(inputVariables);

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
