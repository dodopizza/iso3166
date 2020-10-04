using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace ISO3166.Generator
{
    public class CountriesDataScraper
    {
        private readonly ChromeDriver _chromeDriver;

        public CountriesDataScraper(ChromeDriver chromeDriver)
        {
            _chromeDriver = chromeDriver;
        }

        public async Task<Country[]> Scrape()
        {
            OpenIsoSite();

            var countries = new List<Country>();
            while (true)
            {
                await Task.Delay(TimeSpan.FromSeconds(10));
                var countriesPart = GetDataRows()
                    .Select(x =>
                    {
                        var td = x.FindElements(By.CssSelector("td"));
                        return new Country
                        {
                            Name = td[0].Text,
                            TwoLetterCode = td[2].Text,
                            ThreeLetterCode = td[3].Text,
                            NumericCode = td[4].Text,
                        };
                    });

                countries.AddRange(countriesPart);
                var nextButton = GetNextButton();
                if (nextButton == null)
                {
                    break;
                }

                nextButton.Click();
            }

            return countries.ToArray();
        }

        private void OpenIsoSite()
        {
            var isoUri = new Uri("https://www.iso.org/obp/ui/#search/code/");
            _chromeDriver
                .Navigate()
                .GoToUrl(isoUri);
        }

        private IEnumerable<IWebElement> GetDataRows()
        {
            return _chromeDriver
                .FindElementsByCssSelector(".v-widget.country-code .v-grid-body .v-grid-row");
        }

        private IWebElement GetNextButton()
        {
            return _chromeDriver
                .FindElementsByCssSelector(".v-widget.paging-align-fix")
                .First()
                .FindElements(By.CssSelector(".v-button"))
                .SkipWhile(el => !el.GetAttribute("class").Contains("v-disabled"))
                .SkipWhile(el => el.GetAttribute("class").Contains("v-disabled"))
                .FirstOrDefault();
        }
    }
}