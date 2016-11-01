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
using Net.Dreceiptx.Receipt.Document;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using NUnit.Framework;

namespace Net.Dreceiptx.UnitTests.Receipt.Document
{
    [TestFixture]
    public class StandardBusinessDocumentHeaderTest
    {
        [Test]
        public void TestSeralization()
        {
            StandardBusinessDocumentHeaderBuilder builder = new StandardBusinessDocumentHeaderBuilder();
            StandardBusinessDocumentHeader header = builder.MerchangeGLN("anz_concierge")
                .DrxFLN("AUS_ALPHA_EXPENSEMANAGER")
                .UserIdentifier("UATANZALPHAUSR14660443061787969")
                .AddMerchantContact(new ReceiptContact(ReceiptContactType.SALES_ADMINISTRATION, "Sabre Online"))
                .AddRMSContact(new ReceiptContact(ReceiptContactType.PURCHASING_CONTACT, "Grignell Michelle"))
                .AddRMSContact(new ReceiptContact(ReceiptContactType.RECIPIENT_CONTACT, "Clarke Emma MS"))
                .DocumentInformation().InstanceIdentifier("UATANZALPHADR0000000014679796096749582")
                .TypeVersion("1.2.0")
                .CreationDateAndTime(DateTime.Now)
                .Builder()
                .Build();

            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                //DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                DateFormatString = "yyyy-MM-ddTHH:mm:ss%K"
            };
            settings.Converters.Add(new StringEnumConverter());
            string result = JsonConvert.SerializeObject(header, Formatting.Indented, settings);
            //string result = new { dRxDigitalReceipt = receipt}.SerializeToJson();}
            Console.WriteLine(result);
        }
    }

    public class StandardBusinessDocumentHeaderBuilder
    {
        private StandardBusinessDocumentHeader _header;
        private DocumentInformationBuilder _documentInformationBuilder;
        public StandardBusinessDocumentHeaderBuilder()
        {
            _header = new StandardBusinessDocumentHeader();
            _documentInformationBuilder = new DocumentInformationBuilder(this, _header);

        }

        public StandardBusinessDocumentHeaderBuilder MerchangeGLN(string merchantGLN)
        {
            _header.MerchantGLN = merchantGLN;
            return this;
        }

        public StandardBusinessDocumentHeaderBuilder DrxFLN(string DdxFLN)
        {
            _header.DrxFLN = DdxFLN;
            return this;
        }

        public StandardBusinessDocumentHeaderBuilder UserIdentifier(string userIdentifier)
        {
            _header.UserIdentifier = userIdentifier;
            return this;
        }

        public StandardBusinessDocumentHeaderBuilder AddMerchantContact(ReceiptContact contact)
        {
            _header.Sender[0].AddDocumentOwnerContact(contact);
            return this;
        }

        public StandardBusinessDocumentHeaderBuilder AddRMSContact(ReceiptContact contact)
        {
            _header.Receiver[1].AddDocumentOwnerContact(contact);
            return this;
        }

        public StandardBusinessDocumentHeaderBuilder AddReceiver(DocumentOwner receiver)
        {
            _header.Receiver.Add(receiver);
            return this;
        }

        public DocumentInformationBuilder DocumentInformation()
        {
            return _documentInformationBuilder;
        }

        public StandardBusinessDocumentHeader Build()
        {
            return _header;
        }

        public class DocumentInformationBuilder
        {
            private readonly StandardBusinessDocumentHeaderBuilder _builder;
            private readonly StandardBusinessDocumentHeader _header;

            public DocumentInformationBuilder(StandardBusinessDocumentHeaderBuilder builder, StandardBusinessDocumentHeader header)
            {
                _builder = builder;
                _header = header;
            }

            public DocumentInformationBuilder TypeVersion(string typeVersion)
            {
                _header.DocumentIdentification.TypeVersion = typeVersion;
                return this;
            }

            public DocumentInformationBuilder InstanceIdentifier(string instanceIdentifier)
            {
                _header.DocumentIdentification.InstanceIdentifier = instanceIdentifier;
                return this;
            }

            public DocumentInformationBuilder CreationDateAndTime(DateTime creationDateAndTime)
            {
                _header.DocumentIdentification.CreationDateAndTime = creationDateAndTime;
                return this;
            }

            public StandardBusinessDocumentHeaderBuilder Builder()
            {
                return _builder;
            }
        }
    }
}