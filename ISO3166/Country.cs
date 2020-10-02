// ReSharper disable InconsistentNaming

namespace Dodo.ISO3166
{
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;

    // Don't edit it manually!
    // This class is automatically generated!
    // Created at {%DATETIME%}
    public static class Country
    {
        public enum Alpha2Code
        {
            RU = 643,
            DE = 276,
        }

        public enum Alpha3Code
        {
            RUS = 643,
            DEU = 276,
        }

        private static readonly IReadOnlyDictionary<int, string> Names = new Dictionary<int, string>(250)
        {
            [643] = "Russia",
            [276] = "Germany",
        };

        public static string GetNameByCode(int code) =>
            Names[code];
    }
}