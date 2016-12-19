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
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using log4net;
using Net.Dreceiptx.Client.Exceptions;
using Net.Dreceiptx.Extensions;
using Net.Dreceiptx.Receipt.Config;
using Net.Dreceiptx.Receipt.Serialization.Json;
using Net.Dreceiptx.Users;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using DigitalReceipt = Net.Dreceiptx.Receipt.Serialization.Json.DigitalReceipt;

namespace Net.Dreceiptx.Client
{
    public class DRXClient : IExchangeClient
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(DRXClient));
        private readonly IConfigManager _configManager;
        private static readonly string CONTENT_TYPE_JSON = "application/json";
        private static readonly string CONTENT_TYPE_XML = "application/xml";
        private static readonly string CONTENT_TYPE_PDF = "application/pdf";
        private readonly string USER_AGENT;
        private readonly string _exchangeProtocol = "https";
        private readonly string _directoryProtocol = "https";
        private readonly string _exchangeHostname;
        private readonly string _requesterId;
        private readonly string _receiptVersion;
        
        private readonly string _directoryHostname;
        private readonly string _userVersion;

        /// <summary>
        /// Creates instance of ExchangeClient using the given ConfigManager
        /// </summary>
        /// <param name="configManager"></param>
        public DRXClient(IConfigManager configManager)
        {
            USER_AGENT = "dRx .NET SDK/"+ SDK.VERSION +" Receipt/" + SDK.RECEIPT_VERSION_COMPATIBILITY;
            _configManager = configManager;
            _exchangeHostname = ValidateConfigOption("exchange.hostname");
            _directoryHostname = ValidateConfigOption("directory.hostname");
            _requesterId = ValidateConfigOption("api.requesterId");
            _receiptVersion = ValidateConfigOption("receipt.version");
            _userVersion = ValidateConfigOption("user.version");
        
            if(_configManager.Exists("exchange.protocol")){
                _exchangeProtocol = _configManager.GetConfigValue("exchange.protocol");
            }
        
            if(_configManager.Exists("directory.protocol")){
                _directoryProtocol = _configManager.GetConfigValue("directory.protocol");
            }
        }

        public User SearchUser(UserIdentifierType identifierType, string identifier)
        {
            string encodedIdentifier = HttpUtility.UrlEncode(identifier);
            UriParameters uriParameters = new UriParameters();
            uriParameters.Add("idtype", identifierType.Value());
            return SearchUser(encodedIdentifier, uriParameters);
        }

        private User SearchUser(string encodedIdentifier, UriParameters uriParameters)
        {
            try
            {
                using (HttpClient client = CreateDirectoryConnection("/user/" + encodedIdentifier, _userVersion, uriParameters))
                {
                    var response = client.GetAsync("");
                    var statusCode = response.Result.StatusCode;
                    if (response.Result.StatusCode == HttpStatusCode.OK ||
                        response.Result.StatusCode == HttpStatusCode.BadRequest)
                    {
                        string contentResult = response.Result.Content.ReadAsStringAsync().Result;
                        ExchangeResponseResult exchangeResponse = ExchangeResponseResult.DeserializeFromJson(contentResult);
                        if (exchangeResponse.ExchangeResponse.Success)
                        {
                            User user = new User();
                            user.Guid = exchangeResponse?.ExchangeResponse?.ResponseData?.Guid;
                            user.Rms = exchangeResponse?.ExchangeResponse?.ResponseData?.Rms;
                            return user;
                        }
                        else
                        {
                            throw new ExchangeClientException(exchangeResponse.ExchangeResponse.Code.Value,
                                exchangeResponse.ExchangeResponse.ExceptionMessage);
                        }
                    }
                    else if (response.Result.StatusCode == HttpStatusCode.NotFound)
                    {
                        throw new ExchangeClientException(404,
                            "The exchange host could not be found or is currently unavailable, please check ConfigManager setting and ensure they are correct.");
                    }
                    else if (response.Result.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        string contentResult = response.Result.Content.ReadAsStringAsync().Result;
                        ExchangeResponseResult exchangeResponse =
                            JsonConvert.DeserializeObject<ExchangeResponseResult>(contentResult,
                                new JsonSerializerSettings
                                {
                                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                                    DateTimeZoneHandling = DateTimeZoneHandling.Utc
                                });
                        throw new ExchangeClientException(exchangeResponse.ExchangeResponse.Code.Value,
                            exchangeResponse.ExchangeResponse.ExceptionMessage);
                    }
                    else
                    {
                        //TODO: Not sure if errorMessage is correc here
                        string errorMessage = response.Result.Content.ToString();
                        throw new ExchangeClientException((int)statusCode, errorMessage);
                    }
                }

            }
            //catch (SocketTimeoutException te){
            //    throw new ExchangeClientException(500, "The connection to the exchange timed out and did not receive a response", te);
            //}
            catch (ExchangeClientException dRxE)
            {
                throw dRxE;
            }
            catch (Exception e)
            {
                throw new ExchangeClientException(500, e.ToString(), e);
            }
        }

        private HttpClient CreateDirectoryConnection(string uri, string requestVersion, UriParameters uriParameters)
        {
            return CreateConnection(_directoryProtocol, _directoryHostname, uri, requestVersion, uriParameters);
        }

        private HttpClient CreateExchangeConnection(string uri, string requestVersion, UriParameters uriParameters)
        {
            return CreateConnection(_exchangeProtocol, _exchangeHostname, uri, requestVersion, uriParameters);
        }

        private HttpClient CreateConnection(string protocol, string hostname, string uri, 
            string requestVersion, UriParameters uriParameters)
        {
            var client = new HttpClient();

            var url = new Uri($"{protocol}://{hostname}{uri}");
            uriParameters?.GetKeyValuePairs().ForEach(x => url = url.AddQuery(x.Key, x.Value));
            client.BaseAddress = url;

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(CONTENT_TYPE_JSON));
            long timestamp = (long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds;
            client.DefaultRequestHeaders.Add("x-drx-timestamp", timestamp.ToString());
            client.DefaultRequestHeaders.Add("x-drx-requester", _requesterId);
            client.DefaultRequestHeaders.Add("x-drx-version", requestVersion);
            client.DefaultRequestHeaders.Add("User-Agent", USER_AGENT);
            string auth = $"DRX {CreateAuthKey(timestamp)}";
            client.DefaultRequestHeaders.Add("Authorization", auth);
            client.Timeout = new TimeSpan(0, 0, 0, 30);
            return client;
        }

        public List<User> SearchUsers(UserIdentifierType identifierType, List<string> userIdentifiers)  {
            if (Log.IsDebugEnabled)
            {
                Log.DebugFormat("SearchUsers Entering...{0}, {1}", identifierType, string.Join(", ", userIdentifiers));
            }
            List<User> users = new List<User>();
            UriParameters uriParameters = new UriParameters();
            uriParameters.Add("idtype", identifierType.Value().ToLower());
            StringBuilder userIdentifiersParam = new StringBuilder();
            bool firstIteration = true;
            foreach (string userIdentifier in userIdentifiers)
            {
                if (!firstIteration)
                {
                    userIdentifiersParam.Append(";");
                }
                userIdentifiersParam.Append(userIdentifier);
                firstIteration = false;
            }
            uriParameters.Add("identifiers", userIdentifiersParam.ToString());
            try
            {
                using (HttpClient client = CreateDirectoryConnection("/user", _userVersion, uriParameters))
                {
                    Log.Debug("Request user data...");
                    var response = client.GetAsync("");
                    var statusCode = response.Result.StatusCode;
                    if (response.Result.StatusCode == HttpStatusCode.OK)
                    {
                        string contentResult = response.Result.Content.ReadAsStringAsync().Result;
                        Log.DebugFormat("Response Content: {0}", contentResult);
                        UsersResponse exchangeResponse = UsersResponse.DeserializeFromJson(contentResult);
                        if (exchangeResponse.ExchangeResponse.Success)
                        {
                            foreach (var keyValuePair in exchangeResponse.ExchangeResponse.ResponseData.UserIdentifiers)
                            {
                                User user = new User();
                                user.IdentifierType = identifierType;
                                user.Identifier = keyValuePair.Key;
                                user.Guid = keyValuePair.Value.Guid;
                                user.Rms = keyValuePair.Value.Rms;
                                users.Add(user);
                            }
                            return users;
                        }
                        throw new ExchangeClientException(exchangeResponse.ExchangeResponse.Code.Value,
                            exchangeResponse.ExchangeResponse.ExceptionMessage);
                    }
                    if (response.Result.StatusCode == HttpStatusCode.NotFound)
                    {
                        throw new ExchangeClientException(404,
                            "The exchange host could not be found or is currently unavailable, please check ConfigManager setting and ensure they are correct.");
                    }
                    if (response.Result.StatusCode == HttpStatusCode.BadGateway ||
                        response.Result.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        string contentResult = response.Result.Content.ReadAsStringAsync().Result;
                        ExchangeResponseResult exchangeResponse = ExchangeResponseResult.DeserializeFromJson(contentResult);
                        throw new ExchangeClientException(exchangeResponse.ExchangeResponse.Code.Value,
                            exchangeResponse.ExchangeResponse.ExceptionMessage);
                    }
                    //TODO: Not sure if errorMessage is correc here
                    string errorMessage = response.Result.Content.ToString();
                    throw new ExchangeClientException((int)statusCode, errorMessage);
                }
            }
            catch (ExchangeClientException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new ExchangeClientException(500, e.ToString(), e);
            }
            finally 
            {
                Log.Debug("SearchUsers Exiting...");
            }

        }

        public string SendReceipt(DigitalReceipt receipt)
        {
            Log.DebugFormat("SendReceipt Entering...");
            try
            {
                using (HttpClient client = CreateExchangeConnection("/receipt", _receiptVersion, null))
                {
                    string request = new DigitalReceiptMessageWrapper {DRxDigitalReceipt = receipt}.SerializeToJson();

                    StringContent content = new StringContent(request, Encoding.UTF8, "application/json");
                    Log.DebugFormat("Sending {0}", request);

                    var response = client.PostAsync("", content);
                    var statusCode = response.Result.StatusCode;

                    if (response.Result.StatusCode == HttpStatusCode.Created ||
                        response.Result.StatusCode == HttpStatusCode.BadRequest)
                    {
                        string contentResult = response.Result.Content.ReadAsStringAsync().Result;
                        Log.DebugFormat("Response {0}", contentResult);
                        ExchangeResponseResult exchangeResponse =
                            ExchangeResponseResult.DeserializeFromJson(contentResult);
                        if (exchangeResponse.ExchangeResponse.Success)
                        {
                            return exchangeResponse.ExchangeResponse?.ResponseData?.ReceiptId;
                        }
                        throw new ExchangeClientException(exchangeResponse.ExchangeResponse.Code.Value,
                            exchangeResponse.ExchangeResponse.ExceptionMessage);
                    }
                    if (response.Result.StatusCode == HttpStatusCode.NotFound)
                    {
                        throw new ExchangeClientException(404,
                            "The exchange host could not be found or is currently unavailable, please check ConfigManager setting and ensure they are correct.");
                    }
                    if (response.Result.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        string contentResult = response.Result.Content.ReadAsStringAsync().Result;
                        ExchangeResponseResult exchangeResponse =
                            ExchangeResponseResult.DeserializeFromJson(contentResult);
                        throw new ExchangeClientException(exchangeResponse.ExchangeResponse.Code.Value,
                            exchangeResponse.ExchangeResponse.ExceptionMessage);
                    }
                    //TODO: Not sure if errorMessage is correc here
                    string errorMessage = response.Result.Content.ToString();
                    throw new ExchangeClientException((int) statusCode, errorMessage);
                }
            }
            catch (ExchangeClientException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new ExchangeClientException(500, e.ToString(), e);
            }
            finally
            {
                Log.Debug("Send Receipt Exiting...");
            }
        }

    
        public NewUserRegistrationExchangeResponse RegisterNewUser(NewUser newUser)
        {
            List<NewUser> newUserCollection = new List<NewUser>();
            newUserCollection.Add(newUser);
            return RegisterNewUser(newUserCollection);
        }
    
        public NewUserRegistrationExchangeResponse RegisterNewUser(List<NewUser> newUsers)
        {
            Log.DebugFormat("RegisterNewUser Entering...");
            using (HttpClient client = CreateExchangeConnection("/user", _userVersion, null))
            {
                string request = new NewUserRegistrationRequest() {Users = newUsers}.SerializeToJsonString();
                    
                StringContent content = new StringContent(request, Encoding.UTF8, CONTENT_TYPE_JSON);
                Log.DebugFormat("RegisterNewUser Request: {0}", request);
                var response = client.PostAsync("", content);
                var statusCode = response.Result.StatusCode;
                if (response.Result.StatusCode == HttpStatusCode.Created ||
                    response.Result.StatusCode == HttpStatusCode.BadRequest)
                {

                    string contentResult = response.Result.Content.ReadAsStringAsync().Result;
                    Log.DebugFormat("RegisterNewUser Response: {0}", contentResult);
                    NewUserRegistrationExchangeResponse exchangeResponse = NewUserRegistrationExchangeResponse.DeserializeFromJson(contentResult);
                    return exchangeResponse;
                        
                }
                if (response.Result.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new ExchangeClientException(404,
                        "The exchange host could not be found or is currently unavailable, please check ConfigManager setting and ensure they are correct.");
                }
                if (response.Result.StatusCode == HttpStatusCode.Unauthorized)
                {
                    string contentResult = response.Result.Content.ReadAsStringAsync().Result;
                    Log.DebugFormat("RegisterNewUser Response: {0}", contentResult);
                    ExchangeResponseResult exchangeResponse = ExchangeResponseResult.DeserializeFromJson(contentResult);
                    throw new ExchangeClientException(exchangeResponse.ExchangeResponse.Code.Value,
                        exchangeResponse.ExchangeResponse.ExceptionMessage);
                }
                //TODO: Not sure if errorMessage is correc here
                string errorMessage = response.Result.Content.ToString();
                throw new ExchangeClientException((int)statusCode, errorMessage);
            }
        }


        public string CreateAuthKey(double timestamp)
        {
            string key = $"{_configManager.GetConfigValue("api.key")}:{timestamp}:{_configManager.GetConfigValue("api.requesterId")}";

            using (var mac = new HMACSHA256(Encoding.UTF8.GetBytes(_configManager.GetConfigValue("api.secret"))))
            {
                byte[] hashmessage = mac.ComputeHash(Encoding.UTF8.GetBytes(key));

                return Convert.ToBase64String(hashmessage);
            }
        }
    
        private string ValidateConfigOption(string configParameter)  {
            if(_configManager.Exists(configParameter)){
                return _configManager.GetConfigValue(configParameter);
            }else{
                throw new ExchangeClientException(101, "Required config parameter "+configParameter+" not supplied");
            }
        }
    }
}