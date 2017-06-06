using System.Collections.Generic;
using Net.Dreceiptx.Users;
using NUnit.Framework;

namespace Net.Dreceiptx.UnitTests.Users
{
    [TestFixture]
    public class NewUserRegistrationRequestTest
    {
        [Test]
        public void TestSerialization()
        {
            NewUserRegistrationRequest request = new NewUserRegistrationRequest();
            request.Users = new List<NewUser>();
            var user1 = new NewUser("test@gmail.com");
            user1.Identifiers.Add(new Identifier{Type = UserIdentifierType.Email});
            user1.Config.Add(new UserConfigurationOption(UserConfigOptionType.EndPointId, "SCA"));
            request.Users.Add(user1);

            string expected = @"{
  ""users"": [
    {
      ""userEmail"": ""test@gmail.com"",
      ""identifiers"": [
        {
          ""type"": ""EMAIL"",
          ""value"": ""test@gmail.com""
        },
        {
          ""type"": ""EMAIL""
        }
      ],
      ""config"": [
        {
          ""option"": ""ENDPOINTID"",
          ""value"": ""SCA""
        }
      ]
    }
  ],
  ""usersToRegister"": 1
}";
            Assert.AreEqual(expected, request.SerializeToJsonString());
        }
    }
}
