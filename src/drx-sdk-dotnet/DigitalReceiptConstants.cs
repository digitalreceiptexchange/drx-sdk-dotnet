namespace Net.Dreceiptx
{
    /// <summary>
    /// Class containing constant values used by DigitalReceiptExchange
    /// </summary>
    public static class DigitalReceiptConstants
    {
        /// <summary>
        /// Constant values reported as Validation Errors
        /// </summary>
        public static class ValidationErrors
        {
            public const string ReceiptMustHaveALeastLineItem = "Digital Receipt must have a least 1 line item.";
            public const string ReceiptTypeMustBeSet = "Receipt Type must be set.";
            public const string CurrencyCodeMustBeSet = "Currency Code must be set.";
            public const string CountryCodeMustBeSet = "Country Code must be set.";
            public const string MerchantGlnMustBeSet = "Merchant GLN must be set.";
            public const string UserGuidMustBeSet = "User GUID must be set.";
            public const string MerchantReferencenumberMustBeSet = "Merchant referenceNumber must be set.";
            public const string ReceiptTransactionDateMustBeSet = "Receipt transactionDateTime must be set.";
        }
    }
}