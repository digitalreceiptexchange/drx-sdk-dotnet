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
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Net.Dreceiptx.Client.Exceptions;
using Net.Dreceiptx.Receipt;
using Net.Dreceiptx.Receipt.Config;
using Net.Dreceiptx.Receipt.Merchant;
using Net.Dreceiptx.Users;
using Newtonsoft.Json;
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
        }

        public User SearchUser(UserIdentifierType identifierType, string identifier)
        {
            string encodedIdentifier = HttpUtility.UrlEncode(identifier);
            UriParameters uriParameters = new UriParameters();
            uriParameters.Add("type", identifierType.Value());
            return SearchUser(encodedIdentifier, uriParameters);
        }

        private HttpClient CreateConnection(string protocol, string hostname, string uri, string contentType, string requestMethod,
            string requestVersion)
        {
            using (var client = new HttpClient())
            {
                string url = null;
                client.BaseAddress = new Uri($"{protocol}://{hostname}{url}");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                long timestamp = (long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds;
                client.DefaultRequestHeaders.Add("x-drx-timestamp", timestamp.ToString());
                client.DefaultRequestHeaders.Add("x-drx-requester", _requesterId);
                client.DefaultRequestHeaders.Add("x-drx-version", requestVersion);
                client.DefaultRequestHeaders.Add("User-Agent", USER_AGENT);
                //string auth = "DRX " + CreateAuthKey(timestamp, authentication);
                string auth = null;
                client.DefaultRequestHeaders.Add("Authorization", auth);
                HttpRequestMessage message = new HttpRequestMessage();
                Receipt.DigitalReceipt receipt = null;
                message.Content = new StringContent(JsonConvert.SerializeObject(receipt, Formatting.Indented, new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    DateTimeZoneHandling = DateTimeZoneHandling.Utc
                }), Encoding.UTF8, "application/json");

                Console.WriteLine(message.Content);


                try
                {
                    JsonMediaTypeFormatter jsonFormatter = new JsonMediaTypeFormatter();
                    jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    var response = client.PostAsync("receipt?XDEBUG_SESSION_START=netbeans-xdebug", new { dRxDigitalReceipt = receipt }, jsonFormatter).Result;

                    string contentResult = response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine($"StatusCode={response.StatusCode}");
                    Console.WriteLine($"ResponseData={contentResult}");
                    ExchangeResponseResult exchangeResponse = JsonConvert.DeserializeObject<ExchangeResponseResult>(contentResult, new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver(),
                        DateTimeZoneHandling = DateTimeZoneHandling.Utc
                    });
                    if (exchangeResponse.ExchangeResponse.Success)
                    {
                        Console.WriteLine($"Success!, RecieptId={exchangeResponse.ExchangeResponse.ResponseData.ReceiptId}, Reference={exchangeResponse.ExchangeResponse.ResponseData.Reference}");
                    }
                    else
                    {
                        Console.WriteLine($"FAILURE,{exchangeResponse.ExchangeResponse.ExceptionMessage}");
                    }

                    //return exchangeResponse.ExchangeResponse;
                    return client;
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine(e);
                    throw;
                }

            }
        }
        private User SearchUser(string encodedIdentifier, UriParameters uriParameters) {
            try {
                
                //HttpClient client = CreateConnection(_directoryProtocol, _directoryHostname,
                //        "/user/"+encodedIdentifier, CONTENT_TYPE_JSON, "GET", _userVersion, uriParameters);
                //connection.connect();
                //int responseCode = connection.getResponseCode();
                //if (responseCode == HttpCodes.HTTP_200_OK || responseCode == HttpCodes.HTTP_400_BAD_REQUEST) {
                //    JsonObject exchangeResponse = getResponseJsonObject(connection);
                //    if (exchangeResponse.get("success").getAsbool()) {
                //        User user = new User();
                //        JsonObject responseData = exchangeResponse.get("responseData").getAsJsonObject();
                //        user.setGUID(responseData.get("guid").getAsstring());
                //        user.setRMS(responseData.get("rms").getAsstring());
                //        connection.disconnect();
                //        return user;
                //    } else {
                //        connection.disconnect();
                //        throw new ExchangeClientException(exchangeResponse.get("code").getAsInt(), exchangeResponse.get("exceptionMessage").getAsstring());
                //    }
                //} else if (responseCode == HttpCodes.HTTP_404_NOTFOUND) {
                //    connection.disconnect();
                //    throw new ExchangeClientException(404, "The exchange host could not be found or is currently unavailable, please check ConfigManager setting and ensure they are correct.");
                //}else if (responseCode == HttpCodes.HTTP_401_UNAUTHORIZED) {
                //    loadErrorResponseJsonObject(connection);
                //    connection.disconnect();
                //    throw new ExchangeClientException(_responseErrorCode, _responseErrorMessage);
                //} else {
                //    string errorMessage = connection.getResponseMessage();
                //    connection.disconnect();
                //    throw new ExchangeClientException(responseCode, errorMessage);
                //}

                return null;
            }
            //catch (SocketTimeoutException te){
            //    throw new ExchangeClientException(500, "The connection to the exchange timed out and did not receive a response", te);
            //}
            catch (ExchangeClientException dRxE) {
                throw dRxE;
            }catch (Exception e) {
                throw new ExchangeClientException(500, e.ToString(), e);
            }
        }

        public Users.Users SearchUsers(UserIdentifierType identifierType, List<string> userIdentifiers)  {
            //UriParameters uriParameters = new UriParameters();
            //uriParameters.Add("type", identifierType.Value().ToLower());
            //StringBuilder userIdentifiersParam = new StringBuilder();
            //bool firstIteration = true;
            //foreach (string userIdentifier in userIdentifiers) {
            //    if(!firstIteration){
            //        userIdentifiersParam.Append(";");
            //    }
            //    userIdentifiersParam.Append(userIdentifier);
            //    firstIteration = false;
            //}
            //uriParameters.Add("identifiers", userIdentifiersParam.ToString());
            //Users.Users users = new Users();
            //try {
            //    HttpURLConnection connection = createConnection(_directoryProtocol, _directoryHostname,
            //            "/user", CONTENT_TYPE_JSON, "GET", _userVersion, uriParameters);
            //    connection.connect();
            //    int responseCode = connection.getResponseCode();
            //    if (responseCode == HttpCodes.HTTP_200_OK) {
            //        JsonObject exchangeResponse = getResponseJsonObject(connection);

            //        if (exchangeResponse.get("success").getAsbool()) {
            //            JsonObject responseData = exchangeResponse.get("responseData").getAsJsonObject();
            //            JsonObject userIdentifiersObject = responseData.get("userIdentifiers").getAsJsonObject();
            //            for (Dictionary.Entry<string, JsonElement> entry : userIdentifiersObject.entrySet()) {
            //                User user = null;
            //                if (!entry.getValue().isJsonNull()) {
            //                    JsonObject userIdentifierObject = userIdentifiersObject.getAsJsonObject(entry.getKey());
            //                    user = new User();
            //                    user.setIdentifierType(identifierType);
            //                    user.setIdentifier(entry.getKey());
            //                    user.setGUID(userIdentifierObject.get("guid").getAsstring());
            //                    user.setRMS(userIdentifierObject.get("rms").getAsstring());
            //                }
            //                users.add(entry.getKey(), user);
            //            }
            //            connection.disconnect();
            //            return users;
            //        } else {
            //            throw new ExchangeClientException(exchangeResponse.get("code").getAsInt(), exchangeResponse.get("exceptionMessage").getAsstring());
            //        }
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

            return null;
        }

        public string SendReceipt(DigitalReceiptGenerator receipt)
        {
            //try {
            //    HttpURLConnection connection = createConnection(_exchangeProtocol, _exchangeHostname, "/receipt?XDEBUG_SESSION_START=drx-xdebug", CONTENT_TYPE_JSON,
            //            "POST", _receiptVersion);
            //    OutputStream os = connection.getOutputStream();
            //    os.write(receipt.encodeJson().getBytes());
            //    os.flush();
            //    connection.connect();
            //    int responseCode = connection.getResponseCode();
            //    if (responseCode == HttpCodes.HTTP_201_CREATED) {
            //        JsonObject exchangeResponse = getResponseJsonObject(connection);
            //        if (exchangeResponse.get("success").getAsbool()) {
            //            JsonObject responseData = exchangeResponse.get("responseData").getAsJsonObject();
            //            return responseData.get("receiptId").getAsstring();
            //        } else {
            //            throw new ExchangeClientException(exchangeResponse.get("code").getAsInt(), exchangeResponse.get("exceptionMessage").getAsstring());
            //        }

            //    } else if (responseCode == HttpCodes.HTTP_400_BAD_REQUEST || responseCode == HttpCodes.HTTP_401_UNAUTHORIZED) {
            //        loadErrorResponseJsonObject(connection);
            //        connection.disconnect();
            //        throw new ExchangeClientException(_responseErrorCode, _responseErrorMessage);
            //    } else if (responseCode == HttpCodes.HTTP_404_NOTFOUND) {
            //        connection.disconnect();
            //        throw new ExchangeClientException(404, "The exchange host could not be found or is currently unavailable, please check ConfigManager setting and ensure they are correct.");
            //    }else {
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
            return null;
        }

        public DigitalReceipt LookupReceipt(string receiptId)
        {
            //try {
            //    HttpURLConnection connection = createConnection(_exchangeProtocol, _exchangeHostname, "/receipt/" + receiptId, CONTENT_TYPE_JSON, "GET", _receiptVersion);
            //    connection.connect();
            //    int responseCode = connection.getResponseCode();
            //    if (responseCode == HttpCodes.HTTP_200_OK) {
            //        string response = getResponsestring(connection);
            //        connection.disconnect();
            //        return new DigitalReceipt(response);
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
            //        throw new ExchangeClientException(500, "The connection to the exchange timed out and did not receive a response", te);
            //}catch (Exception e) {
            //    throw new ExchangeClientException(500, e.tostring(), e);
            //}

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
    
        public NewUserRegistrationResult RegisterNewUser(NewUser newUser)
        {
            List<NewUser> _newUserCollection = new List<NewUser>();
            _newUserCollection.Add(newUser);
            //Dictionary<string, NewUserRegistrationResult> _newUserRegistrationResponse = this.registerNewUser(_newUserCollection);
            //return _newUserRegistrationResponse.get(newUser.getEmail());

            return null;
        }
    
        public Dictionary<string, NewUserRegistrationResult> RegisterNewUser(List<NewUser> newUsers)
        {
            //Dictionary<string, NewUserRegistrationResult> _newUserRegistrationResponse = new Dictionary<>();
            //try {
            //    Gson gson = new GsonBuilder()
            //        .setDateFormat("yyyy-MM-dd'T'HH:mm:ssZ")
            //        .registerTypeAdapter(newUsers.getClass(), new NewUsersSerializer())
            //        .create();
            //    string newUserRegistrationJson = gson.toJson(newUsers);

            //    HttpURLConnection connection = createConnection(_exchangeProtocol, _exchangeHostname, "/user?XDEBUG_SESSION_START=drx-xdebug", CONTENT_TYPE_JSON, "POST", _userVersion);
            //    OutputStream os = connection.getOutputStream();
            //    os.write(newUserRegistrationJson.getBytes());
            //    os.flush();
            //    connection.connect();
            //    int responseCode = connection.getResponseCode();
            //    if (responseCode == HttpCodes.HTTP_201_CREATED || responseCode == HttpCodes.HTTP_400_BAD_REQUEST) {
            //        JsonObject exchangeResponse = getResponseJsonObject(connection);
            //        if (exchangeResponse.get("success").getAsbool()) {
            //            JsonObject responseData = exchangeResponse.get("responseData").getAsJsonObject();
            //            JsonObject usersObject = responseData.get("users").getAsJsonObject();
            //            for (Dictionary.Entry<string, JsonElement> entry : usersObject.entrySet()) {
            //                NewUserRegistrationResult newUserRegistrationResult = new NewUserRegistrationResult();
            //                if(!entry.getValue().isJsonNull()){
            //                    JsonObject userRegistrationObject = usersObject.getAsJsonObject(entry.getKey());
            //                    if(userRegistrationObject.get("success").getAsbool()){
            //                        newUserRegistrationResult.setUserGUID(userRegistrationObject.get("guid").getAsstring());
            //                    }else{
            //                        newUserRegistrationResult.setException(userRegistrationObject.get("code").getAsInt(),userRegistrationObject.get("exception").getAsstring());
            //                    }
            //                }
            //                _newUserRegistrationResponse.put(entry.getKey(), newUserRegistrationResult);
            //            }
            //        } else {
            //            throw new ExchangeClientException(exchangeResponse.get("code").getAsInt(), exchangeResponse.get("exceptionMessage").getAsstring());
            //        }
            //    } else if (responseCode == HttpCodes.HTTP_401_UNAUTHORIZED) {
            //        loadErrorResponseJsonObject(connection);
            //        connection.disconnect();
            //        throw new ExchangeClientException(_responseErrorCode, _responseErrorMessage);
            //    } else if (responseCode == HttpCodes.HTTP_404_NOTFOUND) {
            //        connection.disconnect();
            //        throw new ExchangeClientException(_responseErrorCode, "Exchange could not be found, ensure internet connection or valid URL");
            //    }else {
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
        
            //return _newUserRegistrationResponse;

            return null;
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


        public string CreateAuthKey(double timestamp, DrxCredentials credentials)
        {
            string key = $"{credentials.ApiKey}:{timestamp}:{credentials.RequesterId}";

            using (var mac = new HMACSHA256(Encoding.UTF8.GetBytes(credentials.Secret)))
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

        private static class HttpCodes {
            private static int HTTP_200_OK = 200;
            private static int HTTP_201_CREATED = 201;
            private static int HTTP_400_BAD_REQUEST = 400;
            private static int HTTP_401_UNAUTHORIZED = 401;
            private static int HTTP_404_NOTFOUND = 404;
            private static int HTTP_501_INTERNAL_SERVER_ERROR = 501;
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