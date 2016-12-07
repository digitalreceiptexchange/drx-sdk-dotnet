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
using Net.Dreceiptx.Client;
using Net.Dreceiptx.Client.Exceptions;
using Net.Dreceiptx.Receipt.AllowanceCharge;
using Net.Dreceiptx.Receipt.Builders;
using Net.Dreceiptx.Receipt.Common;
using Net.Dreceiptx.Receipt.Config;
using Net.Dreceiptx.Receipt.Document;
using Net.Dreceiptx.Receipt.Ecom;
using Net.Dreceiptx.Receipt.Invoice;
using Net.Dreceiptx.Receipt.LineItem;
using Net.Dreceiptx.Receipt.Serialization.Json;
using Net.Dreceiptx.Receipt.Tax;
using Net.Dreceiptx.UnitTests.Receipt.Document;
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
            //_configManager.SetConfigValue("exchange.hostname", "144.132.230.201:56150");
            _configManager.SetConfigValue("directory.hostname", "aus-beta-directory.dreceiptx.net");
            //_configManager.SetConfigValue("api.requesterId", "UAT_AUS_CONCIERGE_TRAVEL_GROUP-1");
            _configManager.SetConfigValue("api.requesterId", "SYSTEM_TEST-1");
            _configManager.SetConfigValue("receipt.version", "1.3.0");
            _configManager.SetConfigValue("user.version", "1.1.0");
            _configManager.SetConfigValue("download.directory", "");
            _configManager.SetConfigValue("exchange.protocol", "https");
            _configManager.SetConfigValue("directory.protocol", "https");
            _configManager.SetConfigValue("environment.type", "UAT");
            //_configManager.SetConfigValue("api.secret", "HPmlotenxFgnT19hXHAkAsmeKxDTiU2lmrGv1tkqta");
            _configManager.SetConfigValue("api.secret", "M26BNoprX2UdJ2EsEjzGa3NmfvIhP7dHrymlHdOqAW");
            //_configManager.SetConfigValue("api.key", "otuwmVfSFZhfpSy6gcCh");
            _configManager.SetConfigValue("api.key", "9mf8u3EuUQfvTA16aYua");

            //            SYSTEM_TEST-1
            //key: 9mf8u3EuUQfvTA16aYua
            //secret: M26BNoprX2UdJ2EsEjzGa3NmfvIhP7dHrymlHdOqAW
        }

        [Test]
        public void TestSearchUser()
        {
            DRXClient client = new DRXClient(_configManager);
            //User user = client.SearchUser(UserIdentifierType.Email, "johannes.lambinon@yum.com");
            //User user = client.SearchUser(UserIdentifierType.Email, "Fatima.Syed@yum.com");
            User user1 = client.SearchUser(UserIdentifierType.Email, "david.gorecki@yum.com");
            Assert.IsNotNull(user1);
            User user2 = client.SearchUser(UserIdentifierType.Guid, user1.Guid);
            Assert.IsNotNull(user2);
            Assert.AreEqual(user1.Guid, user2.Guid);
            Assert.AreEqual(user1.Rms, user2.Rms);
            //user = client.SearchUser(UserIdentifierType.Mobile, "123456789");
            //Assert.IsNotNull(user);
        }

        [Test]
        public void TestSearchUsers()
        {
            DRXClient client = new DRXClient(_configManager);
            List<User> user = client.SearchUsers(UserIdentifierType.Email, new List<string>
            {
                "david.gorecki@yum.com",
                "johannes.lambinon@yum.com",
                "Fatima.Syed@yum.com"

            });
            Assert.IsNotNull(user);
            Assert.AreEqual(3, user.Count);
        }

        [Test]
        public void TestNewUserRegistration()
        {
            NewUser newUser = new NewUser($"dominic.catherin{DateTime.Now.Ticks}@digitalreceiptexchange.com", true);
            newUser.Config = new List<UserConfigurationOption>();
            newUser.AddIdentifier(UserIdentifierType.Mobile, "0401858293");
            newUser.AddConfigOption(UserConfigOptionType.EndPointId, "ENDPOINTID1");
            DRXClient client = new DRXClient(_configManager);
            client.RegisterNewUser(newUser);
        }

        [Test]
        public void TestNewUserRegistrationMultipleUsers()
        {
            NewUser newUser = new NewUser($"dominic.catherin{DateTime.Now.Ticks}@digitalreceiptexchange.com", true);
            newUser.Config = new List<UserConfigurationOption>();
            newUser.AddIdentifier(UserIdentifierType.Mobile, "0401858293");
            newUser.AddConfigOption(UserConfigOptionType.EndPointId, "ENDPOINTID1");
            NewUser newUser2 = new NewUser($"dominic.catherin_{DateTime.Now.Ticks}@digitalreceiptexchange.com", true);
            newUser2.Config = new List<UserConfigurationOption>();
            newUser2.AddIdentifier(UserIdentifierType.Mobile, "0401858293");
            newUser2.AddConfigOption(UserConfigOptionType.EndPointId, "ENDPOINTID1");
            DRXClient client = new DRXClient(_configManager);
            var result = client.RegisterNewUser(new List<NewUser> {newUser, newUser2});
        }

        [Test]
        public void TestNewUserRegistrationWithFailedAuth()
        {
            NewUser newUser = new NewUser("dominic.catherin4@digitalreceiptexchange.com", true);
            newUser.Config = new List<UserConfigurationOption>();
            newUser.AddIdentifier(UserIdentifierType.Mobile, "0401858293");
            newUser.AddConfigOption(UserConfigOptionType.EndPointId, "ENDPOINTID1");
            _configManager.SetConfigValue("api.key", "SUMMY_VALUE");
            DRXClient client = new DRXClient(_configManager);
            Assert.Throws<ExchangeClientException>(() =>
            {
                // call it twice to cause duplicate erro
                client.RegisterNewUser(newUser);
                client.RegisterNewUser(newUser);
            });

        }

        [Test]
        public void TestNewUserRegistrationWhenInvalidData()
        {
            string email = "dominic.catherin4@digitalreceiptexchange.com";
            NewUser newUser = new NewUser(email, true);
            DRXClient client = new DRXClient(_configManager);
            var result = client.RegisterNewUser(newUser);
            Assert.IsFalse(result.ExchangeResponse.ResponseData.Users[email].Success);
            Assert.IsNotEmpty(result.ExchangeResponse.ResponseData.Users[email].Exception);
        }

        [Test]
        public void TestSendReceipt()
        {
            StandardBusinessDocumentHeaderBuilder builder = new StandardBusinessDocumentHeaderBuilder();
            StandardBusinessDocumentHeader header = builder.MerchangeGLN("anz_concierge")
                .DrxFLN("AUS_ALPHA_EXPENSEMANAGER")
                .UserIdentifier(UserIdentifierType.Guid, "UATANZALPHAUSR14660443061787969")
                //.AddMerchantContact(new ReceiptContact(ReceiptContactType.SALES_ADMINISTRATION, "Sabre Online"))
                //.AddRMSContact(new ReceiptContact(ReceiptContactType.PURCHASING_CONTACT, "Grignell Michelle"))
                //.AddRMSContact(new ReceiptContact(ReceiptContactType.RECIPIENT_CONTACT, "Clarke Emma MS"))
                .DocumentInformation().InstanceIdentifier("UATANZALPHADR0000000014679796096749582")
                .TypeVersion("1.2.0")
                .CreationDateAndTime(new DateTime(2016, 11, 2, 12, 0, 0, DateTimeKind.Local))
                .Builder()
                .Build();

            DigitalReceipt digitalReceiptMessage = new DigitalReceipt();
            digitalReceiptMessage.StandardBusinessDocumentHeader = header;
            Invoice invoice = new Invoice();
            digitalReceiptMessage.Invoice = invoice;
            invoice.InvoiceCurrencyCode = Currency.AustralianDollar.Value();
            invoice.CountryOfSupplyOfGoods = "AUS";
            invoice.CreationDateTime = DateTime.Now;
            invoice.InvoiceIdentification = new Identification("206716");
            invoice.CustomerReference = new Identification("CustomerReference1234");
            invoice.PurchaseOrder = new Identification("PUchaseOrder1234");
            invoice.OriginInformation.Address = new Address("1 Sydney Road", "Sydney", "2000", "NSW", "AUS");
            invoice.DestinationInformation.Address = new Address("1000 Sydney Road", "Sydney", "2000", "NSW", "AUS");
            invoice.DespatchInformation.DeliveryDate = new DateTime(2016, 12, 25);
            invoice.DespatchInformation.DeliveryInstructions = "Under the Christmas tree";
            invoice.DespatchInformation.DespatchDateTime = new DateTime(2016, 12, 25);
            invoice.AddLineItem(new StandardLineItem("Google", "Google Cast Chrome", "TV THing", 1, 10.00m));
            invoice.AddLineItem(new StandardLineItem("Samsung", "Samsung Note 7", "Flame Thrower", 1, 1349.00m)
            { BatchNumber = "Batch1234", SerialNumber = "SerialNumber1234" });
            invoice.InvoiceLineItems[0].AddEcomAVP(new AVP("AVP_Name", "AVP_Value"));
            invoice.InvoiceLineItems[0].AddReceiptAllowanceCharges(ReceiptAllowanceCharge.Tip(1, "Good Service Tip", new Tax(10, 1, TaxCategory.APPLICABLE, TaxCode.GoodsAndServicesTax)));
            invoice.InvoiceLineItems[0].AddReceiptAllowanceCharges(ReceiptAllowanceCharge.FreightFee(10, "Freight Fee", new Tax(10, 1, TaxCategory.APPLICABLE, TaxCode.GoodsAndServicesTax)));
            invoice.InvoiceLineItems[0].AddTax(new Tax(1000, 3, TaxCategory.APPLICABLE, TaxCode.EnvironmentalTax));
            DRXClient client = new DRXClient(_configManager);
            client.SendReceipt(digitalReceiptMessage);

        }
    }
}
