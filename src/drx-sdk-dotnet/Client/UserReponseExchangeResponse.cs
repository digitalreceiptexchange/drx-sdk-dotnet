namespace Net.Dreceiptx.Client
{
    public class UserReponseExchangeResponse
    {
        public bool Success { get; set; }
        public int? Code { get; set; }
        public string ExceptionMessage { get; set; }
        public UserReponseResponseData ResponseData { get; set; }
    }
}