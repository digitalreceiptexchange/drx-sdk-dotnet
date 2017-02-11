using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using log4net;
using Net.Dreceiptx.Receipt.Serialization;
using Net.Dreceiptx.Receipt.Serialization.Json;

namespace Net.Dreceiptx.WebApi.Controllers
{
    public class DrxController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(DrxController));
        private static List<DigitalReceiptMessage> _receipts = new List<DigitalReceiptMessage>();

        static DrxController()
        {
            //CreateSampleData();
        }

        [HttpGet]
        public List<DigitalReceiptMessage> GetAllReciepts()
        {
            Log.DebugFormat("Returning {0} receipts", _receipts.Count);
            return _receipts;
        }


        [HttpPost]
        public HttpResponseMessage Post(DigitalReceiptMessage receipt)
        {
            Log.DebugFormat("Receive Receipt: {0}", receipt.SerializeToJson());
            _receipts.Add(receipt);
            Log.DebugFormat("Total receipts {0}", _receipts.Count);
            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        //private static List<DrxDigitalReceipt> CreateSampleData()
        //{
        //    DrxDigitalReceipt receipt = new DrxDigitalReceipt();
        //    //receipt.StandardBusinessDocumentHeader.Sender.Identifier.Authority = "GS1";
        //    //receipt.StandardBusinessDocumentHeader.Sender.Identifier.Value = "9377778071234";
        //    //receipt.StandardBusinessDocumentHeader.Sender.ContactInformation = new List<ContactInformation>();
        //    ContactInformation contact = new ContactInformation{RepId = "123456789", RepName = "Dominic Catherin"};
        //    //receipt.StandardBusinessDocumentHeader.Sender.ContactInformation.Add(contact);
                
        //    receipt.StandardBusinessDocumentHeader.Receiver[0].Identifier.Authority = "GS1";
        //    receipt.StandardBusinessDocumentHeader.Receiver[0].Identifier.Value = "9377779309247";
        //    receipt.StandardBusinessDocumentHeader.DocumentIdentification.Standard = "GS1";
        //    receipt.StandardBusinessDocumentHeader.DocumentIdentification.TypeVersion = "DigitalReceipt v1.0";
        //    receipt.StandardBusinessDocumentHeader.DocumentIdentification.Type = "CONSUMER";
        //    receipt.StandardBusinessDocumentHeader.DocumentIdentification.InstanceIdentifier = "123456789";
        //    receipt.StandardBusinessDocumentHeader.DocumentIdentification.MultipleType = "true";
        //    receipt.StandardBusinessDocumentHeader.DocumentIdentification.CreationDateAndTime = DateTime.Now;


        //    //receipt.Invoice = new DigitalReceipt("AUD", "AUS", "TEST_GLN");
        //    receipt.Invoice = new DigitalReceiptRemoveMe();
        //    //receipt.Invoice.Delivery = new Delivery(DateTime.Now.AddDays(10), "Leave out the front Gov.!");
        //    //receipt.Invoice.Delivery.DeliveryAddress = new Address("Dominic Catherin",
        //    //"2/39 Lodge Street", "Balgowlah", null, "Sydney", "2093", "NSW", "AUS");
        //    LineItem lineItem = new LineItem(1, 50, 50);
        //    lineItem.TransactionalTradeItem = new LineItemTradeInformation("Chromecase", "Google Chromecast", false, false, "Google");
        //    lineItem.AddGeneralDiscount(5, "End of year sale", new TaxFee(1));
        //    receipt.Invoice.AddLineItem(lineItem);
        //    receipt.Invoice.AddTip(1, "Thanks for discount", new TaxFee(0));
        //    //receipt.Invoice.ReceiptType = "Consumer";
        //    //receipt.Invoice.UserGUID = Guid.NewGuid().ToString();
        //    //receipt.Invoice.MerchantReferenceNumber = "1234";
        //    //receipt.Invoice.TransactionDateTime = DateTime.Now;
        //    _receipts.Add(receipt);
        //    return _receipts;
        //}
    }

    public class LogRequestAndResponseHandler : DelegatingHandler
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(LogRequestAndResponseHandler));
        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // log request body
            string requestBody = await request.Content.ReadAsStringAsync();
            Log.DebugFormat("INCOMING::{0}", requestBody);

            // let other handlers process the request
            var result = await base.SendAsync(request, cancellationToken);

            if (result.Content != null)
            {
                // once response body is ready, log it
                var responseBody = await result.Content.ReadAsStringAsync();
                Log.DebugFormat("OUTGOING::{0}", requestBody);
            }

            return result;
        }
    }
}
