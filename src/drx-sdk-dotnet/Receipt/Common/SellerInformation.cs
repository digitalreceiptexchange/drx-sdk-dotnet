using System.Runtime.Serialization;

namespace Net.Dreceiptx.Receipt.Common
{
    public class Organisation
    {
        [DataMember]
        public string OrganisationName { get; set; }
    }
}
