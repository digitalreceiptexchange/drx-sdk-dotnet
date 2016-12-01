using System.Runtime.Serialization;

namespace Net.Dreceiptx.Client
{
    [DataContract]
    public class UserReponseExchangeResponse
    {
        [DataMember]
        public bool Success { get; set; }
        [DataMember]
        public int? Code { get; set; }
        [DataMember]
        public string ExceptionMessage { get; set; }
        [DataMember]
        public UserReponseResponseData ResponseData { get; set; }
    }

    public class NewUserRegistrationExchangeResponse
    {
        public NewUserRegistrationResponse ExchangeResponse { get; set; }
    }

    [DataContract]
    public class NewUserRegistrationResponse
    {
        [DataMember]
        public bool Success { get; set; }
        [DataMember]
        public int? Code { get; set; }
        [DataMember]
        public string ExceptionMessage { get; set; }
        [DataMember]
        public NewUserRegistrationResponseData ResponseData { get; set; }
    }
}