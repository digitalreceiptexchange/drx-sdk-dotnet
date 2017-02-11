using System;
using System.Diagnostics;
using System.IO;
using Net.Dreceiptx.Receipt;
using Net.Dreceiptx.Receipt.AllowanceCharge;
using Net.Dreceiptx.Receipt.Builders;
using Net.Dreceiptx.Receipt.Common;
using Net.Dreceiptx.Receipt.Common.Measurements;
using Net.Dreceiptx.Receipt.Config;
using Net.Dreceiptx.Receipt.Document;
using Net.Dreceiptx.Receipt.Ecom;
using Net.Dreceiptx.Receipt.Invoice;
using Net.Dreceiptx.Receipt.LineItem;
using Net.Dreceiptx.Receipt.LineItem.Construction;
using Net.Dreceiptx.Receipt.LineItem.Travel;
using Net.Dreceiptx.Receipt.Serialization;
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
            DictionaryConfigManager _configManager = new DictionaryConfigManager();
            _configManager.SetConfigValue("drx.gln", "9377778071234");
            _configManager.SetConfigValue("merchant.gln", "anz_concierge");
            _configManager.SetConfigValue("default.currency", "AUD");
            _configManager.SetConfigValue("default.country", "AUS");
            _configManager.SetConfigValue("default.language", "ENG");
            _configManager.SetConfigValue("default.timezone", "AEDT");
            _configManager.SetConfigValue("default.taxCategory", "APPLICABLE");
            _configManager.SetConfigValue("default.taxCode", "GoodsAndServicesTax");
            _configManager.SetConfigValue("receipt.version", "1.3.0");
            DigitalReceiptGenerator _newDigitalReceipt = new DigitalReceiptGenerator(_configManager);
            _newDigitalReceipt.SetUserGUID(UserIdentifierType.Guid, "GUID12345678910");
            _newDigitalReceipt.AddLineItem("Test Brand", "Test Name", "Test Description", 1, 2.30m);
            MaterialGeneric materialGeneric = new MaterialGeneric("Test MaterialGeneric", "Test MaterialGeneric",
                "Test MaterialGeneric Description", 1, 2.30m);
            materialGeneric.SetMeasurements(1200, 1100, 1000, MeasurementType.MILLIMETRE);
            _newDigitalReceipt.AddLineItem(materialGeneric);
            _newDigitalReceipt.AddLineItem(new StandardLineItem("Test StandardLineItem", "Test StandardLineItem", "Test StandardLineItem Description", 1, 30.00m));
            GroundTransport groundTransport = new GroundTransport(GroundTransportType.TAXI, "GroundTransport Provider",
                "Test GroundTransport", "Test GroundTransport Description", 14.30m);
            groundTransport.AddTax(new Tax(30.00m, 3.00m));
            _newDigitalReceipt.AddLineItem(groundTransport);
            _newDigitalReceipt.SetMerchantReference("1234567");
            _newDigitalReceipt.SetPurchaseOrderNumber("123456789");
            DigitalReceiptMessage _digitalReceiptMessage = _newDigitalReceipt.GetDigitalReceiptMessage();
            string json = _digitalReceiptMessage.SerializeToJson();
            Debug.WriteLine(json);

            /*StandardBusinessDocumentHeaderBuilder builder = new StandardBusinessDocumentHeaderBuilder();
            StandardBusinessDocumentHeader header = builder.MerchantGLN("anz_concierge")
                .DRxGLN("AUS_ALPHA_EXPENSEMANAGER")
                .UserIdentifier(UserIdentifierType.Guid, "UATANZALPHAUSR14660443061787969")
                .AddMerchantContact(new ReceiptContact(ReceiptContactType.SALES_ADMINISTRATION, "Sabre Online"))
                .AddRMSContact(new ReceiptContact(ReceiptContactType.PURCHASING_CONTACT, "Grignell Michelle"))
                .AddRMSContact(new ReceiptContact(ReceiptContactType.RECIPIENT_CONTACT, "Clarke Emma MS"))
                .DocumentInformation().InstanceIdentifier("UATANZALPHADR0000000014679796096749582")
                .TypeVersion("1.2.0")
                .CreationDateAndTime(new DateTime(2016, 11, 2, 12, 0, 0, DateTimeKind.Local))
                .Builder()
                .Build();

            DigitalReceiptMessageWrapper wrapper = new DigitalReceiptMessageWrapper();
            
            DigitalReceipt digitalReceiptMessage = new DigitalReceipt();
            wrapper.DRxDigitalReceipt = digitalReceiptMessage;
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



            string result1 = wrapper.SerializeToJson();
            //Console.WriteLine($"Result1: {result1}");

            var message = DigitalReceiptMessageWrapper.DeserializeFromJson(result1);
            string result2 = message.SerializeToJson();
            //Console.WriteLine($"Result2: {result2}");
            //foreach (var lineItem in message.DRxDigitalReceipt.Invoice.InvoiceLineItems)
            //{
            //    lineItem.
            //}

            Assert.AreEqual(result1, result2);
            //string result = new { dRxDigitalReceipt = receipt}.SerializeToJson();}*/

        }

        [Test]
        public void TestSample1()
        {
            //string json = File.ReadAllText(".\\SampleFiles\\Sample1.json");
            //var message = DigitalReceiptMessageWrapper.DeserializeFromJson(json);

        }

        [Test]
        public void TestSample2()
        {
            string json = File.ReadAllText(".\\SampleFiles\\Sample3.json");
            //DigitalReceiptMessage message = DigitalReceiptMessage.DeserializeFromJson(json);
            DigitalReceipt dr = new DigitalReceipt(json);
            //string jsonOutput = dr.toJson();
            //Debug.WriteLine(jsonOutput);
            Currency drCurrency = dr.Currency;
            Debug.WriteLine(drCurrency.Description());
            foreach (Dreceiptx.Receipt.LineItem.LineItem lineitem in dr.LineItems)
            {
                Debug.WriteLine("TYPE: "+lineitem.GetType().Name);
                if (lineitem.GetType() == typeof(MaterialGeneric))
                {
                    MaterialGeneric materialGeneric = (MaterialGeneric) lineitem;
                    Debug.WriteLine(materialGeneric.BrandName);
                    Debug.WriteLine(materialGeneric.Name);
                    Debug.WriteLine(materialGeneric.Description);
                    Debug.WriteLine("MeasurementDepth: " + materialGeneric.MeasurementDepth);
                    Debug.WriteLine("MeasurementHeight: " + materialGeneric.MeasurementHeight);
                    Debug.WriteLine("MeasurementDiameter: " + materialGeneric.MeasurementDiameter);
                    Debug.WriteLine("MeasurementWidth: " + materialGeneric.MeasurementWidth);
                }
                else{
                    //StandardLineItem materialGeneric = (StandardLineItem)lineitem;
                    Debug.WriteLine(lineitem.BrandName);
                    Debug.WriteLine(lineitem.Name);
                    Debug.WriteLine(lineitem.Description);
                }
            }
            //Assert.AreEqual(json, jsonOutput);

            //Assert.AreEqual("anz_concierge", message.DRxDigitalReceipt.StandardBusinessDocumentHeader.Sender[0].Identifier.Value);
            //Assert.AreEqual("GS1", message.DRxDigitalReceipt.StandardBusinessDocumentHeader.Sender[0].Identifier.Authority);

        }
    }
}
