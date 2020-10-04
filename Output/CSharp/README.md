# Dodo.ISO3166

The generator scrapes data from https://www.iso.org/iso-3166-country-codes.html then generates c# code

## Code examples

```
const int countryCode = 643;
const int countryCodeAlpha2String = "RU";
const int countryCodeAlpha2IntString = "643";
const int countryCodeAlpha3String = "RUS";
const int countryCodeAlpha3IntString = "643";

// alpha code 2 enum
var countryCodeAlpha2 = countryCode.ToAlpha2Code(); // -> Country.Alpha2Code.RU
var countryCodeAlpha2 = countryCodeAlpha3.ToAlpha2Code(); // -> Country.Alpha2Code.RU
var countryCodeAlpha2 = countryCodeAlpha2String.ToAlpha2Code(); // -> Country.Alpha2Code.RU 
var countryCodeAlpha2 = countryCodeAlpha2IntString.ToAlpha2Code(); // -> Country.Alpha2Code.RU 

// alpha code 3 enum
var countryCodeAlpha3 = countryCode.ToAlpha3Code(); // -> Country.Alpha3Code.RUS
var countryCodeAlpha3 = countryCodeAlpha2.ToAlpha3Code(); // -> Country.Alpha3Code.RUS
var countryCodeAlpha2 = countryCodeAlpha3String.ToAlpha3Code(); // -> Country.Alpha3Code.RUS
var countryCodeAlpha2 = countryCodeAlpha3IntString.ToAlpha3Code(); // -> Country.Alpha3Code.RUS

// names
var countryName = countryCodeAlpha2.GetName(); // -> "Russian Federation (the)"
var countryName = countryCodeAlpha3.GetName(); // -> "Russian Federation (the)"

// int codes
var countryCode = (int) countryCodeAlpha2; // -> 643
var countryCode = (int) countryCodeAlpha3; // -> 643

// string int codes
var countryCode = countryCodeAlpha2.ToString("D"); // -> "643"
var countryCode = countryCodeAlpha3.ToString("D"); // -> "643"

// alpha codes as strings
var countryCode = countryCodeAlpha2.ToString(); // -> "RU"
var countryCode = countryCodeAlpha3.ToString(); // -> "RUS"
```

## New version

1. Edit ISO3166.Generator code
2. Update version in ISO3166.csproj
3. Commit and push
4. PROFIT
