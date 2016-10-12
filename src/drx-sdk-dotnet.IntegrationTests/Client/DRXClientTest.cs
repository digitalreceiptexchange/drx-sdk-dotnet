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
using System.Collections.Generic;
using Net.Dreceiptx.Client;
using Net.Dreceiptx.Receipt.Config;
using Net.Dreceiptx.Users;
using NUnit.Framework;

namespace Net.Dreceiptx.IntegrationTests.Client
{
    [TestFixture]
    public class DRXClientTest
    {
        private IConfigManager _configManager;
        [SetUp]
        public void Setup()
        {
            _configManager = new DictionaryConfigManager();
            _configManager.SetConfigValue("exchange.hostname", "aus-beta-api.dreceiptx.net");
            _configManager.SetConfigValue("directory.hostname", "aus-beta-directory.dreceiptx.net");
            _configManager.SetConfigValue("api.requesterId", "UAT_AUS_CONCIERGE_TRAVEL_GROUP-1");
            _configManager.SetConfigValue("receipt.version", "1.3.0");
            _configManager.SetConfigValue("user.version", "1.1.0");
            _configManager.SetConfigValue("download.directory", "");
            _configManager.SetConfigValue("exchange.protocol", "https");
            _configManager.SetConfigValue("directory.protocol", "https");
            _configManager.SetConfigValue("environment.type", "UAT");
            _configManager.SetConfigValue("api.secret", "HPmlotenxFgnT19hXHAkAsmeKxDTiU2lmrGv1tkqta");
            _configManager.SetConfigValue("api.key", "otuwmVfSFZhfpSy6gcCh");
        }

        [Test]
        public void TestSearchUser()
        {
            DRXClient client = new DRXClient(_configManager);
            User user = client.SearchUser(UserIdentifierType.Email, "belinda.spiteri@yum.com");
            user = client.SearchUser(UserIdentifierType.Guid, user.Guid);
            Assert.IsNotNull(user);
            user = client.SearchUser(UserIdentifierType.Mobile, "123456789");
            Assert.IsNotNull(user);
        }

        [Test]
        public void TestSearchUsers()
        {
            DRXClient client = new DRXClient(_configManager);
            List<User> user = client.SearchUsers(UserIdentifierType.Email, new List<string> { "dominic.catherin@digitalreceiptexchange.com"});
            Assert.IsNotNull(user);
            Assert.AreEqual(1, user.Count);
        }
    }
}
