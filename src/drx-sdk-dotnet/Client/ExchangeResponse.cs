namespace Net.Dreceiptx.Client
{
    public class ExchangeResponse
    {
        public bool Success { get; set; }
        public int? Code { get; set; }
        public string ExceptionMessage { get; set; }
        public ResponseData ResponseData { get; set; }
        
    }
}