using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace ISO3166.Generator
{
    public class Country
    {
        public string Name { get; set; }
        public string TwoLetterCode { get; set; }
        public string ThreeLetterCode { get; set; }
        public string NumericCode { get; set; }
    }

    public class Program
    {
        public static async Task Main()
        {
            // open iso site
            using var chromeDriver = CreateChromeDriver();
            OpenIsoSite(chromeDriver);

            // scrape & parse data
            var countries = new List<Country>();
            while (true)
            {
                // todo: replace this with more specific wait operations like page is loaded/reloaded
                await Task.Delay(TimeSpan.FromSeconds(10));
                var countriesPart = GetDataRows(chromeDriver)
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
                var nextButton = GetNextButton(chromeDriver);
                if (nextButton == null)
                {
                    break;
                }

                nextButton.Click();
            }

            // generate c#
            var enumTemplate = await File.ReadAllTextAsync("./Templates/Enum.txt");
            var countryTwoLetters = countries
                .Select(_ => enumTemplate
                    .Replace("{%LETTERS%}", _.TwoLetterCode)
                    .Replace("{%CODE%}", _.NumericCode));
            var countryThreeLetters = countries
                .Select(_ => enumTemplate
                    .Replace("{%LETTERS%}", _.ThreeLetterCode)
                    .Replace("{%CODE%}", _.NumericCode));

            var nameTemplate = await File.ReadAllTextAsync("./Templates/Dictionary.txt");
            var countryNames = countries
                .Select(_ => nameTemplate
                    .Replace("{%CODE%}", _.NumericCode)
                    .Replace("{%NAME%}", _.Name));

            var classTemplate = await File.ReadAllTextAsync("./Templates/Class.txt");
            var classText = classTemplate
                .Replace("{%CODES2%}", string.Join('\n', countryTwoLetters))
                .Replace("{%CODES3%}", string.Join('\n', countryThreeLetters))
                .Replace("{%NAMES%}", string.Join('\n', countryNames))
                .Replace("{%DATETIME%}", DateTime.UtcNow.ToString("O"));
            Directory.CreateDirectory("./Output");
            await File.WriteAllTextAsync("./Output/Country.cs", classText);
        }

        private static ChromeDriver CreateChromeDriver()
        {
            var chromeDriverOptions = new ChromeOptions();
            chromeDriverOptions.AddArguments(new List<string>
            {
                "no-sandbox",
                "headless",
                "disable-gpu",
                "window-size=1920,1080",
                "--whitelisted-ips"
            });
            return new ChromeDriver(chromeDriverOptions);
        }

        private static void OpenIsoSite(ChromeDriver chromeDriver)
        {
            var IsoUri = new Uri("https://www.iso.org/obp/ui/#search/code/");
            chromeDriver.Navigate().GoToUrl(IsoUri);
        }

        private static IEnumerable<IWebElement> GetDataRows(ChromeDriver chromeDriver)
        {
            return chromeDriver
                .FindElementsByCssSelector(".v-widget.country-code .v-grid-body .v-grid-row");
        }

        private static IWebElement GetNextButton(ChromeDriver chromeDriver)
        {
            return chromeDriver
                .FindElementsByCssSelector(".v-widget.paging-align-fix")
                .First()
                .FindElements(By.CssSelector(".v-button"))
                .SkipWhile(el => !el.GetAttribute("class").Contains("v-disabled"))
                .SkipWhile(el => el.GetAttribute("class").Contains("v-disabled"))
                .FirstOrDefault();
        }
    }
}