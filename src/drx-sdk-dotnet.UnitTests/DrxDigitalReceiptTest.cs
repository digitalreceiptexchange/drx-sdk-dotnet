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
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Net.Dreceiptx.GS1.SDBH;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NUnit.Framework;

namespace Net.Dreceiptx.UnitTests
{
    [TestFixture]
    public class DrxDigitalReceiptTest
    {
        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void TestEndToEndTest()
        {
            DrxDigitalReceipt receipt = new DrxDigitalReceipt();
            Partner partner = new Partner();
            partner.Identifier.Authority = "GS1";
            partner.Identifier.Value = "anz_merchant_862";
            receipt.StandardBusinessDocumentHeader.Sender.Add(partner);
            //receipt.StandardBusinessDocumentHeader.Sender.ContactInformation.Add(new ContactInformation
            //{
            //    RepId = "1",
            //    RepName = "Dominic Catherin1",
            //    EmailAddress = "test@gmail.com",
            //    FaxNumber = "+61 2 12345678",
            //    TelephoneNumber = "+61 2 987654321",
            //    ContactTypeIdentifier = "Sales",
            //    Contact = "Dom Catherin"

            //});
            Partner partner1 = new Partner();
            partner1.Identifier.Authority = "GS1";
            partner1.Identifier.Value = "9377778071234";
            //partner1.ContactInformation.Add(new ContactInformation
            //{
            //    RepId = "2",
            //    RepName = "Dominic Catherin2",
            //    EmailAddress = "test@gmail.com",
            //    FaxNumber = "+61 2 12345678",
            //    TelephoneNumber = "+61 2 987654321",
            //    ContactTypeIdentifier = "Sales",
            //    Contact = "Dom Catherin"

            //});
            //partner1.ContactInformation.Add(new ContactInformation
            //{
            //    RepId = "3",
            //    RepName = "Dominic Catherin3",
            //    EmailAddress = "test@gmail.com",
            //    FaxNumber = "+61 2 12345678",
            //    TelephoneNumber = "+61 2 987654321",
            //    ContactTypeIdentifier = "Sales",
            //    Contact = "Dom Catherin"

            //});

            Partner partner2 = new Partner();
            partner2.Identifier.Authority = "dRx";
            partner2.Identifier.Value = "ALPHAANZTEST00000000000000219";
            //partner2.ContactInformation = null;
            //partner1.ContactInformation = null;
            //partner2.ContactInformation.Add(new ContactInformation
            //{
            //    RepId = "4",
            //    RepName = "Dominic Catherin4",
            //    EmailAddress = "test@gmail.com",
            //    FaxNumber = "+61 2 12345678",
            //    TelephoneNumber = "+61 2 987654321",
            //    ContactTypeIdentifier = "Sales",
            //    Contact = "Dom Catherin"

            //});
            //partner2.ContactInformation.Add(new ContactInformation
            //{
            //    RepId = "5",
            //    RepName = "Dominic Catherin5",
            //    EmailAddress = "test@gmail.com",
            //    FaxNumber = "+61 2 12345678",
            //    TelephoneNumber = "+61 2 987654321",
            //    ContactTypeIdentifier = "Sales",
            //    Contact = "Dom Catherin"

            //});
            receipt.StandardBusinessDocumentHeader.Receiver.Add(partner1);
            receipt.StandardBusinessDocumentHeader.Receiver.Add(partner2);
            receipt.StandardBusinessDocumentHeader.DocumentIdentification.Standard = "GS1";
            receipt.StandardBusinessDocumentHeader.DocumentIdentification.TypeVersion = "1.0.0";
            receipt.StandardBusinessDocumentHeader.DocumentIdentification.Type = "CONSUMER";
            receipt.StandardBusinessDocumentHeader.DocumentIdentification.InstanceIdentifier = "123456789";
            receipt.StandardBusinessDocumentHeader.DocumentIdentification.MultipleType = "true";
            receipt.StandardBusinessDocumentHeader.DocumentIdentification.CreationDateAndTime = DateTime.Now;


            receipt.Invoice = new DigitalReceipt();
            receipt.Invoice.DocumentStatusCode = "ORIGINAL";
            receipt.Invoice.InvoiceIdentification.EntityIdentification = "DOM1234";
            //receipt.Invoice.Delivery = new Delivery(DateTime.Now.AddDays(10), "Leave out the front!");
            //receipt.Invoice.Delivery.DeliveryAddress = new Address("Dominic Catherin",
            //    "2/39 Lodge Street", "Balgowlah", "Manly", "Sydney", "2093", "NSW", "AUS");
            //receipt.Invoice.AddDeliveryFee(1, "Delivery out of state", new TaxFee(2));
            //receipt.Invoice.Delivery.AddContact(new Contact("ChannelCode1", "ChannelCodevalue"));
            //receipt.Invoice.Delivery.AddDeliveryFee(2, "Delivery out of country", new TaxFee(3));
            //receipt.Invoice.Code = "InvoiceCode";
            //receipt.Invoice.PrepaidAmount = 10;
            receipt.Invoice.AddTip(1, "Thanks for discount", new TaxFee(0));
            //receipt.Invoice.ReceiptType = "DIGITALRECEIPT";
            receipt.Invoice.InvoiceType = "TAX_INVOICE"; // will always be this.
            receipt.Invoice.CountryOfSupplyOfGoods = "AUS"; //TODO: new
            receipt.Invoice.InvoiceCurrencyCode = "AUD"; // read from config?
            //receipt.Invoice.UserGUID = "d75f5c13-3603-4ab7-9bc0-597981ef7da4";
            //receipt.Invoice.MerchantReferenceNumber = "1234";
            //receipt.Invoice.TransactionDateTime = DateTime.Now;
            //receipt.Invoice.Representative = "RepresentativeValue";

            LineItem lineItem = new LineItem(1, 50, 50);
            lineItem.TransactionalTradeItem = new LineItemTradeInformation("Chromecase", "Google Chromecast", false, false, "Google");
            lineItem.GTIN = "GTIN";
            lineItem.ISBN = "ISBN";
            lineItem.AddGeneralDiscount(5, "End of year sale", new TaxFee(1));
            receipt.Invoice.AddLineItem(lineItem);

            

            var validationResult = receipt.Invoice.Validate();
            Console.WriteLine("Valid?: {0}", validationResult.IsValid);
            validationResult.Errors.ForEach(Console.WriteLine);

            //receipt.Invoice.Delivery.DeliveryAddress.Name("Dominic Catherin")
            //    .StreetAddress1("2/39 Lodge Street")
            //    .StreetAddress2("Balgowlah")
            //    .City("Sydney")
            //    .PostalCode("2093")
            //    .State("NSW")
            //    .CountryCode("AUS");


            string result = JsonConvert.SerializeObject(receipt, Formatting.Indented, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateTimeZoneHandling = DateTimeZoneHandling.Utc
            });
            //string result = new { dRxDigitalReceipt = receipt}.SerializeToJson();}
            Console.WriteLine(result);
            DrxDigitalReceipt result2 = JsonConvert.DeserializeObject<DrxDigitalReceipt>(result);



            ////result = receipt.SerializeToXml();
            ////Console.WriteLine(result);


            DrxService service = new DrxService();
            DrxCredentials credentials = new DrxCredentials();
            credentials.ApiKey = "7FRLDrWCQH49AeG60iPZ";
            credentials.RequesterId = "ALPHA_ANZ_TEST_REQUESTER";
            credentials.Secret = "UVk3LJINT1DyDHgdWU1LJGfl9ASyEflU20BXl26trR";
            var response = service.Submit(credentials, receipt);
            Assert.True(response.Success);
                

            //string xxx = CreateAuthKey(1);
            //Submit(receipt).Wait();

        }

        public string CreateAuthKey(double timestamp)
        {
            byte [] salt = System.Text.Encoding.UTF8.GetBytes("UVk3LJINT1DyDHgdWU1LJGfl9ASyEflU20BXl26trR");
            string key = string.Format("{0}:{1}:{2}", "7FRLDrWCQH49AeG60iPZ", timestamp, "ALPHA_ANZ_TEST_REQUESTER");
            //string key = "7FRLDrWCQH49AeG60iPZ:1453291791394:ALPHA_ANZ_TEST_REQUESTER";

            SHA256 mySHA256 = SHA256Managed.Create();
            byte[] hasValue = mySHA256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(key));
            string result1 = string.Empty;
            using (var mac = new HMACSHA256(salt))
            {
                byte[] hashmessage = mac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(key));

                return Convert.ToBase64String(hashmessage);

                for (int i = 0; i < hasValue.Length; i++)
                {
                    result1 += string.Format("{0:X2}", hasValue[i]);
                }
            }
            //    string result1 = string.Empty;
            //for (int i = 0; i < hasValue.Length; i++)
            //{
            //    result1 += string.Format("{0:X2}", hasValue[i]);
            //}
            return result1;
        }

        public async Task Submit(DrxDigitalReceipt receipt)
        {
            using (var client = new HttpClient())
            {
                // New code:
                //client.BaseAddress = new Uri("http://local.api.dreceiptx.net:8080/");
                client.BaseAddress = new Uri("https://api-alpha-anz.dreceiptx.net/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                long timestamp  = (long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds;
                client.DefaultRequestHeaders.Add("dRx-RequestTimeStamp", timestamp.ToString());
                client.DefaultRequestHeaders.Add("dRx-Version", "1.0.0");
                client.DefaultRequestHeaders.Add("dRx-Requester", "ALPHA_ANZ_TEST_REQUESTER");
                string auth = "DRX " + CreateAuthKey(timestamp);
                Console.WriteLine(auth);
                client.DefaultRequestHeaders.Add("Authorization", auth);
                //client.DefaultRequestHeaders.Add("Content-Type", "application/json");
                HttpRequestMessage message = new HttpRequestMessage();
                
                message.Content = new StringContent( JsonConvert.SerializeObject(receipt, Formatting.Indented, new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    DateTimeZoneHandling = DateTimeZoneHandling.Utc
                }), Encoding.UTF8, "application/json");


                try
                {
                    MediaTypeFormatter formatter = new JsonMediaTypeFormatter();
                    JsonMediaTypeFormatter jsonFormatter = new JsonMediaTypeFormatter();
                    jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    var response = await client.PostAsync("receipt?XDEBUG_SESSION_START=netbeans-xdebug", new { dRxDigitalReceipt = receipt }, jsonFormatter);
                    //var response = await client.PostAsync("receipt?XDEBUG_SESSION_START=netbeans-xdebug", message.Content);
                    //var response = await client.PostAsync("receipt?XDEBUG_SESSION_START=netbeans-xdebug", new { dRxDigitalReceipt = receipt }, jsonFormatter);

                    response.EnsureSuccessStatusCode();

                    
                    //if (response.IsSuccessStatusCode)
                    //{
                    //    // Get the URI of the created resource.
                    //    Uri gizmoUrl = response.Headers.Location;
                    //}
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine(e);
                }

            }
        }

        [Test]
        public void TestDeserializeFromJavaGeneratedJason()
        {
            DrxDigitalReceipt result2 = JsonConvert.DeserializeObject<DrxDigitalReceipt>(Resource1.JavaJson);
            
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void PhpGenerate()
        {


            PrintProperties<StandardBusinessDocumentHeader>();
            PrintProperties<Partner>();
            PrintProperties<DocumentIdentification>();
            PrintProperties<DigitalReceipt>();
        }

        private static void PrintProperties<T>()
        {
            Console.WriteLine("{0}", typeof(T).Name);
            foreach (PropertyInfo property in typeof (T).GetProperties())
            {
                var attribute = property.GetCustomAttribute<DataMemberAttribute>();
                if (attribute != null)
                {
                    Console.WriteLine("var ${0}{1};", property.Name[0].ToString().ToLower(), property.Name.Substring(1));
                }
            }
        }

        [Test]
        public void TestResponse()
        {
            string result = 
"{ \"exchangeResponse\":" +
"{ \"success\":true," +
"\"code\":201," +
"\"responseData\":{ \"receiptId\":\"ALPHAANZTESTDR0000000000000000000000394\",\"reference\":\"123456789\"}," +
"\"exceptionMessage\":null}}";
            ExchangeResponseResult exchangeResponse = JsonConvert.DeserializeObject<ExchangeResponseResult>(result, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,

            });

            ExchangeResponseResult dom = new ExchangeResponseResult();
            dom.ExchangeResponse = new ExchangeResponse();
            dom.ExchangeResponse.Success = true;
            dom.ExchangeResponse.Code = "Code 1";
            dom.ExchangeResponse.ResponseData = null;
            string xxx = JsonConvert.SerializeObject(dom, Formatting.Indented, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,

            });
            Console.WriteLine(xxx);

            Assert.IsTrue(exchangeResponse.ExchangeResponse.Success);
        }
    }
}
