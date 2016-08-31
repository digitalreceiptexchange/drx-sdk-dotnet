namespace Net.Dreceiptx
{
    public class PaymentAuthorisation
    {
        public string PaymentProvider { get; set; }
        public string AuthorisationId { get; set; }
        public string AuthorisationCode { get; set; }

        public PaymentAuthorisation(string paymentProvider, string authorisationId,
            string authorisationCode)
        {
            PaymentProvider = paymentProvider;
            AuthorisationId = authorisationId;
            AuthorisationCode = authorisationCode;
        }
    }
}