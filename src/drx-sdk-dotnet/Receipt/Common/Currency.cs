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

using System.Runtime.Serialization;
using Net.Dreceiptx.Users;

namespace Net.Dreceiptx.Receipt.Common
{
    //TODO: Add conversion methods like in java side of things
    public enum Currency
    {
        [EnumMember(Value = "AFN")]
        [DrxEnumExtendedInformation("AFN", "Afghani")]
        Afghani,

        [EnumMember(Value = "EUR")]
        [DrxEnumExtendedInformation("EUR", "Euro")]
        Euro,

        [EnumMember(Value = "ALL")]
        [DrxEnumExtendedInformation("ALL", "Lek")]
        Lek,

        [EnumMember(Value = "DZD")]
        [DrxEnumExtendedInformation("DZD", "Algerian Dinar")]
        AlgerianDinar,

        [EnumMember(Value = "USD")]
        [DrxEnumExtendedInformation("USD", "US Dollar")]
        UsDollar,

        [EnumMember(Value = "AOA")]
        [DrxEnumExtendedInformation("AOA", "Kwanza")]
        Kwanza,

        [EnumMember(Value = "XCD")]
        [DrxEnumExtendedInformation("XCD", "East Caribbean Dollar")]
        EastCaribbeanDollar,

        [EnumMember(Value = "ARS")]
        [DrxEnumExtendedInformation("ARS", "Argentine Peso")]
        ArgentinePeso,

        [EnumMember(Value = "AMD")]
        [DrxEnumExtendedInformation("AMD", "Armenian Dram")]
        ArmenianDram,

        [EnumMember(Value = "AWG")]
        [DrxEnumExtendedInformation("AWG", "Aruban Florin")]
        ArubanFlorin,

        [EnumMember(Value = "AUD")]
        [DrxEnumExtendedInformation("AUD", "Australian Dollar")]
        AustralianDollar,

        [EnumMember(Value = "AZN")]
        [DrxEnumExtendedInformation("AZN", "Azerbaijanian Manat")]
        AzerbaijanianManat,

        [EnumMember(Value = "BSD")]
        [DrxEnumExtendedInformation("BSD", "Bahamian Dollar")]
        BahamianDollar,

        [EnumMember(Value = "BHD")]
        [DrxEnumExtendedInformation("BHD", "Bahraini Dinar")]
        BahrainiDinar,

        [EnumMember(Value = "BDT")]
        [DrxEnumExtendedInformation("BDT", "Taka")]
        Taka,

        [EnumMember(Value = "BBD")]
        [DrxEnumExtendedInformation("BBD", "Barbados Dollar")]
        BarbadosDollar,

        [EnumMember(Value = "BYR")]
        [DrxEnumExtendedInformation("BYR", "Belarussian Ruble")]
        BelarussianRuble,

        [EnumMember(Value = "BZD")]
        [DrxEnumExtendedInformation("BZD", "Belize Dollar")]
        BelizeDollar,

        [EnumMember(Value = "XOF")]
        [DrxEnumExtendedInformation("XOF", "CFA Franc BCEAO")]
        CfaFrancBceao,

        [EnumMember(Value = "BMD")]
        [DrxEnumExtendedInformation("BMD", "Bermudian Dollar")]
        BermudianDollar,

        [EnumMember(Value = "BTN")]
        [DrxEnumExtendedInformation("BTN", "Ngultrum")]
        Ngultrum,

        [EnumMember(Value = "INR")]
        [DrxEnumExtendedInformation("INR", "Indian Rupee")]
        IndianRupee,

        [EnumMember(Value = "BOB")]
        [DrxEnumExtendedInformation("BOB", "Boliviano")]
        Boliviano,

        [EnumMember(Value = "BOV")]
        [DrxEnumExtendedInformation("BOV", "Mvdol")]
        Mvdol,

        [EnumMember(Value = "BAM")]
        [DrxEnumExtendedInformation("BAM", "Convertible Mark")]
        ConvertibleMark,

        [EnumMember(Value = "BWP")]
        [DrxEnumExtendedInformation("BWP", "Pula")]
        Pula,

        [EnumMember(Value = "NOK")]
        [DrxEnumExtendedInformation("NOK", "Norwegian Krone")]
        NorwegianKrone,

        [EnumMember(Value = "BRL")]
        [DrxEnumExtendedInformation("BRL", "Brazilian Real")]
        BrazilianReal,

        [EnumMember(Value = "BND")]
        [DrxEnumExtendedInformation("BND", "Brunei Dollar")]
        BruneiDollar,

        [EnumMember(Value = "BGN")]
        [DrxEnumExtendedInformation("BGN", "Bulgarian Lev")]
        BulgarianLev,

        [EnumMember(Value = "BIF")]
        [DrxEnumExtendedInformation("BIF", "Burundi Franc")]
        BurundiFranc,

        [EnumMember(Value = "CVE")]
        [DrxEnumExtendedInformation("CVE", "Cabo Verde Escudo")]
        CaboVerdeEscudo,

        [EnumMember(Value = "KHR")]
        [DrxEnumExtendedInformation("KHR", "Riel")]
        Riel,

        [EnumMember(Value = "XAF")]
        [DrxEnumExtendedInformation("XAF", "CFA Franc BEAC")]
        CfaFrancBeac,

        [EnumMember(Value = "CAD")]
        [DrxEnumExtendedInformation("CAD", "Canadian Dollar")]
        CanadianDollar,

        [EnumMember(Value = "KYD")]
        [DrxEnumExtendedInformation("KYD", "Cayman Islands Dollar")]
        CaymanIslandsDollar,

        [EnumMember(Value = "CLF")]
        [DrxEnumExtendedInformation("CLF", "Unidad de Fomento")]
        UnidadDeFomento,

        [EnumMember(Value = "CLP")]
        [DrxEnumExtendedInformation("CLP", "Chilean Peso")]
        ChileanPeso,

        [EnumMember(Value = "CNY")]
        [DrxEnumExtendedInformation("CNY", "Yuan Renminbi")]
        YuanRenminbi,

        [EnumMember(Value = "COP")]
        [DrxEnumExtendedInformation("COP", "Colombian Peso")]
        ColombianPeso,

        [EnumMember(Value = "COU")]
        [DrxEnumExtendedInformation("COU", "Unidad de Valor Real")]
        UnidadDeValorReal,

        [EnumMember(Value = "KMF")]
        [DrxEnumExtendedInformation("KMF", "Comoro Franc")]
        ComoroFranc,

        [EnumMember(Value = "CDF")]
        [DrxEnumExtendedInformation("CDF", "Congolese Franc")]
        CongoleseFranc,

        [EnumMember(Value = "NZD")]
        [DrxEnumExtendedInformation("NZD", "New Zealand Dollar")]
        NewZealandDollar,

        [EnumMember(Value = "CRC")]
        [DrxEnumExtendedInformation("CRC", "Costa Rican Colon")]
        CostaRicanColon,

        [EnumMember(Value = "HRK")]
        [DrxEnumExtendedInformation("HRK", "Kuna")]
        Kuna,

        [EnumMember(Value = "CUC")]
        [DrxEnumExtendedInformation("CUC", "Peso Convertible")]
        PesoConvertible,

        [EnumMember(Value = "CUP")]
        [DrxEnumExtendedInformation("CUP", "Cuban Peso")]
        CubanPeso,

        [EnumMember(Value = "ANG")]
        [DrxEnumExtendedInformation("ANG", "Netherlands Antillean Guilder")]
        NetherlandsAntilleanGuilder,

        [EnumMember(Value = "CZK")]
        [DrxEnumExtendedInformation("CZK", "Czech Koruna")]
        CzechKoruna,

        [EnumMember(Value = "DKK")]
        [DrxEnumExtendedInformation("DKK", "Danish Krone")]
        DanishKrone,

        [EnumMember(Value = "DJF")]
        [DrxEnumExtendedInformation("DJF", "Djibouti Franc")]
        DjiboutiFranc,

        [EnumMember(Value = "DOP")]
        [DrxEnumExtendedInformation("DOP", "Dominican Peso")]
        DominicanPeso,

        [EnumMember(Value = "EGP")]
        [DrxEnumExtendedInformation("EGP", "Egyptian Pound")]
        EgyptianPound,

        [EnumMember(Value = "SVC")]
        [DrxEnumExtendedInformation("SVC", "El Salvador Colon")]
        ElSalvadorColon,

        [EnumMember(Value = "ERN")]
        [DrxEnumExtendedInformation("ERN", "Nakfa")]
        Nakfa,

        [EnumMember(Value = "ETB")]
        [DrxEnumExtendedInformation("ETB", "Ethiopian Birr")]
        EthiopianBirr,

        [EnumMember(Value = "FKP")]
        [DrxEnumExtendedInformation("FKP", "Falkland Islands Pound")]
        FalklandIslandsPound,

        [EnumMember(Value = "FJD")]
        [DrxEnumExtendedInformation("FJD", "Fiji Dollar")]
        FijiDollar,

        [EnumMember(Value = "XPF")]
        [DrxEnumExtendedInformation("XPF", "CFP Franc")]
        CfpFranc,

        [EnumMember(Value = "GMD")]
        [DrxEnumExtendedInformation("GMD", "Dalasi")]
        Dalasi,

        [EnumMember(Value = "GEL")]
        [DrxEnumExtendedInformation("GEL", "Lari")]
        Lari,

        [EnumMember(Value = "GHS")]
        [DrxEnumExtendedInformation("GHS", "Ghana Cedi")]
        GhanaCedi,

        [EnumMember(Value = "GIP")]
        [DrxEnumExtendedInformation("GIP", "Gibraltar Pound")]
        GibraltarPound,

        [EnumMember(Value = "GTQ")]
        [DrxEnumExtendedInformation("GTQ", "Quetzal")]
        Quetzal,

        [EnumMember(Value = "GBP")]
        [DrxEnumExtendedInformation("GBP", "British Sterling(Pound)")]
        BritishSterling,

        [EnumMember(Value = "GNF")]
        [DrxEnumExtendedInformation("GNF", "Guinea Franc")]
        GuineaFranc,

        [EnumMember(Value = "GYD")]
        [DrxEnumExtendedInformation("GYD", "Guyana Dollar")]
        GuyanaDollar,

        [EnumMember(Value = "HTG")]
        [DrxEnumExtendedInformation("HTG", "Gourde")]
        Gourde,

        [EnumMember(Value = "HNL")]
        [DrxEnumExtendedInformation("HNL", "Lempira")]
        Lempira,

        [EnumMember(Value = "HKD")]
        [DrxEnumExtendedInformation("HKD", "Hong Kong Dollar")]
        HongKongDollar,

        [EnumMember(Value = "HUF")]
        [DrxEnumExtendedInformation("HUF", "Forint")]
        Forint,

        [EnumMember(Value = "ISK")]
        [DrxEnumExtendedInformation("ISK", "Iceland Krona")]
        IcelandKrona,

        [EnumMember(Value = "IDR")]
        [DrxEnumExtendedInformation("IDR", "Rupiah")]
        Rupiah,

        [EnumMember(Value = "IRR")]
        [DrxEnumExtendedInformation("IRR", "Iranian Rial")]
        IranianRial,

        [EnumMember(Value = "IQD")]
        [DrxEnumExtendedInformation("IQD", "Iraqi Dinar")]
        IraqiDinar,

        [EnumMember(Value = "ILS")]
        [DrxEnumExtendedInformation("ILS", "New Israeli Sheqel")]
        NewIsraeliSheqel,

        [EnumMember(Value = "JMD")]
        [DrxEnumExtendedInformation("JMD", "Jamaican Dollar")]
        JamaicanDollar,

        [EnumMember(Value = "JPY")]
        [DrxEnumExtendedInformation("JPY", "Yen")]
        Yen,

        [EnumMember(Value = "JOD")]
        [DrxEnumExtendedInformation("JOD", "Jordanian Dinar")]
        JordanianDinar,

        [EnumMember(Value = "KZT")]
        [DrxEnumExtendedInformation("KZT", "Tenge")]
        Tenge,

        [EnumMember(Value = "KES")]
        [DrxEnumExtendedInformation("KES", "Kenyan Shilling")]
        KenyanShilling,

        [EnumMember(Value = "KPW")]
        [DrxEnumExtendedInformation("KPW", "North Korean Won")]
        NorthKoreanWon,

        [EnumMember(Value = "KRW")]
        [DrxEnumExtendedInformation("KRW", "Won")]
        Won,

        [EnumMember(Value = "KWD")]
        [DrxEnumExtendedInformation("KWD", "Kuwaiti Dinar")]
        KuwaitiDinar,

        [EnumMember(Value = "KGS")]
        [DrxEnumExtendedInformation("KGS", "Som")]
        Som,

        [EnumMember(Value = "LAK")]
        [DrxEnumExtendedInformation("LAK", "Kip")]
        Kip,

        [EnumMember(Value = "LBP")]
        [DrxEnumExtendedInformation("LBP", "Lebanese Pound")]
        LebanesePound,

        [EnumMember(Value = "LSL")]
        [DrxEnumExtendedInformation("LSL", "Loti")]
        Loti,

        [EnumMember(Value = "ZAR")]
        [DrxEnumExtendedInformation("ZAR", "Rand")]
        Rand,

        [EnumMember(Value = "LRD")]
        [DrxEnumExtendedInformation("LRD", "Liberian Dollar")]
        LiberianDollar,

        [EnumMember(Value = "LYD")]
        [DrxEnumExtendedInformation("LYD", "Libyan Dinar")]
        LibyanDinar,

        [EnumMember(Value = "CHF")]
        [DrxEnumExtendedInformation("CHF", "Swiss Franc")]
        SwissFranc,

        [EnumMember(Value = "MOP")]
        [DrxEnumExtendedInformation("MOP", "Pataca")]
        Pataca,

        [EnumMember(Value = "MKD")]
        [DrxEnumExtendedInformation("MKD", "Denar")]
        Denar,

        [EnumMember(Value = "MGA")]
        [DrxEnumExtendedInformation("MGA", "Malagasy Ariary")]
        MalagasyAriary,

        [EnumMember(Value = "MWK")]
        [DrxEnumExtendedInformation("MWK", "Kwacha")]
        Kwacha,

        [EnumMember(Value = "MYR")]
        [DrxEnumExtendedInformation("MYR", "Malaysian Ringgit")]
        MalaysianRinggit,

        [EnumMember(Value = "MVR")]
        [DrxEnumExtendedInformation("MVR", "Rufiyaa")]
        Rufiyaa,

        [EnumMember(Value = "MRO")]
        [DrxEnumExtendedInformation("MRO", "Ouguiya")]
        Ouguiya,

        [EnumMember(Value = "MUR")]
        [DrxEnumExtendedInformation("MUR", "Mauritius Rupee")]
        MauritiusRupee,

        [EnumMember(Value = "XUA")]
        [DrxEnumExtendedInformation("XUA", "ADB Unit of Account")]
        AdbUnitOfAccount,

        [EnumMember(Value = "MXN")]
        [DrxEnumExtendedInformation("MXN", "Mexican Peso")]
        MexicanPeso,

        [EnumMember(Value = "MDL")]
        [DrxEnumExtendedInformation("MDL", "Moldovan Leu")]
        MoldovanLeu,

        [EnumMember(Value = "MNT")]
        [DrxEnumExtendedInformation("MNT", "Tugrik")]
        Tugrik,

        [EnumMember(Value = "MAD")]
        [DrxEnumExtendedInformation("MAD", "Moroccan Dirham")]
        MoroccanDirham,

        [EnumMember(Value = "MZN")]
        [DrxEnumExtendedInformation("MZN", "Mozambique Metical")]
        MozambiqueMetical,

        [EnumMember(Value = "MMK")]
        [DrxEnumExtendedInformation("MMK", "Kyat")]
        Kyat,

        [EnumMember(Value = "NAD")]
        [DrxEnumExtendedInformation("NAD", "Namibia Dollar")]
        NamibiaDollar,

        [EnumMember(Value = "NPR")]
        [DrxEnumExtendedInformation("NPR", "Nepalese Rupee")]
        NepaleseRupee,

        [EnumMember(Value = "NIO")]
        [DrxEnumExtendedInformation("NIO", "Cordoba Oro")]
        CordobaOro,

        [EnumMember(Value = "NGN")]
        [DrxEnumExtendedInformation("NGN", "Naira")]
        Naira,

        [EnumMember(Value = "OMR")]
        [DrxEnumExtendedInformation("OMR", "Rial Omani")]
        RialOmani,

        [EnumMember(Value = "PKR")]
        [DrxEnumExtendedInformation("PKR", "Pakistan Rupee")]
        PakistanRupee,

        [EnumMember(Value = "PAB")]
        [DrxEnumExtendedInformation("PAB", "Balboa")]
        Balboa,

        [EnumMember(Value = "PGK")]
        [DrxEnumExtendedInformation("PGK", "Kina")]
        Kina,

        [EnumMember(Value = "PYG")]
        [DrxEnumExtendedInformation("PYG", "Guarani")]
        Guarani,

        [EnumMember(Value = "PEN")]
        [DrxEnumExtendedInformation("PEN", "Nuevo Sol")]
        NuevoSol,

        [EnumMember(Value = "PHP")]
        [DrxEnumExtendedInformation("PHP", "Philippine Peso")]
        PhilippinePeso,

        [EnumMember(Value = "PLN")]
        [DrxEnumExtendedInformation("PLN", "Zloty")]
        Zloty,

        [EnumMember(Value = "QAR")]
        [DrxEnumExtendedInformation("QAR", "Qatari Rial")]
        QatariRial,

        [EnumMember(Value = "RON")]
        [DrxEnumExtendedInformation("RON", "Romanian Leu")]
        RomanianLeu,

        [EnumMember(Value = "RUB")]
        [DrxEnumExtendedInformation("RUB", "Russian Ruble")]
        RussianRuble,

        [EnumMember(Value = "RWF")]
        [DrxEnumExtendedInformation("RWF", "Rwanda Franc")]
        RwandaFranc,

        [EnumMember(Value = "SHP")]
        [DrxEnumExtendedInformation("SHP", "Saint Helena Pound")]
        SaintHelenaPound,

        [EnumMember(Value = "WST")]
        [DrxEnumExtendedInformation("WST", "Tala")]
        Tala,

        [EnumMember(Value = "STD")]
        [DrxEnumExtendedInformation("STD", "Dobra")]
        Dobra,

        [EnumMember(Value = "SAR")]
        [DrxEnumExtendedInformation("SAR", "Saudi Riyal")]
        SaudiRiyal,

        [EnumMember(Value = "RSD")]
        [DrxEnumExtendedInformation("RSD", "Serbian Dinar")]
        SerbianDinar,

        [EnumMember(Value = "SCR")]
        [DrxEnumExtendedInformation("SCR", "Seychelles Rupee")]
        SeychellesRupee,

        [EnumMember(Value = "SLL")]
        [DrxEnumExtendedInformation("SLL", "Leone")]
        Leone,

        [EnumMember(Value = "SGD")]
        [DrxEnumExtendedInformation("SGD", "Singapore Dollar")]
        SingaporeDollar,

        [EnumMember(Value = "XSU")]
        [DrxEnumExtendedInformation("XSU", "Sucre")]
        Sucre,

        [EnumMember(Value = "SBD")]
        [DrxEnumExtendedInformation("SBD", "Solomon Islands Dollar")]
        SolomonIslandsDollar,

        [EnumMember(Value = "SOS")]
        [DrxEnumExtendedInformation("SOS", "Somali Shilling")]
        SomaliShilling,

        [EnumMember(Value = "SSP")]
        [DrxEnumExtendedInformation("SSP", "South Sudanese Pound")]
        SouthSudanesePound,

        [EnumMember(Value = "LKR")]
        [DrxEnumExtendedInformation("LKR", "Sri Lanka Rupee")]
        SriLankaRupee,

        [EnumMember(Value = "SDG")]
        [DrxEnumExtendedInformation("SDG", "Sudanese Pound")]
        SudanesePound,

        [EnumMember(Value = "SRD")]
        [DrxEnumExtendedInformation("SRD", "Surinam Dollar")]
        SurinamDollar,

        [EnumMember(Value = "SZL")]
        [DrxEnumExtendedInformation("SZL", "Lilangeni")]
        Lilangeni,

        [EnumMember(Value = "SEK")]
        [DrxEnumExtendedInformation("SEK", "Swedish Krona")]
        SwedishKrona,

        [EnumMember(Value = "CHE")]
        [DrxEnumExtendedInformation("CHE", "WIR Euro")]
        WirEuro,

        [EnumMember(Value = "CHW")]
        [DrxEnumExtendedInformation("CHW", "WIR Franc")]
        WirFranc,

        [EnumMember(Value = "SYP")]
        [DrxEnumExtendedInformation("SYP", "Syrian Pound")]
        SyrianPound,

        [EnumMember(Value = "TWD")]
        [DrxEnumExtendedInformation("TWD", "New Taiwan Dollar")]
        NewTaiwanDollar,

        [EnumMember(Value = "TJS")]
        [DrxEnumExtendedInformation("TJS", "Somoni")]
        Somoni,

        [EnumMember(Value = "TZS")]
        [DrxEnumExtendedInformation("TZS", "Tanzanian Shilling")]
        TanzanianShilling,

        [EnumMember(Value = "THB")]
        [DrxEnumExtendedInformation("THB", "Baht")]
        Baht,

        [EnumMember(Value = "TOP")]
        [DrxEnumExtendedInformation("TOP", "Pa’anga")]
        Paanga,

        [EnumMember(Value = "TTD")]
        [DrxEnumExtendedInformation("TTD", "Trinidad and Tobago Dollar")]
        TrinidadAndTobagoDollar,

        [EnumMember(Value = "TND")]
        [DrxEnumExtendedInformation("TND", "Tunisian Dinar")]
        TunisianDinar,

        [EnumMember(Value = "TRY")]
        [DrxEnumExtendedInformation("TRY", "Turkish Lira")]
        TurkishLira,

        [EnumMember(Value = "TMT")]
        [DrxEnumExtendedInformation("TMT", "Turkmenistan New Manat")]
        TurkmenistanNewManat,

        [EnumMember(Value = "UGX")]
        [DrxEnumExtendedInformation("UGX", "Uganda Shilling")]
        UgandaShilling,

        [EnumMember(Value = "UAH")]
        [DrxEnumExtendedInformation("UAH", "Hryvnia")]
        Hryvnia,

        [EnumMember(Value = "AED")]
        [DrxEnumExtendedInformation("AED", "UAE Dirham")]
        UaeDirham,

        [EnumMember(Value = "UYU")]
        [DrxEnumExtendedInformation("UYU", "Peso Uruguayo")]
        PesoUruguayo,

        [EnumMember(Value = "UZS")]
        [DrxEnumExtendedInformation("UZS", "Uzbekistan Sum")]
        UzbekistanSum,

        [EnumMember(Value = "VUV")]
        [DrxEnumExtendedInformation("VUV", "Vatu")]
        Vatu,

        [EnumMember(Value = "VEF")]
        [DrxEnumExtendedInformation("VEF", "Bolivar")]
        Bolivar,

        [EnumMember(Value = "VND")]
        [DrxEnumExtendedInformation("VND", "Dong")]
        Dong,

        [EnumMember(Value = "YER")]
        [DrxEnumExtendedInformation("YER", "Yemeni Rial")]
        YemeniRial,

        [EnumMember(Value = "ZMW")]
        [DrxEnumExtendedInformation("ZMW", "Zambian Kwacha")]
        ZambianKwacha,

        [EnumMember(Value = "ZWL")]
        [DrxEnumExtendedInformation("ZWL", "Zimbabwe Dollar")]
        ZimbabweDollar
    }
}