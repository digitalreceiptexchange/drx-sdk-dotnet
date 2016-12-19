using Net.Dreceiptx.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NUnit.Framework;

namespace Net.Dreceiptx.UnitTests.Client
{
    [TestFixture]
    public class NewUserRegistrationExchangeResponseTest
    {

        [Test]
        public void TestDeseiralization()
        {
            JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateTimeZoneHandling = DateTimeZoneHandling.Utc
            };
            string message = "{\n" +
                             "   \"exchangeResponse\":{\n" +
                             "      \"code\":201,\n" +
                             "      \"success\":true,\n" +
                             "      \"responseData\":{\n" +
                             "         \"usersRegistered\":1,\n" +
                             "         \"users\":{\n" +
                             "            \"dominic.catherin@digitalreceiptexchange.com\":{\n" +
                             "               \"success\":true,\n" +
                             "               \"code\":201,\n" +
                             "               \"email\":\"dominic.catherin@digitalreceiptexchange.com\",\n" +
                             "               \"guid\":\"UATANZALPHAUSR14805926335450470\"\n" +
                             "            }\n" +
                             "         }\n" +
                             "      }\n" +
                             "   }\n" +
                             "}";
            NewUserRegistrationExchangeResponse exchangeResponse = JsonConvert.DeserializeObject<NewUserRegistrationExchangeResponse>(message, _jsonSerializerSettings);
            Assert.AreEqual(201, exchangeResponse.ExchangeResponse.Code);
            Assert.AreEqual(true, exchangeResponse.ExchangeResponse.Success);
            Assert.AreEqual(1, exchangeResponse.ExchangeResponse.ResponseData.UsersRegistered);
            var user =
                exchangeResponse.ExchangeResponse.ResponseData.Users["dominic.catherin@digitalreceiptexchange.com"];
            Assert.IsNotNull(user);
            Assert.AreEqual("dominic.catherin@digitalreceiptexchange.com", user.Email);
            Assert.AreEqual(true, user.Success);
            Assert.AreEqual(201, user.Code);
            Assert.AreEqual("UATANZALPHAUSR14805926335450470", user.Guid);
        }
    }
}


