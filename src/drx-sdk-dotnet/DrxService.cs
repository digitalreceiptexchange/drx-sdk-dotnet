using System;
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

    public class DrxService
    {
        public async Task Submit(string apiKey, string secret, string requesterId, DrxDigitalReceipt receipt)
        {
            
        }

        public ExchangeResponse Submit(DrxCredentials authentication, DrxDigitalReceipt receipt)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api-alpha-anz.dreceiptx.net/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                long timestamp = (long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds;
                client.DefaultRequestHeaders.Add("dRx-RequestTimeStamp", timestamp.ToString());
                client.DefaultRequestHeaders.Add("dRx-Version", "1.0.0");
                client.DefaultRequestHeaders.Add("dRx-Requester", authentication.RequesterId);
                string auth = "DRX " + CreateAuthKey(timestamp, authentication);
                client.DefaultRequestHeaders.Add("Authorization", auth);
                HttpRequestMessage message = new HttpRequestMessage();

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
                    
                    return exchangeResponse.ExchangeResponse;
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine(e);
                    throw;
                }

            }
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
    }

    public class ExchangeResponseResult
    {
        public ExchangeResponse ExchangeResponse { get; set; }
    }

    public class ExchangeResponse
    {
        public bool Success { get; set; }
        public string Code { get; set; }
        public ResponseData ResponseData { get; set; }
        public string ExceptionMessage { get; set; }
    }

    public class ResponseData
    {
        public string ReceiptId { get; set; }
        public string Reference { get; set; }
    }
}
