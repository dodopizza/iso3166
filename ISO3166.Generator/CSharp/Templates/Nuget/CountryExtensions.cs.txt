using System;

namespace Dodo.ISO3166
{
    public static class CountryExtensions
    {
        public static Country.Alpha3Code ToAlpha3Code(this int code) =>
            Enum.IsDefined(typeof(Country.Alpha3Code), code)
                ? (Country.Alpha3Code) code
                : throw new InvalidOperationException($"\"{code}\" isn't valid {nameof(Country.Alpha3Code)}");

        public static Country.Alpha3Code ToAlpha3Code(this string code) =>
            Enum.TryParse<Country.Alpha3Code>(code, out var @enum) &&
            Enum.IsDefined(typeof(Country.Alpha3Code), (int) @enum)
                ? @enum
                : throw new InvalidOperationException($"\"{code}\" isn't valid {nameof(Country.Alpha3Code)}");

        public static Country.Alpha2Code ToAlpha2Code(this int code) =>
            Enum.IsDefined(typeof(Country.Alpha2Code), code)
                ? (Country.Alpha2Code) code
                : throw new InvalidOperationException($"\"{code}\" isn't valid {nameof(Country.Alpha2Code)}");

        public static Country.Alpha2Code ToAlpha2Code(this string code) =>
            Enum.TryParse<Country.Alpha2Code>(code, out var @enum) &&
            Enum.IsDefined(typeof(Country.Alpha3Code), (int) @enum)
                ? @enum
                : throw new InvalidOperationException($"\"{code}\" isn't valid {nameof(Country.Alpha2Code)}");

        public static Country.Alpha2Code ToAlpha2Code(this Country.Alpha3Code code) =>
            (Country.Alpha2Code) code;

        public static Country.Alpha3Code ToAlpha3Code(this Country.Alpha2Code code) =>
            (Country.Alpha3Code) code;

        public static string GetName(this Country.Alpha2Code code) =>
            Country.GetNameByCode((int) code);

        public static string GetName(this Country.Alpha3Code code) =>
            Country.GetNameByCode((int) code);
    }
}