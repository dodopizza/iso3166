using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ISO3166.Generator.CSharp
{
    public class CSharpGenerator : IGenerator
    {
        private static async Task GeneratePackage(
            string version,
            string templatesDir,
            string nugetDir)
        {
            const string projFilename = "ISO3166.csproj";
            var projTemplate = await File.ReadAllTextAsync($"{templatesDir}/Nuget/{projFilename}.txt");
            var projContent = projTemplate.Replace("<Version></Version>", $"<Version>{version}</Version>");
            await File.WriteAllTextAsync(nugetDir + $"/{projFilename}", projContent);

            const string extFilename = "CountryExtensions.cs";
            var extContent = await File.ReadAllTextAsync($"{templatesDir}/Nuget/{extFilename}.txt");
            await File.WriteAllTextAsync(nugetDir + $"/{extFilename}", extContent);
        }

        private static async Task GenerateCode(
            Country[] countries,
            string templatesDir,
            string nugetDir)
        {
            var enumTemplate = await File.ReadAllTextAsync($"{templatesDir}/Code/EnumEntry.txt");
            var countryTwoLetters = countries
                .Select(_ => enumTemplate
                    .Replace("{%LETTERS%}", _.TwoLetterCode)
                    .Replace("{%CODE%}", _.NumericCode));
            var countryThreeLetters = countries
                .Select(_ => enumTemplate
                    .Replace("{%LETTERS%}", _.ThreeLetterCode)
                    .Replace("{%CODE%}", _.NumericCode));
            var nameTemplate = await File.ReadAllTextAsync($"{templatesDir}/Code/DictionaryEntry.txt");
            var countryNames = countries
                .Select(_ => nameTemplate
                    .Replace("{%CODE%}", _.NumericCode)
                    .Replace("{%NAME%}", _.Name));
            var classTemplate = await File.ReadAllTextAsync($"{templatesDir}/Code/Country.cs.txt");
            var classText = classTemplate
                .Replace("{%CODES2%}", string.Join('\n', countryTwoLetters))
                .Replace("{%CODES3%}", string.Join('\n', countryThreeLetters))
                .Replace("{%NAMES%}", string.Join('\n', countryNames))
                .Replace("{%DATETIME%}", DateTime.UtcNow.ToString("O"));
            await File.WriteAllTextAsync($"{nugetDir}/Country.cs", classText);
        }

        private static async Task GenerateTests(
            string templatesDir,
            string testsDir)
        {
            const string projFilename = "ISO3166.UnitTests.csproj";
            var projTemplate = await File.ReadAllTextAsync($"{templatesDir}/Tests/{projFilename}.txt");
            await File.WriteAllTextAsync(testsDir + $"/{projFilename}", projTemplate);

            const string extFilename = "ExtensionsTests.cs";
            var extTemplate = await File.ReadAllTextAsync($"{templatesDir}/Tests/{extFilename}.txt");
            await File.WriteAllTextAsync(testsDir + $"/{extFilename}", extTemplate);
        }

        private static async Task GenerateSolution(
            string templatesDir,
            string outputDir)
        {
            const string slnFilename = "ISO3166.sln";
            var slnTemplate = await File.ReadAllTextAsync($"{templatesDir}/Solution/{slnFilename}.txt");
            await File.WriteAllTextAsync(outputDir + $"/{slnFilename}", slnTemplate);

            const string licenseFilename = "LICENSE";
            File.Copy($"./{licenseFilename}", outputDir + $"/{licenseFilename}");

            const string readmeFilename = "README.md";
            File.Copy($"./{readmeFilename}", outputDir + $"/{readmeFilename}");

            const string logoFilename = "dodopizza-logo.png";
            File.Copy($"./{logoFilename}", outputDir + $"/{logoFilename}");
        }

        public async Task Generate(Country[] countries)
        {
            const string version = "0.0.1-alpha1";

            const string templatesDir = "./CSharp/Templates";
            const string outputDir = "./Output/CSharp";

            const string nugetDir = outputDir + "/ISO3166";
            const string testsDir = outputDir + "/ISO3166.UnitTests";

            Directory.CreateDirectory(nugetDir);
            Directory.CreateDirectory(testsDir);

            await GeneratePackage(version, templatesDir, nugetDir);
            await GenerateCode(countries, templatesDir, nugetDir);
            await GenerateTests(templatesDir, testsDir);
            await GenerateSolution(templatesDir, outputDir);
        }
    }
}