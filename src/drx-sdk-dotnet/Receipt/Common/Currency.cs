#region copyright
// Copyright 2016 Digital Receipt Exchange Limited
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// 
#endregion

using Net.Dreceiptx.Users;

namespace Net.Dreceiptx.Receipt.Common
{
    //TODO: Add conversion methods like in java side of things
    public enum Currency
    {
        //@SerializedName("AFN")
        [DrxEnumExtendedInformation("AFN", "Afghani")]
        Afghani,
        //@SerializedName("EUR")
        [DrxEnumExtendedInformation("EUR", "Euro")]
        Euro,
        //@SerializedName("ALL")
        [DrxEnumExtendedInformation("ALL", "Lek")]
        Lek,
        //@SerializedName("DZD")
        [DrxEnumExtendedInformation("DZD", "Algerian Dinar")]
        Algerian_Dinar,
        //@SerializedName("USD")
        [DrxEnumExtendedInformation("USD", "US Dollar")]
        US_Dollar,
        //@SerializedName("AOA")
        [DrxEnumExtendedInformation("AOA", "Kwanza")]
        Kwanza,
        //@SerializedName("XCD")
        [DrxEnumExtendedInformation("XCD", "East Caribbean Dollar")]
        East_Caribbean_Dollar,
        //@SerializedName("ARS")
        [DrxEnumExtendedInformation("ARS", "Argentine Peso")]
        Argentine_Peso,
        //@SerializedName("AMD")
        [DrxEnumExtendedInformation("AMD", "Armenian Dram")]
        Armenian_Dram,
        //@SerializedName("AWG")
        [DrxEnumExtendedInformation("AWG", "Aruban Florin")]
        Aruban_Florin,
        //@SerializedName("AUD")
        [DrxEnumExtendedInformation("AUD", "Australian Dollar")]
        Australian_Dollar,
        //@SerializedName("AZN")
        [DrxEnumExtendedInformation("AZN", "Azerbaijanian Manat")]
        Azerbaijanian_Manat,
        //@SerializedName("BSD")
        [DrxEnumExtendedInformation("BSD", "Bahamian Dollar")]
        Bahamian_Dollar,
        //@SerializedName("BHD")
        [DrxEnumExtendedInformation("BHD", "Bahraini Dinar")]
        Bahraini_Dinar,
        //@SerializedName("BDT")
        [DrxEnumExtendedInformation("BDT", "Taka")]
        Taka,
        //@SerializedName("BBD")
        [DrxEnumExtendedInformation("BBD", "Barbados Dollar")]
        Barbados_Dollar,
        //@SerializedName("BYR")
        [DrxEnumExtendedInformation("BYR", "Belarussian Ruble")]
        Belarussian_Ruble,
        //@SerializedName("BZD")
        [DrxEnumExtendedInformation("BZD", "Belize Dollar")]
        Belize_Dollar,
        //@SerializedName("XOF")
        [DrxEnumExtendedInformation("XOF", "CFA Franc BCEAO")]
        CFA_Franc_BCEAO,
        //@SerializedName("BMD")
        [DrxEnumExtendedInformation("BMD", "Bermudian Dollar")]
        Bermudian_Dollar,
        //@SerializedName("BTN")
        [DrxEnumExtendedInformation("BTN", "Ngultrum")]
        Ngultrum,
        //@SerializedName("INR")
        [DrxEnumExtendedInformation("INR", "Indian Rupee")]
        Indian_Rupee,
        //@SerializedName("BOB")
        [DrxEnumExtendedInformation("BOB", "Boliviano")]
        Boliviano,
        //@SerializedName("BOV")
        [DrxEnumExtendedInformation("BOV", "Mvdol")]
        Mvdol,
        //@SerializedName("BAM")
        [DrxEnumExtendedInformation("BAM", "Convertible Mark")]
        Convertible_Mark,
        //@SerializedName("BWP")
        [DrxEnumExtendedInformation("BWP", "Pula")]
        Pula,
        //@SerializedName("NOK")
        [DrxEnumExtendedInformation("NOK", "Norwegian Krone")]
        Norwegian_Krone,
        //@SerializedName("BRL")
        [DrxEnumExtendedInformation("BRL", "Brazilian Real")]
        Brazilian_Real,
        //@SerializedName("BND")
        [DrxEnumExtendedInformation("BND", "Brunei Dollar")]
        Brunei_Dollar,
        //@SerializedName("BGN")
        [DrxEnumExtendedInformation("BGN", "Bulgarian Lev")]
        Bulgarian_Lev,
        //@SerializedName("BIF")
        [DrxEnumExtendedInformation("BIF", "Burundi Franc")]
        Burundi_Franc,
        //@SerializedName("CVE")
        [DrxEnumExtendedInformation("CVE", "Cabo Verde Escudo")]
        Cabo_Verde_Escudo,
        //@SerializedName("KHR")
        [DrxEnumExtendedInformation("KHR", "Riel")]
        Riel,
        //@SerializedName("XAF")
        [DrxEnumExtendedInformation("XAF", "CFA Franc BEAC")]
        CFA_Franc_BEAC,
        //@SerializedName("CAD")
        [DrxEnumExtendedInformation("CAD", "Canadian Dollar")]
        Canadian_Dollar,
        //@SerializedName("KYD")
        [DrxEnumExtendedInformation("KYD", "Cayman Islands Dollar")]
        Cayman_Islands_Dollar,
        //@SerializedName("CLF")
        [DrxEnumExtendedInformation("CLF", "Unidad de Fomento")]
        Unidad_de_Fomento,
        //@SerializedName("CLP")
        [DrxEnumExtendedInformation("CLP", "Chilean Peso")]
        Chilean_Peso,
        //@SerializedName("CNY")
        [DrxEnumExtendedInformation("CNY", "Yuan Renminbi")]
        Yuan_Renminbi,
        //@SerializedName("COP")
        [DrxEnumExtendedInformation("COP", "Colombian Peso")]
        Colombian_Peso,
        //@SerializedName("COU")
        [DrxEnumExtendedInformation("COU", "Unidad de Valor Real")]
        Unidad_de_Valor_Real,
        //@SerializedName("KMF")
        [DrxEnumExtendedInformation("KMF", "Comoro Franc")]
        Comoro_Franc,
        //@SerializedName("CDF")
        [DrxEnumExtendedInformation("CDF", "Congolese Franc")]
        Congolese_Franc,
        //@SerializedName("NZD")
        [DrxEnumExtendedInformation("NZD", "New Zealand Dollar")]
        New_Zealand_Dollar,
        //@SerializedName("CRC")
        [DrxEnumExtendedInformation("CRC", "Costa Rican Colon")]
        Costa_Rican_Colon,
        //@SerializedName("HRK")
        [DrxEnumExtendedInformation("HRK", "Kuna")]
        Kuna,
        //@SerializedName("CUC")
        [DrxEnumExtendedInformation("CUC", "Peso Convertible")]
        Peso_Convertible,
        //@SerializedName("CUP")
        [DrxEnumExtendedInformation("CUP", "Cuban Peso")]
        Cuban_Peso,
        //@SerializedName("ANG")
        [DrxEnumExtendedInformation("ANG", "Netherlands Antillean Guilder")]
        Netherlands_Antillean_Guilder,
        //@SerializedName("CZK")
        [DrxEnumExtendedInformation("CZK", "Czech Koruna")]
        Czech_Koruna,
        //@SerializedName("DKK")
        [DrxEnumExtendedInformation("DKK", "Danish Krone")]
        Danish_Krone,
        //@SerializedName("DJF")
        [DrxEnumExtendedInformation("DJF", "Djibouti Franc")]
        Djibouti_Franc,
        //@SerializedName("DOP")
        [DrxEnumExtendedInformation("DOP", "Dominican Peso")]
        Dominican_Peso,
        //@SerializedName("EGP")
        [DrxEnumExtendedInformation("EGP", "Egyptian Pound")]
        Egyptian_Pound,
        //@SerializedName("SVC")
        [DrxEnumExtendedInformation("SVC", "El Salvador Colon")]
        El_Salvador_Colon,
        //@SerializedName("ERN")
        [DrxEnumExtendedInformation("ERN", "Nakfa")]
        Nakfa,
        //@SerializedName("ETB")
        [DrxEnumExtendedInformation("ETB", "Ethiopian Birr")]
        Ethiopian_Birr,
        //@SerializedName("FKP")
        [DrxEnumExtendedInformation("FKP", "Falkland Islands Pound")]
        Falkland_Islands_Pound,
        //@SerializedName("FJD")
        [DrxEnumExtendedInformation("FJD", "Fiji Dollar")]
        Fiji_Dollar,
        //@SerializedName("XPF")
        [DrxEnumExtendedInformation("XPF", "CFP Franc")]
        CFP_Franc,
        //@SerializedName("GMD")
        [DrxEnumExtendedInformation("GMD", "Dalasi")]
        Dalasi,
        //@SerializedName("GEL")
        [DrxEnumExtendedInformation("GEL", "Lari")]
        Lari,
        //@SerializedName("GHS")
        [DrxEnumExtendedInformation("GHS", "Ghana Cedi")]
        Ghana_Cedi,
        //@SerializedName("GIP")
        [DrxEnumExtendedInformation("GIP", "Gibraltar Pound")]
        Gibraltar_Pound,
        //@SerializedName("GTQ")
        [DrxEnumExtendedInformation("GTQ", "Quetzal")]
        Quetzal,
        //@SerializedName("GBP")
        [DrxEnumExtendedInformation("GBP", "British Sterling(Pound)")]
        British_Sterling,
        //@SerializedName("GNF")
        [DrxEnumExtendedInformation("GNF", "Guinea Franc")]
        Guinea_Franc,
        //@SerializedName("GYD")
        [DrxEnumExtendedInformation("GYD", "Guyana Dollar")]
        Guyana_Dollar,
        //@SerializedName("HTG")
        [DrxEnumExtendedInformation("HTG", "Gourde")]
        Gourde,
        //@SerializedName("HNL")
        [DrxEnumExtendedInformation("HNL", "Lempira")]
        Lempira,
        //@SerializedName("HKD")
        [DrxEnumExtendedInformation("HKD", "Hong Kong Dollar")]
        Hong_Kong_Dollar,
        //@SerializedName("HUF")
        [DrxEnumExtendedInformation("HUF", "Forint")]
        Forint,
        //@SerializedName("ISK")
        [DrxEnumExtendedInformation("ISK", "Iceland Krona")]
        Iceland_Krona,
        //@SerializedName("IDR")
        [DrxEnumExtendedInformation("IDR", "Rupiah")]
        Rupiah,
        //@SerializedName("IRR")
        [DrxEnumExtendedInformation("IRR", "Iranian Rial")]
        Iranian_Rial,
        //@SerializedName("IQD")
        [DrxEnumExtendedInformation("IQD", "Iraqi Dinar")]
        Iraqi_Dinar,
        //@SerializedName("ILS")
        [DrxEnumExtendedInformation("ILS", "New Israeli Sheqel")]
        New_Israeli_Sheqel,
        //@SerializedName("JMD")
        [DrxEnumExtendedInformation("JMD", "Jamaican Dollar")]
        Jamaican_Dollar,
        //@SerializedName("JPY")
        [DrxEnumExtendedInformation("JPY", "Yen")]
        Yen,
        //@SerializedName("JOD")
        [DrxEnumExtendedInformation("JOD", "Jordanian Dinar")]
        Jordanian_Dinar,
        //@SerializedName("KZT")
        [DrxEnumExtendedInformation("KZT", "Tenge")]
        Tenge,
        //@SerializedName("KES")
        [DrxEnumExtendedInformation("KES", "Kenyan Shilling")]
        Kenyan_Shilling,
        //@SerializedName("KPW")
        [DrxEnumExtendedInformation("KPW", "North Korean Won")]
        North_Korean_Won,
        //@SerializedName("KRW")
        [DrxEnumExtendedInformation("KRW", "Won")]
        Won,
        //@SerializedName("KWD")
        [DrxEnumExtendedInformation("KWD", "Kuwaiti Dinar")]
        Kuwaiti_Dinar,
        //@SerializedName("KGS")
        [DrxEnumExtendedInformation("KGS", "Som")]
        Som,
        //@SerializedName("LAK")
        [DrxEnumExtendedInformation("LAK", "Kip")]
        Kip,
        //@SerializedName("LBP")
        [DrxEnumExtendedInformation("LBP", "Lebanese Pound")]
        Lebanese_Pound,
        //@SerializedName("LSL")
        [DrxEnumExtendedInformation("LSL", "Loti")]
        Loti,
        //@SerializedName("ZAR")
        [DrxEnumExtendedInformation("ZAR", "Rand")]
        Rand,
        //@SerializedName("LRD")
        [DrxEnumExtendedInformation("LRD", "Liberian Dollar")]
        Liberian_Dollar,
        //@SerializedName("LYD")
        [DrxEnumExtendedInformation("LYD", "Libyan Dinar")]
        Libyan_Dinar,
        //@SerializedName("CHF")
        [DrxEnumExtendedInformation("CHF", "Swiss Franc")]
        Swiss_Franc,
        //@SerializedName("MOP")
        [DrxEnumExtendedInformation("MOP", "Pataca")]
        Pataca,
        //@SerializedName("MKD")
        [DrxEnumExtendedInformation("MKD", "Denar")]
        Denar,
        //@SerializedName("MGA")
        [DrxEnumExtendedInformation("MGA", "Malagasy Ariary")]
        Malagasy_Ariary,
        //@SerializedName("MWK")
        [DrxEnumExtendedInformation("MWK", "Kwacha")]
        Kwacha,
        //@SerializedName("MYR")
        [DrxEnumExtendedInformation("MYR", "Malaysian Ringgit")]
        Malaysian_Ringgit,
        //@SerializedName("MVR")
        [DrxEnumExtendedInformation("MVR", "Rufiyaa")]
        Rufiyaa,
        //@SerializedName("MRO")
        [DrxEnumExtendedInformation("MRO", "Ouguiya")]
        Ouguiya,
        //@SerializedName("MUR")
        [DrxEnumExtendedInformation("MUR", "Mauritius Rupee")]
        Mauritius_Rupee,
        //@SerializedName("XUA")
        [DrxEnumExtendedInformation("XUA", "ADB Unit of Account")]
        ADB_Unit_of_Account,
        //@SerializedName("MXN")
        [DrxEnumExtendedInformation("MXN", "Mexican Peso")]
        Mexican_Peso,
        //@SerializedName("MDL")
        [DrxEnumExtendedInformation("MDL", "Moldovan Leu")]
        Moldovan_Leu,
        //@SerializedName("MNT")
        [DrxEnumExtendedInformation("MNT", "Tugrik")]
        Tugrik,
        //@SerializedName("MAD")
        [DrxEnumExtendedInformation("MAD", "Moroccan Dirham")]
        Moroccan_Dirham,
        //@SerializedName("MZN")
        [DrxEnumExtendedInformation("MZN", "Mozambique Metical")]
        Mozambique_Metical,
        //@SerializedName("MMK")
        [DrxEnumExtendedInformation("MMK", "Kyat")]
        Kyat,
        //@SerializedName("NAD")
        [DrxEnumExtendedInformation("NAD", "Namibia Dollar")]
        Namibia_Dollar,
        //@SerializedName("NPR")
        [DrxEnumExtendedInformation("NPR", "Nepalese Rupee")]
        Nepalese_Rupee,
        //@SerializedName("NIO")
        [DrxEnumExtendedInformation("NIO", "Cordoba Oro")]
        Cordoba_Oro,
        //@SerializedName("NGN")
        [DrxEnumExtendedInformation("NGN", "Naira")]
        Naira,
        //@SerializedName("OMR")
        [DrxEnumExtendedInformation("OMR", "Rial Omani")]
        Rial_Omani,
        //@SerializedName("PKR")
        [DrxEnumExtendedInformation("PKR", "Pakistan Rupee")]
        Pakistan_Rupee,
        //@SerializedName("PAB")
        [DrxEnumExtendedInformation("PAB", "Balboa")]
        Balboa,
        //@SerializedName("PGK")
        [DrxEnumExtendedInformation("PGK", "Kina")]
        Kina,
        //@SerializedName("PYG")
        [DrxEnumExtendedInformation("PYG", "Guarani")]
        Guarani,
        //@SerializedName("PEN")
        [DrxEnumExtendedInformation("PEN", "Nuevo Sol")]
        Nuevo_Sol,
        //@SerializedName("PHP")
        [DrxEnumExtendedInformation("PHP", "Philippine Peso")]
        Philippine_Peso,
        //@SerializedName("PLN")
        [DrxEnumExtendedInformation("PLN", "Zloty")]
        Zloty,
        //@SerializedName("QAR")
        [DrxEnumExtendedInformation("QAR", "Qatari Rial")]
        Qatari_Rial,
        //@SerializedName("RON")
        [DrxEnumExtendedInformation("RON", "Romanian Leu")]
        Romanian_Leu,
        //@SerializedName("RUB")
        [DrxEnumExtendedInformation("RUB", "Russian Ruble")]
        Russian_Ruble,
        //@SerializedName("RWF")
        [DrxEnumExtendedInformation("RWF", "Rwanda Franc")]
        Rwanda_Franc,
        //@SerializedName("SHP")
        [DrxEnumExtendedInformation("SHP", "Saint Helena Pound")]
        Saint_Helena_Pound,
        //@SerializedName("WST")
        [DrxEnumExtendedInformation("WST", "Tala")]
        Tala,
        //@SerializedName("STD")
        [DrxEnumExtendedInformation("STD", "Dobra")]
        Dobra,
        //@SerializedName("SAR")
        [DrxEnumExtendedInformation("SAR", "Saudi Riyal")]
        Saudi_Riyal,
        //@SerializedName("RSD")
        [DrxEnumExtendedInformation("RSD", "Serbian Dinar")]
        Serbian_Dinar,
        //@SerializedName("SCR")
        [DrxEnumExtendedInformation("SCR", "Seychelles Rupee")]
        Seychelles_Rupee,
        //@SerializedName("SLL")
        [DrxEnumExtendedInformation("SLL", "Leone")]
        Leone,
        //@SerializedName("SGD")
        [DrxEnumExtendedInformation("SGD", "Singapore Dollar")]
        Singapore_Dollar,
        //@SerializedName("XSU")
        [DrxEnumExtendedInformation("XSU", "Sucre")]
        Sucre,
        //@SerializedName("SBD")
        [DrxEnumExtendedInformation("SBD", "Solomon Islands Dollar")]
        Solomon_Islands_Dollar,
        //@SerializedName("SOS")
        [DrxEnumExtendedInformation("SOS", "Somali Shilling")]
        Somali_Shilling,
        //@SerializedName("SSP")
        [DrxEnumExtendedInformation("SSP", "South Sudanese Pound")]
        South_Sudanese_Pound,
        //@SerializedName("LKR")
        [DrxEnumExtendedInformation("LKR", "Sri Lanka Rupee")]
        Sri_Lanka_Rupee,
        //@SerializedName("SDG")
        [DrxEnumExtendedInformation("SDG", "Sudanese Pound")]
        Sudanese_Pound,
        //@SerializedName("SRD")
        [DrxEnumExtendedInformation("SRD", "Surinam Dollar")]
        Surinam_Dollar,
        //@SerializedName("SZL")
        [DrxEnumExtendedInformation("SZL", "Lilangeni")]
        Lilangeni,
        //@SerializedName("SEK")
        [DrxEnumExtendedInformation("SEK", "Swedish Krona")]
        Swedish_Krona,
        //@SerializedName("CHE")
        [DrxEnumExtendedInformation("CHE", "WIR Euro")]
        WIR_Euro,
        //@SerializedName("CHW")
        [DrxEnumExtendedInformation("CHW", "WIR Franc")]
        WIR_Franc,
        //@SerializedName("SYP")
        [DrxEnumExtendedInformation("SYP", "Syrian Pound")]
        Syrian_Pound,
        //@SerializedName("TWD")
        [DrxEnumExtendedInformation("TWD", "New Taiwan Dollar")]
        New_Taiwan_Dollar,
        //@SerializedName("TJS")
        [DrxEnumExtendedInformation("TJS", "Somoni")]
        Somoni,
        //@SerializedName("TZS")
        [DrxEnumExtendedInformation("TZS", "Tanzanian Shilling")]
        Tanzanian_Shilling,
        //@SerializedName("THB")
        [DrxEnumExtendedInformation("THB", "Baht")]
        Baht,
        //@SerializedName("TOP")
        [DrxEnumExtendedInformation("TOP", "Pa’anga")]
        Paanga,
        //@SerializedName("TTD")
        [DrxEnumExtendedInformation("TTD", "Trinidad and Tobago Dollar")]
        Trinidad_and_Tobago_Dollar,
        //@SerializedName("TND")
        [DrxEnumExtendedInformation("TND", "Tunisian Dinar")]
        Tunisian_Dinar,
        //@SerializedName("TRY")
        [DrxEnumExtendedInformation("TRY", "Turkish Lira")]
        Turkish_Lira,
        //@SerializedName("TMT")
        [DrxEnumExtendedInformation("TMT", "Turkmenistan New Manat")]
        Turkmenistan_New_Manat,
        //@SerializedName("UGX")
        [DrxEnumExtendedInformation("UGX", "Uganda Shilling")]
        Uganda_Shilling,
        //@SerializedName("UAH")
        [DrxEnumExtendedInformation("UAH", "Hryvnia")]
        Hryvnia,
        //@SerializedName("AED")
        [DrxEnumExtendedInformation("AED", "UAE Dirham")]
        UAE_Dirham,
        //@SerializedName("UYU")
        [DrxEnumExtendedInformation("UYU", "Peso Uruguayo")]
        Peso_Uruguayo,
        //@SerializedName("UZS")
        [DrxEnumExtendedInformation("UZS", "Uzbekistan Sum")]
        Uzbekistan_Sum,
        //@SerializedName("VUV")
        [DrxEnumExtendedInformation("VUV", "Vatu")]
        Vatu,
        //@SerializedName("VEF")
        [DrxEnumExtendedInformation("VEF", "Bolivar")]
        Bolivar,
        //@SerializedName("VND")
        [DrxEnumExtendedInformation("VND", "Dong")]
        Dong,
        //@SerializedName("YER")
        [DrxEnumExtendedInformation("YER", "Yemeni Rial")]
        Yemeni_Rial,
        //@SerializedName("ZMW")
        [DrxEnumExtendedInformation("ZMW", "Zambian Kwacha")]
        Zambian_Kwacha,
        //@SerializedName("ZWL")
        [DrxEnumExtendedInformation("ZWL", "Zimbabwe Dollar")]
        Zimbabwe_Dollar
    }
}