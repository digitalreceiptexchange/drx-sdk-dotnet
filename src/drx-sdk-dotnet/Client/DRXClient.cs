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
using Net.Dreceiptx.Client.Exceptions;
using Net.Dreceiptx.Extensions;
using Net.Dreceiptx.Receipt;
using Net.Dreceiptx.Receipt.Config;
using Net.Dreceiptx.Receipt.Merchant;
using Net.Dreceiptx.Receipt.Serialization.Json;
using Net.Dreceiptx.Users;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Net.Dreceiptx.Client
{
    public class DRXClient : IExchangeClient
    {
        private static readonly int BUFFER_SIZE = 4096;
        private readonly IConfigManager _configManager;
        private static readonly string CONTENT_TYPE_JSON = "application/json";
        private static readonly string CONTENT_TYPE_XML = "application/xml";
        private static readonly string CONTENT_TYPE_PDF = "application/pdf";
        private string USER_AGENT;
        private readonly EnvironmentType _environmentType;
        private string _exchangeProtocol = "https";
        private readonly string _exchangeHostname;
        private readonly string _requesterId;
        private readonly string _receiptVersion;
        private string _directoryProtocol = "https";
        private readonly string _directoryHostname;
        private readonly string _userVersion;
        private readonly string _downloadDirectory;
        private int _responseErrorCode = 500;
        private string _responseErrorMessage = "Unknown Error";
        private readonly JsonSerializerSettings _jsonSerializerSettings;

        /**
         * Creates instance of ExchangeClient using the given ConfigManager
         *
         * @param configManager the ConfigManager to be used by the ExchangeClient
         * @throws ExchangeClientException
         */
        public DRXClient(IConfigManager configManager)
        {
            USER_AGENT = "dRx .NET SDK/"+ SDK.VERSION +" Receipt/" + SDK.RECEIPT_VERSION_COMPATIBILITY;
            _configManager = configManager;
            _exchangeHostname = ValidateConfigOption("exchange.hostname");
            _directoryHostname = ValidateConfigOption("directory.hostname");
            _requesterId = ValidateConfigOption("api.requesterId");
            _receiptVersion = ValidateConfigOption("receipt.version");
            _userVersion = ValidateConfigOption("user.version");
            _downloadDirectory = ValidateConfigOption("download.directory");
        
            if(_configManager.Exists("exchange.protocol")){
                EnvironmentType.TryParse(_configManager.GetConfigValue("environment.type"), out _environmentType);
            }else{
                _environmentType = EnvironmentType.PROD;
            }
        
            if(_configManager.Exists("exchange.protocol")){
                _exchangeProtocol = _configManager.GetConfigValue("exchange.protocol");
            }
        
            if(_configManager.Exists("directory.protocol")){
                _directoryProtocol = _configManager.GetConfigValue("directory.protocol");
            }
        
            switch (_environmentType) {
                case EnvironmentType.PROD:
                    _exchangeProtocol = "https";
                    _directoryProtocol = "https";
                break;
            }

            _jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateTimeZoneHandling = DateTimeZoneHandling.Utc
            };
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
                        ExchangeResponseResult exchangeResponse = JsonConvert.DeserializeObject<ExchangeResponseResult>(
                            contentResult, _jsonSerializerSettings);
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
            string auth = "DRX " + CreateAuthKey(timestamp);
            client.DefaultRequestHeaders.Add("Authorization", auth);
            client.Timeout = new TimeSpan(0, 0, 0, 30);
            return client;
        }


        public List<User> SearchUsers(UserIdentifierType identifierType, List<string> userIdentifiers)  {
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
                    var response = client.GetAsync("");
                    var statusCode = response.Result.StatusCode;
                    if (response.Result.StatusCode == HttpStatusCode.OK)
                    {
                        string contentResult = response.Result.Content.ReadAsStringAsync().Result;
                        UsersResponse exchangeResponse = JsonConvert.DeserializeObject<UsersResponse>(contentResult, _jsonSerializerSettings);
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
                    else if (response.Result.StatusCode == HttpStatusCode.BadGateway ||
                        response.Result.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        string contentResult = response.Result.Content.ReadAsStringAsync().Result;
                        ExchangeResponseResult exchangeResponse =
                            JsonConvert.DeserializeObject<ExchangeResponseResult>(contentResult, _jsonSerializerSettings);
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
            //catch (ConnectException ce)
            //{
            //    throw new ExchangeClientException(500, "There was a connection exception, please ensure internet connectivity and exchange host settings", ce);
            //}
            //catch (SocketTimeoutException te)
            //{
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

        public string SendReceipt(DigitalReceiptMessage receipt)
        {
            try
            { 
                using (HttpClient client = CreateExchangeConnection("/receipt", _receiptVersion, null))
                {
                    JsonSerializerSettings settings = new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver(),
                        //DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                        DateFormatString = "yyyy-MM-ddTHH:mm:ss%K",
                        Formatting = Formatting.Indented,
                        NullValueHandling = NullValueHandling.Ignore,
                        
                        //TypeNameHandling =  TypeNameHandling.Objects
                    };
                    settings.Converters.Add(new StringEnumConverter());
                    string result = new DigitalReceiptMessageWrapper {DRxDigitalReceipt = receipt}.Serialize();

                    StringContent content = new StringContent(result, Encoding.UTF8, "application/json");
                    var response = client.PostAsync("", content);
                    var statusCode = response.Result.StatusCode;
                    if (response.Result.StatusCode == HttpStatusCode.Created || response.Result.StatusCode == HttpStatusCode.BadRequest)
                    {
                        string contentResult = response.Result.Content.ReadAsStringAsync().Result;
                        ExchangeResponseResult exchangeResponse = JsonConvert.DeserializeObject<ExchangeResponseResult>(contentResult, _jsonSerializerSettings);
                        if (exchangeResponse.ExchangeResponse.Success)
                        {
                            return exchangeResponse.ExchangeResponse?.ResponseData?.ReceiptId;
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
                            JsonConvert.DeserializeObject<ExchangeResponseResult>(contentResult, _jsonSerializerSettings);
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
            catch (ExchangeClientException dRxE)
            {
                throw dRxE;
            }
            catch (Exception e)
            {
                throw new ExchangeClientException(500, e.ToString(), e);
            }
        }

        public DigitalReceipt LookupReceipt(string receiptId)
        {
//            try {
//                using (HttpClient client = CreateConnection("/receipt/" + receiptId, _receiptVersion, null))
//                {
//                    var response = client.GetAsync("");
//                    var statusCode = response.Result.StatusCode;
//                    if (response.Result.StatusCode == HttpStatusCode.OK)
//                    {
//                        string contentResult = response.Result.Content.ReadAsStringAsync().Result;
//                        ExchangeResponseResult exchangeResponse = JsonConvert.DeserializeObject<ExchangeResponseResult>(
//                            contentResult, _jsonSerializerSettings);
//                        if (exchangeResponse.ExchangeResponse.Success)
//                        {
//                            User user = new User();
//                            user.Guid = exchangeResponse?.ExchangeResponse?.ResponseData?.Guid;
//                            user.Rms = exchangeResponse?.ExchangeResponse?.ResponseData?.Rms;
//                            return user;
//                        }
//                        else
//                        {
//                            throw new ExchangeClientException(exchangeResponse.ExchangeResponse.Code.Value,
//                                exchangeResponse.ExchangeResponse.ExceptionMessage);
//                        }
//                    }
//                    else if (response.Result.StatusCode == HttpStatusCode.NotFound)
//                    {
//                        throw new ExchangeClientException(404,
//                            "The exchange host could not be found or is currently unavailable, please check ConfigManager setting and ensure they are correct.");
//                    }
//                    int responseCode = connection.getResponseCode();
//                if (responseCode == HttpCodes.HTTP_200_OK) {
//                    string response = getResponsestring(connection);
//                    connection.disconnect();
//                    return new DigitalReceipt(response);
//                } else if (responseCode == HttpCodes.HTTP_404_NOTFOUND) {
//                    connection.disconnect();
//                    throw new ExchangeClientException(404, "The exchange host could not be found or is currently unavailable, please check ConfigManager setting and ensure they are correct.");
//                } else if (responseCode == HttpCodes.HTTP_400_BAD_REQUEST || responseCode == HttpCodes.HTTP_401_UNAUTHORIZED) {
//                    loadErrorResponseJsonObject(connection);
//                    connection.disconnect();
//                    throw new ExchangeClientException(_responseErrorCode, _responseErrorMessage);
//                } else {
//                    string errorMessage = connection.getResponseMessage();
//                    connection.disconnect();
//                    throw new ExchangeClientException(responseCode, errorMessage);
//                }
//            }catch(ConnectException ce){
//                throw new ExchangeClientException(500, "There was a connection exception, please ensure internet connectivity and exchange host settings", ce);
//            }catch (SocketTimeoutException te){
//                    throw new ExchangeClientException(500, "The connection to the exchange timed out and did not receive a response", te);
//            }catch (Exception e) {
//                throw new ExchangeClientException(500, e.tostring(), e);
//            }

            return null;
        }

        public bool DownloadReceiptPDF(string receiptId)
        {
            //try {
            //    HttpURLConnection connection = createConnection(_exchangeProtocol, _exchangeHostname, "/receipt/" + receiptId, CONTENT_TYPE_PDF, "GET", _receiptVersion);
            //    connection.connect();
            //    int responseCode = connection.getResponseCode();
            //    if (responseCode == HttpCodes.HTTP_200_OK) {
            //        InputStream pdfInputStream = connection.getInputStream();
            //        string saveFilePath = _downloadDirectory + File.separator + receiptId + ".pdf";

            //        // opens an output stream to save into file
            //        FileOutputStream pdfOutputStream = new FileOutputStream(saveFilePath);

            //        int bytesRead;
            //        byte[] buffer = new byte[BUFFER_SIZE];
            //        while ((bytesRead = pdfInputStream.read(buffer)) != -1) {
            //            pdfOutputStream.write(buffer, 0, bytesRead);
            //        }

            //        pdfOutputStream.close();
            //        pdfInputStream.close();

            //        return true;
            //    } else if (responseCode == HttpCodes.HTTP_404_NOTFOUND) {
            //        connection.disconnect();
            //        throw new ExchangeClientException(404, "The exchange host could not be found or is currently unavailable, please check ConfigManager setting and ensure they are correct.");
            //    } else if (responseCode == HttpCodes.HTTP_400_BAD_REQUEST || responseCode == HttpCodes.HTTP_401_UNAUTHORIZED) {
            //        loadErrorResponseJsonObject(connection);
            //        connection.disconnect();
            //        throw new ExchangeClientException(_responseErrorCode, _responseErrorMessage);
            //    } else {
            //        string errorMessage = connection.getResponseMessage();
            //        connection.disconnect();
            //        throw new ExchangeClientException(responseCode, errorMessage);
            //    }
            //}catch(ConnectException ce){
            //    throw new ExchangeClientException(500, "There was a connection exception, please ensure internet connectivity and exchange host settings", ce);
            //}catch (SocketTimeoutException te){
            //    throw new ExchangeClientException(500, "The connection to the exchange timed out and did not receive a response", te);
            //}catch (ExchangeClientException dRxE) {
            //    throw dRxE;
            //}catch (Exception e) {
            //    throw new ExchangeClientException(500, e.tostring(), e);
            //}


            return false;
        }
    
        public NewUserRegistrationExchangeResponse RegisterNewUser(NewUser newUser)
        {
            List<NewUser> _newUserCollection = new List<NewUser>();
            _newUserCollection.Add(newUser);
            return RegisterNewUser(_newUserCollection);
        }
    
        public NewUserRegistrationExchangeResponse RegisterNewUser(List<NewUser> newUsers)
        {
            Dictionary<string, NewUserRegistrationResult> _newUserRegistrationResponse = new Dictionary<string, NewUserRegistrationResult>();
            using (HttpClient client = CreateExchangeConnection("/user", _userVersion, null))
            {
                JsonSerializerSettings settings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    //DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                    DateFormatString = "yyyy-MM-ddTHH:mm:ss%K",
                    Formatting = Formatting.Indented,
                    NullValueHandling = NullValueHandling.Ignore,
                };
                settings.Converters.Add(new StringEnumConverter());
                string result = new NewUserRegistrationRequest() {Users = newUsers}.Serialize();
                    
                StringContent content = new StringContent(result, Encoding.UTF8, "application/json");
                var response = client.PostAsync("", content);
                var statusCode = response.Result.StatusCode;
                if (response.Result.StatusCode == HttpStatusCode.Created ||
                    response.Result.StatusCode == HttpStatusCode.BadRequest)
                {

                    string contentResult = response.Result.Content.ReadAsStringAsync().Result;
                    NewUserRegistrationExchangeResponse exchangeResponse = JsonConvert.DeserializeObject<NewUserRegistrationExchangeResponse>(contentResult,
                            _jsonSerializerSettings);
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
                    ExchangeResponseResult exchangeResponse =
                        JsonConvert.DeserializeObject<ExchangeResponseResult>(contentResult, _jsonSerializerSettings);
                    throw new ExchangeClientException(exchangeResponse.ExchangeResponse.Code.Value,
                        exchangeResponse.ExchangeResponse.ExceptionMessage);
                }
                //TODO: Not sure if errorMessage is correc here
                string errorMessage = response.Result.Content.ToString();
                throw new ExchangeClientException((int)statusCode, errorMessage);
            }
        }

        public Merchant LookupMerchant(string Id)
        {

            //try {
            //    string merchantLocationHostname = "https://cdn.dreceiptx.net/merchant/location/";
            //    if(!_environmentType.equals(EnvironmentType.PROD)){
            //        merchantLocationHostname = "https://cdn.dreceiptx.net/uat/merchant/location/";
            //    }
            //    URL merchantRequest = new URL(merchantLocationHostname + Id + "/info.json");
            //    HttpURLConnection connection = (HttpURLConnection) merchantRequest.openConnection();
            
            //    int responseCode = connection.getResponseCode();
            //    if (responseCode == HttpCodes.HTTP_200_OK) {
            //        BufferedReader merchantRequestConnectionReader = new BufferedReader(new InputStreamReader(
            //            connection.getInputStream()));
            //        StringBuilder response = new StringBuilder();
            //        string line;
            //        while ((line = merchantRequestConnectionReader.readLine()) != null) {
            //            response.append(line);
            //        }
            //        merchantRequestConnectionReader.close();
            //        Gson gson = new Gson();
            //        Merchant merchant = gson.fromJson(response.tostring(), Merchant.class);
            //        merchant.setMerchantLocationHostname(merchantLocationHostname);
            //        return merchant;
            //    }
            //    else{
            //        throw new ExchangeClientException(404, "Unknown merchant Id, please supply a valid ");
            //    }
            //}catch(ConnectException ce){
            //    throw new ExchangeClientException(500, "There was a connection exception, please ensure internet connectivity and exchange host settings", ce);
            //}catch (SocketTimeoutException te){
            //    throw new ExchangeClientException(500, "The connection to the exchange timed out and did not receive a response", te);
            //}catch (ExchangeClientException dRxE) {
            //    throw dRxE;
            //}catch (Exception e) {
            //    throw new ExchangeClientException(500, e.tostring(), e);
            //}

            return null;
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
        //    private string CreateAuthKey(string timestamp) throws NoSuchAlgorithmException, InvalidKeyException, UnsupportedEncodingException {
        //    string key = _configManager.getConfigValue("api.key") + ":" + string.valueOf(timestamp) + ":" + _configManager.getConfigValue("api.requesterId");
        //    Mac sha256_HMAC = Mac.getInstance("HmacSHA256");
        //    SecretKeySpec secret_key = new SecretKeySpec(_configManager.getConfigValue("api.secret").getBytes(), "HmacSHA256");
        //    sha256_HMAC.init(secret_key);
        //    string hash = Base64.encodeBase64string(sha256_HMAC.doreadonly(key.getBytes()));
        //    System.out.println(hash);
        //    return "DRX " + hash;
        //}

        //private HttpURLConnection CreateConnection(string protocol, string hostname, string uri, string contentType, string requestMethod,
        //                                           string requestVersion, UriParameters parameters) throws IOException, InvalidKeyException, NoSuchAlgorithmException {
        //    if(parameters != null) {
        //        StringBuilder parameterstring = new StringBuilder();
        //        bool firstIteration = true;
        //        for (Dictionary.Entry<string, string> param : parameters.getEntrySet()) {
        //            if(firstIteration){
        //                parameterstring.append("?");
        //                firstIteration = false;
        //            }else{
        //                parameterstring.append("&");
        //            }
        //            parameterstring.append(param.getKey()).append("=").append(param.getValue());
        //        }
        //        uri = uri+parameterstring.tostring();
        //    }
        //    URL url = new URL(protocol + "://" + hostname + uri);
        //    HttpURLConnection connection = (HttpURLConnection) url.openConnection();
        //    connection.setRequestMethod(requestMethod);
        //    connection.setDoOutput(true);
        //    connection.setDoInput(true);
        //    if(contentType != null) {
        //        connection.setRequestProperty("Content-Type", contentType);
        //    }
        //    string timestamp = string.valueOf(System.currentTimeMillis());
        //    connection.setRequestProperty("Authorization", createAuthKey(timestamp));
        //    connection.setRequestProperty("x-drx-timestamp", timestamp);
        //    connection.setRequestProperty("x-drx-requester", _requesterId);
        //    connection.setRequestProperty("x-drx-version", requestVersion);
        //    connection.setRequestProperty("User-Agent", USER_AGENT);
        //    connection.setConnectTimeout(5000);
        //    connection.setReadTimeout(300000);
        //    return connection;
        //}


        //private HttpURLConnection createConnection(string protocol, string hostname, string uri, string contentType, string requestMethod,
        //                                           string requestVersion) throws IOException, InvalidKeyException, NoSuchAlgorithmException {
        //    return this.createConnection(protocol, hostname, uri, contentType, requestMethod, requestVersion, null);
        //}

        //private string getResponsestring(HttpURLConnection connection) throws IOException, ExchangeClientException {
        //    try (BufferedReader br = new BufferedReader(new InputStreamReader(connection.getInputStream()))) {
        //        StringBuilder sb = new StringBuilder();
        //        string line;
        //        while ((line = br.readLine()) != null) {
        //            sb.append(line).append("\n");
        //        }
        //        return sb.tostring();
        //    } catch (Exception e) {
        //        throw new ExchangeClientException(500, e.tostring(), e);
        //    }
        //}
        //private JsonObject getResponseJsonObject(HttpURLConnection connection) throws IOException, ExchangeClientException {
        //    JsonParser parser = new JsonParser();
        //    JsonObject jsonResponse = parser.parse(getResponsestring(connection)).getAsJsonObject();
        //    JsonElement element = jsonResponse.get("exchangeResponse");
        //    return element.getAsJsonObject();
        //}

        //private void loadErrorResponseJsonObject(HttpURLConnection connection) throws ExchangeClientException {
        //    try (BufferedReader br = new BufferedReader(new InputStreamReader(connection.getErrorStream()))) {
        //        StringBuilder sb = new StringBuilder();
        //        string line;
        //        while ((line = br.readLine()) != null) {
        //            sb.append(line).append("\n");
        //        }
        //        JsonParser parser = new JsonParser();
        //        JsonObject jsonResponse = parser.parse(sb.tostring()).getAsJsonObject();
        //        JsonObject exchangeResponse = jsonResponse.get("exchangeResponse").getAsJsonObject();
        //        _responseErrorCode = exchangeResponse.get("code").getAsInt();
        //        _responseErrorMessage = exchangeResponse.get("exceptionMessage").getAsstring();
        //    } catch (Exception e) {
        //        throw new ExchangeClientException(500, e.tostring(), e);
        //    }
        //}
    
        private string ValidateConfigOption(string configParameter)  {
            if(_configManager.Exists(configParameter)){
                return _configManager.GetConfigValue(configParameter);
            }else{
                throw new ExchangeClientException(101, "Required config parameter "+configParameter+" not supplied");
            }
        }

        private enum EnvironmentType
        {
            [DrxEnumExtendedInformation("DEV", "Development or System Test")]
            DEV,
            [DrxEnumExtendedInformation("UAT", "User acceptance testing or model office")]
            UAT,
            [DrxEnumExtendedInformation("PROD", "Production/Live environment")]
            PROD
        }
    }
}