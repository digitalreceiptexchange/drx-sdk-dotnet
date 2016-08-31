using System.Runtime.Serialization;

namespace Net.Dreceiptx
{
    [DataContract]
    public class Contact
    {
        /// <summary> Required for XML serialization </summary>
        protected Contact() { }
        
        public Contact(string channelCode, string value)
        {
            ChannelCode = channelCode;
            Value = value;
        }


        //TODO: What is ChannelCode?
        [DataMember]
        public string ChannelCode { get; set; }
        
        [DataMember]
        public string Value { get; set; }
    }
}