using System.Runtime.Serialization;

namespace Net.Dreceiptx
{
    [DataContract]
    public class Address
    {
        protected Address()
        {
        }

        public Address(string name, string streetAddress1, 
            string streetAddress2, string streetAddress3, 
            string city, string postalCode,
            string state, string countryCode)
        {
            Name = name;
            StreetAddress1 = streetAddress1;
            StreetAddress2 = streetAddress2;
            StreetAddress3 = streetAddress3;
            City = city;
            PostalCode = postalCode;
            State = state;
            CountryCode = countryCode;
        }

        [DataMember]
        public string Name { get; set; }
        
        [DataMember]
        public string StreetAddress1 { get; set; }
        
        [DataMember]
        public string StreetAddress2 { get; set; }
        
        [DataMember]
        public string StreetAddress3 { get; set; }
        
        [DataMember]
        public string City { get; set; }
        
        [DataMember]
        public string PostalCode { get; set; }
        
        [DataMember]
        public string State { get; set; }
        
        [DataMember]
        public string CountryCode { get; set; }
    }
}