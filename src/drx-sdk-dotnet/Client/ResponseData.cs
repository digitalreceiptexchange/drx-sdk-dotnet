using System.Runtime.Serialization;

namespace Net.Dreceiptx.Client
{
    [DataContract]
    public class ResponseData
    {
        public string ReceiptId { get; set; }
        public string Reference { get; set; }
        public string Guid { get; set; }
        public string Rms { get; set; }
    }
}