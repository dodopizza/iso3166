using System;
using System.Linq;
using System.Threading.Tasks;

namespace ISO3166.Generator
{
    public static class Program
    {
        public static async Task Main()
        {
            using var chromeDriver = ChromeDriverFactory.Create();
            var countries = await new CountriesDataScraper(chromeDriver).Scrape();
            var generators = GetGenerators();
            foreach (var generator in generators)
                await generator.Generate(countries);
        }

        private static IGenerator[] GetGenerators()
        {
            var generatorType = typeof(IGenerator);
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(t => generatorType.IsAssignableFrom(t))
                .Where(t => t.IsClass)
                .Select(t => (IGenerator)
                    Activator.CreateInstance(t))
                .ToArray();
        }
    }
}