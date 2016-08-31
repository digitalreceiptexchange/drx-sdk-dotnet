using System.Runtime.Serialization;
using Net.Dreceiptx.GS1.SDBH;

namespace Net.Dreceiptx
{
    [DataContract]
    public abstract class RequestBase<T> : MessageBase
    {
        protected RequestBase()
        {
            StandardBusinessDocumentHeader = new StandardBusinessDocumentHeader();
        }

        [DataMember]
        public StandardBusinessDocumentHeader StandardBusinessDocumentHeader { get; set; }
    }
}