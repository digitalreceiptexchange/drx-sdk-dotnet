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
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Net.Dreceiptx
{
    public class DrxCredentials
    {
        public string ApiKey { get; set; }
        public string Secret { get; set; }
        public string RequesterId { get; set; }
    }

    public class ExchangeResponseResult
    {
        public ExchangeResponse ExchangeResponse { get; set; }
    }

    public class ExchangeResponse
    {
        public bool Success { get; set; }
        public int? Code { get; set; }
        public string ExceptionMessage { get; set; }
        public ResponseData ResponseData { get; set; }
        
    }

    public class ResponseData
    {
        public string ReceiptId { get; set; }
        public string Reference { get; set; }
        public string Guid { get; set; }
        public string Rms { get; set; }
    }

    public class UsersResponse
    {
        public UserReponseExchangeResponse ExchangeResponse { get; set; }
    }

    public class UserReponseExchangeResponse
    {
        public bool Success { get; set; }
        public int? Code { get; set; }
        public string ExceptionMessage { get; set; }
        public UserReponseResponseData ResponseData { get; set; }
    }

    public class UserReponseResponseData
    {
        public Dictionary<string, ResponseData> UserIdentifiers { get; set; }
    }
}
