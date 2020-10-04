using System.Collections.Generic;
using OpenQA.Selenium.Chrome;

namespace ISO3166.Generator
{
    public static class ChromeDriverFactory
    {
        public static ChromeDriver Create()
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
    }
}