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
using Net.Dreceiptx.Receipt.Common;
using Net.Dreceiptx.Users;

namespace Net.Dreceiptx.Receipt.Tax
{
    public enum TaxCode
    {
        [DrxEnumExtendedInformation("AAD", "Tobacco tax")]
        [EnumMember(Value = "AAD")]
        TobaccoTax,

        [DrxEnumExtendedInformation("AAF", "Coffee tax")]
        [EnumMember(Value = "AAF")]
        CoffeeTax,

        [DrxEnumExtendedInformation("EXC", "Excise duty")]
        [EnumMember(Value = "EXC")]
        ExciseDuty,

        [DrxEnumExtendedInformation("AAJ", "Tax on replacement part")]
        [EnumMember(Value = "AAJ")]
        ReplacementPartTax,

        [DrxEnumExtendedInformation("GST", "Good and services tax")]
        [EnumMember(Value = "GST")]
        GoodsAndServicesTax,

        [DrxEnumExtendedInformation("ENV", "Environmental tax")]
        [EnumMember(Value = "ENV")]
        EnvironmentalTax,

        [DrxEnumExtendedInformation("VAT", "value added tax")]
        [EnumMember(Value = "VAT")]
        ValueAddedTax,
        [DrxEnumExtendedInformation("IMP", "Import tax")]
        [EnumMember(Value = "IMP")]
        ImportTax,

        [DrxEnumExtendedInformation("OTH", "Other taxes")]
        [EnumMember(Value = "OTH")]
        OtherTaxes
    }

    public class TaxCodeManager
    {
        private static readonly EnumExtensions.DrxEnumExtendedInformationHelper<TaxCode> Converter = new EnumExtensions.DrxEnumExtendedInformationHelper<TaxCode>();
        public static TaxCode GetTaxCode(string code)
        {
            return Converter.GetByValue(code);
        }
    }
}