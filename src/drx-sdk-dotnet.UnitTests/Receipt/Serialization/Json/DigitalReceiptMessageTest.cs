using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Net.Dreceiptx.Receipt.Common;
using Net.Dreceiptx.Receipt.Document;
using Net.Dreceiptx.Receipt.Invoice;
using Net.Dreceiptx.Receipt.LineItem;
using Net.Dreceiptx.Receipt.Serialization.Json;
using Net.Dreceiptx.UnitTests.Receipt.Document;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using NUnit.Framework;

namespace Net.Dreceiptx.UnitTests.Receipt.Serialization.Json
{
    [TestFixture]
    public class DigitalReceiptMessageTest
    {
        [Test]
        public void TestSerialization()
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
                .CreationDateAndTime(new DateTime(2016, 11, 2, 12, 0, 0, DateTimeKind.Local))
                .Builder()
                .Build();

            DigitalReceiptMessage digitalReceiptMessage = new DigitalReceiptMessage();
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
            invoice.DespatchInformation.DeliveryDate = new DateTime(2016,12,25);
            invoice.DespatchInformation.DeliveryInstructions = "Under the Christmas tree";
            invoice.DespatchInformation.DespatchDate = new DateTime(2016, 12, 25);
            invoice.AddLineItem(new StandardLineItem("Google", "Google Cast Chrome", "TV THing", 1, 10.00m));
            invoice.AddLineItem(new StandardLineItem("Samsung", "Samsung Note 7", "Flame Thrower", 1, 1349.00m));


            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                //DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                DateFormatString = "yyyy-MM-ddTHH:mm:ss%K",
                Formatting = Formatting.Indented,
                //TypeNameHandling =  TypeNameHandling.Objects
            };
            settings.Converters.Add(new StringEnumConverter());
            string result = JsonConvert.SerializeObject(digitalReceiptMessage, settings);

            //string result = new { dRxDigitalReceipt = receipt}.SerializeToJson();}
            Console.WriteLine(result);
        }
    }
}
