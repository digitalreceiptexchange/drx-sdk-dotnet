using System.Collections.Generic;

namespace Net.Dreceiptx
{
    public class PaymentReceipt
    {
        private readonly List<ReceiptAllowanceCharge> _adminFees = new List<ReceiptAllowanceCharge>();
        public string PaymentReceiptId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public decimal PaymentAmount { get; set; }
        public string CurrencyCode { get; set; }

        public PaymentReceipt(string paymentReceiptId, PaymentMethod paymentMethod,
            decimal paymentAmount, string currencyCode)
        {
            PaymentReceiptId = paymentReceiptId;
            PaymentMethod = paymentMethod;
            PaymentAmount = paymentAmount;
            CurrencyCode = currencyCode;
        }

        public PaymentAuthorisation PaymentAuthorisationDetail { get; set; }
        public List<ReceiptAllowanceCharge> PaymentAdminFees { get; set; }

        public void AddPaymentAdminFee(decimal amount, string description, TaxFee tax)
        {
            ReceiptAllowanceCharge adminFee = new ReceiptAllowanceCharge("CHARGE",
                "FI", "CHARGE_TO_BE_PAID_BY_CUSTOMER", amount, description, tax);
            _adminFees.Add(adminFee);
        }
    }

    public enum PaymentMethod : short
    {
        NotDefined,
        Cash,
        CreditCard,
        DebitCard,
        Cheque
    }
}