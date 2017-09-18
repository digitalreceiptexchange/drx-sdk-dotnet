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
namespace Net.Dreceiptx.Receipt.Config
{
    public static class ConfigKeys
    {
        public static readonly string dRxGLN = "drx.gln";
        public static readonly string MerchantGLN = "merchant.gln";
        public static readonly string DefaultCurrency = "default.currency";
        public static readonly string DefaultCountry = "default.country";
        public static readonly string DefaultLanguage = "default.language";
        public static readonly string DefaultTimeZone = "default.timezone";
        public static readonly string DefaultTaxCategory = "default.taxCategory";
        public static readonly string DefaultTaxCode = "default.taxCode";
        public static readonly string APIRequesterId = "api.requesterId";
        public static readonly string APISecret = "api.secret";
        public static readonly string APIKey = "api.key";
        public static readonly string ExchangeProtocol = "exchange.protocol";
        public static readonly string ExchangeHost = "exchange.hostname";
        public static readonly string DirectoryProtocol = "directory.protocol";
        public static readonly string DirectoryHost = "directory.hostname";
        public static readonly string ReceiptVersion = "receipt.version";
        public static readonly string UserVersion = "user.version";
        public static readonly string DownloadDirectory = "download.directory";
    }
}
