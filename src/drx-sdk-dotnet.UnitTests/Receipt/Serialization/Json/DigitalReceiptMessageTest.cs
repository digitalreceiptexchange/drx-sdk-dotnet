using System;
using Net.Dreceiptx.Receipt.AllowanceCharge;
using Net.Dreceiptx.Receipt.Common;
using Net.Dreceiptx.Receipt.Document;
using Net.Dreceiptx.Receipt.Ecom;
using Net.Dreceiptx.Receipt.Invoice;
using Net.Dreceiptx.Receipt.LineItem;
using Net.Dreceiptx.Receipt.Serialization.Json;
using Net.Dreceiptx.Receipt.Tax;
using Net.Dreceiptx.UnitTests.Receipt.Document;
using Net.Dreceiptx.Users;
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
                .UserIdentifier(UserIdentifierType.Guid, "UATANZALPHAUSR14660443061787969")
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
            invoice.DespatchInformation.DespatchDateTime = new DateTime(2016, 12, 25);
            invoice.AddLineItem(new StandardLineItem("Google", "Google Cast Chrome", "TV THing", 1, 10.00m));
            invoice.AddLineItem(new StandardLineItem("Samsung", "Samsung Note 7", "Flame Thrower", 1, 1349.00m)
            { BatchNumber = "Batch1234", SerialNumber = "SerialNumber1234"});
            invoice.InvoiceLineItems[0].AddEcomAVP(new AVP("AVP_Name", "AVP_Value"));
            invoice.InvoiceLineItems[0].AddReceiptAllowanceCharges(ReceiptAllowanceCharge.Tip(1, "Good Service Tip", new Tax(10, 1, TaxCategory.APPLICABLE, TaxCode.GoodsAndServicesTax)));
            invoice.InvoiceLineItems[0].AddReceiptAllowanceCharges(ReceiptAllowanceCharge.FreightFee(10, "Freight Fee", new Tax(10, 1, TaxCategory.APPLICABLE, TaxCode.GoodsAndServicesTax)));
            invoice.InvoiceLineItems[0].AddTax(new Tax(1000, 3, TaxCategory.APPLICABLE, TaxCode.EnvironmentalTax));



            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                //DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                DateFormatString = "yyyy-MM-ddTHH:mm:ss%K",
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
                //TypeNameHandling =  TypeNameHandling.Objects,
                
            };
            settings.Converters.Add(new StringEnumConverter());
            string result1 = JsonConvert.SerializeObject(digitalReceiptMessage, settings);
            Console.WriteLine($"Result1: {result1}");

            var receipt = JsonConvert.DeserializeObject<DigitalReceiptMessage>(result1, settings);
            string result2 = JsonConvert.SerializeObject(receipt, settings);
            Console.WriteLine($"Result2: {result2}");

            Assert.AreEqual(result1, result2);
            //string result = new { dRxDigitalReceipt = receipt}.SerializeToJson();}

        }
    }
}
