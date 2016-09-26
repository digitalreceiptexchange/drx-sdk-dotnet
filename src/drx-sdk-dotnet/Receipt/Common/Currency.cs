/*
 * Copyright 2016 Digital Receipt Exchange Limited
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

package net.dreceiptx.receipt.common;

import com.google.gson.annotations.SerializedName;
import java.util.HashMap;
import java.util.Map;

public enum Currency {
    @SerializedName("AFN")
    Afghani("AFN", "Afghani"),
    @SerializedName("EUR")
    Euro("EUR", "Euro"),
    @SerializedName("ALL")
    Lek("ALL", "Lek"),
    @SerializedName("DZD")
    Algerian_Dinar("DZD", "Algerian Dinar"),
    @SerializedName("USD")
    US_Dollar("USD", "US Dollar"),
    @SerializedName("AOA")
    Kwanza("AOA", "Kwanza"),
    @SerializedName("XCD")
    East_Caribbean_Dollar("XCD", "East Caribbean Dollar"),
    @SerializedName("ARS")
    Argentine_Peso("ARS", "Argentine Peso"),
    @SerializedName("AMD")
    Armenian_Dram("AMD", "Armenian Dram"),
    @SerializedName("AWG")
    Aruban_Florin("AWG", "Aruban Florin"),
    @SerializedName("AUD")
    Australian_Dollar("AUD", "Australian Dollar"),
    @SerializedName("AZN")
    Azerbaijanian_Manat("AZN", "Azerbaijanian Manat"),
    @SerializedName("BSD")
    Bahamian_Dollar("BSD", "Bahamian Dollar"),
    @SerializedName("BHD")
    Bahraini_Dinar("BHD", "Bahraini Dinar"),
    @SerializedName("BDT")
    Taka("BDT", "Taka"),
    @SerializedName("BBD")
    Barbados_Dollar("BBD", "Barbados Dollar"),
    @SerializedName("BYR")
    Belarussian_Ruble("BYR", "Belarussian Ruble"),
    @SerializedName("BZD")
    Belize_Dollar("BZD", "Belize Dollar"),
    @SerializedName("XOF")
    CFA_Franc_BCEAO("XOF", "CFA Franc BCEAO"),
    @SerializedName("BMD")
    Bermudian_Dollar("BMD", "Bermudian Dollar"),
    @SerializedName("BTN")
    Ngultrum("BTN", "Ngultrum"),
    @SerializedName("INR")
    Indian_Rupee("INR", "Indian Rupee"),
    @SerializedName("BOB")
    Boliviano("BOB", "Boliviano"),
    @SerializedName("BOV")
    Mvdol("BOV", "Mvdol"),
    @SerializedName("BAM")
    Convertible_Mark("BAM", "Convertible Mark"),
    @SerializedName("BWP")
    Pula("BWP", "Pula"),
    @SerializedName("NOK")
    Norwegian_Krone("NOK", "Norwegian Krone"),
    @SerializedName("BRL")
    Brazilian_Real("BRL", "Brazilian Real"),
    @SerializedName("BND")
    Brunei_Dollar("BND", "Brunei Dollar"),
    @SerializedName("BGN")
    Bulgarian_Lev("BGN", "Bulgarian Lev"),
    @SerializedName("BIF")
    Burundi_Franc("BIF", "Burundi Franc"),
    @SerializedName("CVE")
    Cabo_Verde_Escudo("CVE", "Cabo Verde Escudo"),
    @SerializedName("KHR")
    Riel("KHR", "Riel"),
    @SerializedName("XAF")
    CFA_Franc_BEAC("XAF", "CFA Franc BEAC"),
    @SerializedName("CAD")
    Canadian_Dollar("CAD", "Canadian Dollar"),
    @SerializedName("KYD")
    Cayman_Islands_Dollar("KYD", "Cayman Islands Dollar"),
    @SerializedName("CLF")
    Unidad_de_Fomento("CLF", "Unidad de Fomento"),
    @SerializedName("CLP")
    Chilean_Peso("CLP", "Chilean Peso"),
    @SerializedName("CNY")
    Yuan_Renminbi("CNY", "Yuan Renminbi"),
    @SerializedName("COP")
    Colombian_Peso("COP", "Colombian Peso"),
    @SerializedName("COU")
    Unidad_de_Valor_Real("COU", "Unidad de Valor Real"),
    @SerializedName("KMF")
    Comoro_Franc("KMF", "Comoro Franc"),
    @SerializedName("CDF")
    Congolese_Franc("CDF", "Congolese Franc"),
    @SerializedName("NZD")
    New_Zealand_Dollar("NZD", "New Zealand Dollar"),
    @SerializedName("CRC")
    Costa_Rican_Colon("CRC", "Costa Rican Colon"),
    @SerializedName("HRK")
    Kuna("HRK", "Kuna"),
    @SerializedName("CUC")
    Peso_Convertible("CUC", "Peso Convertible"),
    @SerializedName("CUP")
    Cuban_Peso("CUP", "Cuban Peso"),
    @SerializedName("ANG")
    Netherlands_Antillean_Guilder("ANG", "Netherlands Antillean Guilder"),
    @SerializedName("CZK")
    Czech_Koruna("CZK", "Czech Koruna"),
    @SerializedName("DKK")
    Danish_Krone("DKK", "Danish Krone"),
    @SerializedName("DJF")
    Djibouti_Franc("DJF", "Djibouti Franc"),
    @SerializedName("DOP")
    Dominican_Peso("DOP", "Dominican Peso"),
    @SerializedName("EGP")
    Egyptian_Pound("EGP", "Egyptian Pound"),
    @SerializedName("SVC")
    El_Salvador_Colon("SVC", "El Salvador Colon"),
    @SerializedName("ERN")
    Nakfa("ERN", "Nakfa"),
    @SerializedName("ETB")
    Ethiopian_Birr("ETB", "Ethiopian Birr"),
    @SerializedName("FKP")
    Falkland_Islands_Pound("FKP", "Falkland Islands Pound"),
    @SerializedName("FJD")
    Fiji_Dollar("FJD", "Fiji Dollar"),
    @SerializedName("XPF")
    CFP_Franc("XPF", "CFP Franc"),
    @SerializedName("GMD")
    Dalasi("GMD", "Dalasi"),
    @SerializedName("GEL")
    Lari("GEL", "Lari"),
    @SerializedName("GHS")
    Ghana_Cedi("GHS", "Ghana Cedi"),
    @SerializedName("GIP")
    Gibraltar_Pound("GIP", "Gibraltar Pound"),
    @SerializedName("GTQ")
    Quetzal("GTQ", "Quetzal"),
    @SerializedName("GBP")
    British_Sterling("GBP", "British Sterling(Pound)"),
    @SerializedName("GNF")
    Guinea_Franc("GNF", "Guinea Franc"),
    @SerializedName("GYD")
    Guyana_Dollar("GYD", "Guyana Dollar"),
    @SerializedName("HTG")
    Gourde("HTG", "Gourde"),
    @SerializedName("HNL")
    Lempira("HNL", "Lempira"),
    @SerializedName("HKD")
    Hong_Kong_Dollar("HKD", "Hong Kong Dollar"),
    @SerializedName("HUF")
    Forint("HUF", "Forint"),
    @SerializedName("ISK")
    Iceland_Krona("ISK", "Iceland Krona"),
    @SerializedName("IDR")
    Rupiah("IDR", "Rupiah"),
    @SerializedName("IRR")
    Iranian_Rial("IRR", "Iranian Rial"),
    @SerializedName("IQD")
    Iraqi_Dinar("IQD", "Iraqi Dinar"),
    @SerializedName("ILS")
    New_Israeli_Sheqel("ILS", "New Israeli Sheqel"),
    @SerializedName("JMD")
    Jamaican_Dollar("JMD", "Jamaican Dollar"),
    @SerializedName("JPY")
    Yen("JPY", "Yen"),
    @SerializedName("JOD")
    Jordanian_Dinar("JOD", "Jordanian Dinar"),
    @SerializedName("KZT")
    Tenge("KZT", "Tenge"),
    @SerializedName("KES")
    Kenyan_Shilling("KES", "Kenyan Shilling"),
    @SerializedName("KPW")
    North_Korean_Won("KPW", "North Korean Won"),
    @SerializedName("KRW")
    Won("KRW", "Won"),
    @SerializedName("KWD")
    Kuwaiti_Dinar("KWD", "Kuwaiti Dinar"),
    @SerializedName("KGS")
    Som("KGS", "Som"),
    @SerializedName("LAK")
    Kip("LAK", "Kip"),
    @SerializedName("LBP")
    Lebanese_Pound("LBP", "Lebanese Pound"),
    @SerializedName("LSL")
    Loti("LSL", "Loti"),
    @SerializedName("ZAR")
    Rand("ZAR", "Rand"),
    @SerializedName("LRD")
    Liberian_Dollar("LRD", "Liberian Dollar"),
    @SerializedName("LYD")
    Libyan_Dinar("LYD", "Libyan Dinar"),
    @SerializedName("CHF")
    Swiss_Franc("CHF", "Swiss Franc"),
    @SerializedName("MOP")
    Pataca("MOP", "Pataca"),
    @SerializedName("MKD")
    Denar("MKD", "Denar"),
    @SerializedName("MGA")
    Malagasy_Ariary("MGA", "Malagasy Ariary"),
    @SerializedName("MWK")
    Kwacha("MWK", "Kwacha"),
    @SerializedName("MYR")
    Malaysian_Ringgit("MYR", "Malaysian Ringgit"),
    @SerializedName("MVR")
    Rufiyaa("MVR", "Rufiyaa"),
    @SerializedName("MRO")
    Ouguiya("MRO", "Ouguiya"),
    @SerializedName("MUR")
    Mauritius_Rupee("MUR", "Mauritius Rupee"),
    @SerializedName("XUA")
    ADB_Unit_of_Account("XUA", "ADB Unit of Account"),
    @SerializedName("MXN")
    Mexican_Peso("MXN", "Mexican Peso"),
    @SerializedName("MDL")
    Moldovan_Leu("MDL", "Moldovan Leu"),
    @SerializedName("MNT")
    Tugrik("MNT", "Tugrik"),
    @SerializedName("MAD")
    Moroccan_Dirham("MAD", "Moroccan Dirham"),
    @SerializedName("MZN")
    Mozambique_Metical("MZN", "Mozambique Metical"),
    @SerializedName("MMK")
    Kyat("MMK", "Kyat"),
    @SerializedName("NAD")
    Namibia_Dollar("NAD", "Namibia Dollar"),
    @SerializedName("NPR")
    Nepalese_Rupee("NPR", "Nepalese Rupee"),
    @SerializedName("NIO")
    Cordoba_Oro("NIO", "Cordoba Oro"),
    @SerializedName("NGN")
    Naira("NGN", "Naira"),
    @SerializedName("OMR")
    Rial_Omani("OMR", "Rial Omani"),
    @SerializedName("PKR")
    Pakistan_Rupee("PKR", "Pakistan Rupee"),
    @SerializedName("PAB")
    Balboa("PAB", "Balboa"),
    @SerializedName("PGK")
    Kina("PGK", "Kina"),
    @SerializedName("PYG")
    Guarani("PYG", "Guarani"),
    @SerializedName("PEN")
    Nuevo_Sol("PEN", "Nuevo Sol"),
    @SerializedName("PHP")
    Philippine_Peso("PHP", "Philippine Peso"),
    @SerializedName("PLN")
    Zloty("PLN", "Zloty"),
    @SerializedName("QAR")
    Qatari_Rial("QAR", "Qatari Rial"),
    @SerializedName("RON")
    Romanian_Leu("RON", "Romanian Leu"),
    @SerializedName("RUB")
    Russian_Ruble("RUB", "Russian Ruble"),
    @SerializedName("RWF")
    Rwanda_Franc("RWF", "Rwanda Franc"),
    @SerializedName("SHP")
    Saint_Helena_Pound("SHP", "Saint Helena Pound"),
    @SerializedName("WST")
    Tala("WST", "Tala"),
    @SerializedName("STD")
    Dobra("STD", "Dobra"),
    @SerializedName("SAR")
    Saudi_Riyal("SAR", "Saudi Riyal"),
    @SerializedName("RSD")
    Serbian_Dinar("RSD", "Serbian Dinar"),
    @SerializedName("SCR")
    Seychelles_Rupee("SCR", "Seychelles Rupee"),
    @SerializedName("SLL")
    Leone("SLL", "Leone"),
    @SerializedName("SGD")
    Singapore_Dollar("SGD", "Singapore Dollar"),
    @SerializedName("XSU")
    Sucre("XSU", "Sucre"),
    @SerializedName("SBD")
    Solomon_Islands_Dollar("SBD", "Solomon Islands Dollar"),
    @SerializedName("SOS")
    Somali_Shilling("SOS", "Somali Shilling"),
    @SerializedName("SSP")
    South_Sudanese_Pound("SSP", "South Sudanese Pound"),
    @SerializedName("LKR")
    Sri_Lanka_Rupee("LKR", "Sri Lanka Rupee"),
    @SerializedName("SDG")
    Sudanese_Pound("SDG", "Sudanese Pound"),
    @SerializedName("SRD")
    Surinam_Dollar("SRD", "Surinam Dollar"),
    @SerializedName("SZL")
    Lilangeni("SZL", "Lilangeni"),
    @SerializedName("SEK")
    Swedish_Krona("SEK", "Swedish Krona"),
    @SerializedName("CHE")
    WIR_Euro("CHE", "WIR Euro"),
    @SerializedName("CHW")
    WIR_Franc("CHW", "WIR Franc"),
    @SerializedName("SYP")
    Syrian_Pound("SYP", "Syrian Pound"),
    @SerializedName("TWD")
    New_Taiwan_Dollar("TWD", "New Taiwan Dollar"),
    @SerializedName("TJS")
    Somoni("TJS", "Somoni"),
    @SerializedName("TZS")
    Tanzanian_Shilling("TZS", "Tanzanian Shilling"),
    @SerializedName("THB")
    Baht("THB", "Baht"),
    @SerializedName("TOP")
    Paanga("TOP", "Pa’anga"),
    @SerializedName("TTD")
    Trinidad_and_Tobago_Dollar("TTD", "Trinidad and Tobago Dollar"),
    @SerializedName("TND")
    Tunisian_Dinar("TND", "Tunisian Dinar"),
    @SerializedName("TRY")
    Turkish_Lira("TRY", "Turkish Lira"),
    @SerializedName("TMT")
    Turkmenistan_New_Manat("TMT", "Turkmenistan New Manat"),
    @SerializedName("UGX")
    Uganda_Shilling("UGX", "Uganda Shilling"),
    @SerializedName("UAH")
    Hryvnia("UAH", "Hryvnia"),
    @SerializedName("AED")
    UAE_Dirham("AED", "UAE Dirham"),
    @SerializedName("UYU")
    Peso_Uruguayo("UYU", "Peso Uruguayo"),
    @SerializedName("UZS")
    Uzbekistan_Sum("UZS", "Uzbekistan Sum"),
    @SerializedName("VUV")
    Vatu("VUV", "Vatu"),
    @SerializedName("VEF")
    Bolivar("VEF", "Bolivar"),
    @SerializedName("VND")
    Dong("VND", "Dong"),
    @SerializedName("YER")
    Yemeni_Rial("YER", "Yemeni Rial"),
    @SerializedName("ZMW")
    Zambian_Kwacha("ZMW", "Zambian Kwacha"),
    @SerializedName("ZWL")
    Zimbabwe_Dollar("ZWL", "Zimbabwe Dollar");

    private String _value;

    private String _displayName;
    
    private static Map<String, Currency> enums = new HashMap<String, Currency>();

    static {
        for (Currency CurrencyEnum : Currency.values()) {
            enums.put(CurrencyEnum._value, CurrencyEnum);
        }
    }

    Currency(String value, String name) {
        _value = value;
        _displayName = name;
    }

    public String getValue() {
        return _value;
    }

    public String getDisplayName() {
        return _displayName;
    }

    @Override
    public String toString() {
        return _displayName;
    }
    
    private Currency(final String currencyCode) { _value = currencyCode; }

    public static Currency codeOf(String currencyCode) {
        return enums.get(currencyCode);
    }
}
