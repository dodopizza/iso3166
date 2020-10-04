using System;
using Dodo.ISO3166;
using NUnit.Framework;

namespace ISO3166.UnitTests
{
    public class ExtensionsTests
    {
        [TestCase("RU", Country.Alpha2Code.RU)]
        [TestCase("643", Country.Alpha2Code.RU)]
        [TestCase("DE", Country.Alpha2Code.DE)]
        [TestCase("276", Country.Alpha2Code.DE)]
        public void GivenValidStringCountry_WhenCalledToAlpha2Code_ThenCorrectAlpha2CodeEnumReturned(
            string @string,
            Country.Alpha2Code @enum)
        {
            Assert.AreEqual(@enum, @string.ToAlpha2Code());
        }

        [TestCase("RUS", Country.Alpha3Code.RUS)]
        [TestCase("643", Country.Alpha3Code.RUS)]
        [TestCase("DEU", Country.Alpha3Code.DEU)]
        [TestCase("276", Country.Alpha3Code.DEU)]
        public void GivenValidStringCountry_WhenCalledToAlpha3Code_ThenCorrectAlpha3CodeEnumReturned(
            string @string,
            Country.Alpha3Code @enum)
        {
            Assert.AreEqual(@enum, @string.ToAlpha3Code());
        }

        [TestCase(643, Country.Alpha2Code.RU)]
        [TestCase(276, Country.Alpha2Code.DE)]
        public void GivenValidIntCountry_WhenCalledToAlpha2Code_ThenCorrectCountryCodeEnumReturned(
            int @int,
            Country.Alpha2Code @enum)
        {
            Assert.AreEqual(@enum, @int.ToAlpha2Code());
        }

        [TestCase(643, Country.Alpha3Code.RUS)]
        [TestCase(276, Country.Alpha3Code.DEU)]
        public void GivenValidIntCountry_WhenCalledToAlpha3Code_ThenCorrectCountryCodeEnumReturned(
            int @int,
            Country.Alpha3Code @enum)
        {
            Assert.AreEqual(@enum, @int.ToAlpha3Code());
        }

        [TestCase("-12315326")]
        [TestCase("-1")]
        [TestCase("0")]
        [TestCase("6666")]
        [TestCase("Invalid currency!")]
        [TestCase(" ")]
        public void GivenInvalidStringCountry_WhenCalledToAlpha2Code_ThenInvalidOperationExceptionThrown(
            string @string)
        {
            Assert.Throws<InvalidOperationException>(() => @string.ToAlpha2Code());
        }

        [TestCase("-12315326")]
        [TestCase("-1")]
        [TestCase("0")]
        [TestCase("6666")]
        [TestCase("Invalid currency!")]
        [TestCase(" ")]
        public void GivenInvalidStringCountry_WhenCalledToAlpha3Code_ThenInvalidOperationExceptionThrown(
            string @string)
        {
            Assert.Throws<InvalidOperationException>(() => @string.ToAlpha3Code());
        }

        [TestCase(int.MinValue)]
        [TestCase(0)]
        [TestCase(6666)]
        [TestCase(int.MaxValue)]
        public void GivenInvalidIntCountry_WhenCalledToAlpha2Code_ThenInvalidOperationExceptionThrown(int @int)
        {
            Assert.Throws<InvalidOperationException>(() => @int.ToAlpha2Code());
        }

        [TestCase(int.MinValue)]
        [TestCase(0)]
        [TestCase(6666)]
        [TestCase(int.MaxValue)]
        public void GivenInvalidIntCountry_WhenCalledToAlpha3Code_ThenInvalidOperationExceptionThrown(int @int)
        {
            Assert.Throws<InvalidOperationException>(() => @int.ToAlpha3Code());
        }

        [TestCase(Country.Alpha2Code.RU, "Russian Federation (the)")]
        [TestCase(Country.Alpha2Code.DE, "Germany")]
        public void GivenAlpha2Code_WhenCalledGetName_ThenCorrectNameReturned(
            Country.Alpha2Code @enum,
            string name)
        {
            Assert.AreEqual(name, @enum.GetName());
        }

        [TestCase(Country.Alpha3Code.RUS, "Russian Federation (the)")]
        [TestCase(Country.Alpha3Code.DEU, "Germany")]
        public void GivenAlpha3Code_WhenCalledGetName_ThenCorrectNameReturned(
            Country.Alpha3Code @enum,
            string name)
        {
            Assert.AreEqual(name, @enum.GetName());
        }
    }
}