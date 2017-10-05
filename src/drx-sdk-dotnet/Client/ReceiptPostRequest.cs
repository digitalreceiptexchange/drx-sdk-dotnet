using Net.Dreceiptx.Receipt.Common;
using Net.Dreceiptx.Receipt.Config;
using Net.Dreceiptx.Receipt.Serialization;

namespace Net.Dreceiptx.Client
{
    public class ReceiptPostRequest
    {
        public DigitalReceiptMessage ReceiptMessage { get; set; }

        public bool IsDryRun { get; set; }

        public Location Location { get; set; }

        public Currency Currency { get; set; }

        public string UserGUID { get; set; }

        public string MerchantGLN { get; set; }

        public string dRxGLN { get; set; }

        public string JsonPayloadContent()
        {
            return ReceiptMessage.SerializeToJson();
        }
    }
}
