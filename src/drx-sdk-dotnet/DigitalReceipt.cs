using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Net.Dreceiptx
{
    /// <summary>
    /// Class representing a DigitalRecipt instance
    /// </summary>
    [DataContract]
    public class DigitalReceipt
    {
        private List<LineItem> _invoiceLineItem = new List<LineItem>();
        private readonly List<PaymentReceipt> _paymentReceipts = new List<PaymentReceipt>();
        private List<ReceiptAllowanceCharge> _allowanceOrCharges = new List<ReceiptAllowanceCharge>();
        private int _allowanceOrChargeId = 1;
        private const string Tip = "TIP";
        private const string Original = "ORIGINAL";
        private const string ChargeToBePaidByCustomer = "CHARGE_TO_BE_PAID_BY_CUSTOMER";
        protected const string CreditCustomerAccount = "CREDIT_CUSTOMER_ACCOUNT";
        private const string Discount = "DI";
        private const string PackagingFee = "PC";
        private const string DeliveryFee = "ADZ";
        private const string FrieghtCharge = "FC";

        #region Constructors
        

        /// <summary>
        /// Creates an instance of the DigitalReceipt.
        /// </summary>
        public DigitalReceipt()
        {
            InvoiceIdentification = new InvoiceIdentification();
            InvoiceTotals = new Totals();
        }

        #endregion Constructors

        [DataMember]
        public InvoiceIdentification InvoiceIdentification { get; set; }

        /// <summary> Gets and sets the InvoiceCurrencyCode </summary>
        [DataMember]
        public string InvoiceCurrencyCode { get; set; }
        
        [DataMember]
        public List<LineItem> InvoiceLineItem
        {
            get { return _invoiceLineItem; }
            set { _invoiceLineItem = value; }
        }

        [DataMember]
        public string DocumentStatusCode { get; set; }

        /// <summary>
        /// Gets the Total of the order. This is a calculated value based on
        /// the properties of the receipt. This will not be sent to dRx.
        /// </summary>
        public decimal Total
        {
            get { return SubTotal + SubTotalTax + SubTotalCharges - SubTotalAllowances; }
        }

        public decimal TaxPercentage
        {
            get
            {
                decimal subTotal = SubTotal;
                decimal taxPercentage = 0;
                if (subTotal != 0)
                {
                    taxPercentage = (SubTotalTax/SubTotal)*100;
                }
                return taxPercentage;
            }
        }

        /// <summary>
        /// Validates if the current DigitalReceipt instance is valid. The returned ReceiptValidation
        /// instance will indicate if the receipt is valid or not and also indicate the missing
        /// data on the DigitalReceipt
        /// </summary>
        /// <returns>A ReceiptValidation instance will always be returned from this method. You should check
        /// the IsValid propery on the returned instance to see if the DigitalReceipt is valid.</returns>
        public ReceiptValidation Validate()
        {
            ReceiptValidation receiptValidation = new ReceiptValidation();
            if(_invoiceLineItem.Count < 1)
            {
                receiptValidation.AddError(DigitalReceiptConstants.ValidationErrors.ReceiptMustHaveALeastLineItem);
            }
            
            if(string.IsNullOrWhiteSpace(InvoiceCurrencyCode))
            {
                receiptValidation.AddError(DigitalReceiptConstants.ValidationErrors.CurrencyCodeMustBeSet);
            }
            return receiptValidation;

        }

        public decimal SubTotal
        {
            get { return _invoiceLineItem.Sum(x => x.Total); }
            
        }

        public decimal SubTotalTax
        {
            get 
            { 
                return _invoiceLineItem.Sum(x => x.TaxesTotal) +
                       _allowanceOrCharges.Where(x=>x.Tax != null).Sum(x=>x.Tax.TaxAmount);
            }

        }

        public decimal SubTotalCharges
        {
            get
            {
                return _invoiceLineItem.Sum(x => x.ChargesTotal) + 
                       _allowanceOrCharges.Where(x=>x.AllowanceOrCharge == "CHARGE").Sum(x=>x.Amount);
            }

        }

        public decimal SubTotalAllowances
        {
            get { return _invoiceLineItem.Sum(x => x.ChargesTotal) + 
                         _allowanceOrCharges.Where(x=>x.AllowanceOrCharge == "ALLOWANCE").Sum(x=>x.Amount); }

        }

        [DataMember]
        public Totals InvoiceTotals { get; set; }

        public class Totals
        {
            public Totals()
            {
                TotalInvoiceAmount = new Amount();
                TotalLineAmountInclusiveAllowancesCharges = new Amount();
                TotalTaxAmount = new Amount();
            }
            [DataMember]
            public Amount TotalInvoiceAmount { get; set; }

            [DataMember]
            public Amount TotalLineAmountInclusiveAllowancesCharges { get; set; }
            [DataMember]
            public Amount TotalTaxAmount { get; set; }
        }

        public class Amount
        {
            public Amount()
            {
                CurrencyCode = "AUD";
            }
            [DataMember]
            public string CurrencyCode { get; set; }
            [DataMember]
            public double Value { get; set; }
        }
        [DataMember]
        public string InvoiceType { get; set; }
        [DataMember]
        public string CountryOfSupplyOfGoods { get; set; }

        public void AddLineItem(LineItem lineItem)
        {
            _invoiceLineItem.Add(lineItem);
        }

        public void RemoveLineItem(int lineItemId)
        {
            LineItem item = _invoiceLineItem.Find(x => x.LineItemNumber == lineItemId);
            if (item != null)
            {
                _invoiceLineItem.Remove(item);
            }
        }

        public void AddPaymentReceipt(PaymentReceipt paymentReceipt)
        {
            _paymentReceipts.Add(paymentReceipt);
        }

        public void RemovePaymentReceipt(string paymentId)
        {
            PaymentReceipt item = _paymentReceipts.Find(x => x.PaymentReceiptId == paymentId);
            _paymentReceipts.Remove(item);
        }

        public void RemoveAllowanceOrChange(int id)
        {
            var item = _allowanceOrCharges.Find(x => x.ReceiptAllowanceChargeId == id);
            _allowanceOrCharges.Remove(item);
        }

        private void AddAllowanceOrCharge(ReceiptAllowanceCharge receiptAllowanceCharge)
        {
            receiptAllowanceCharge.ReceiptAllowanceChargeId = _allowanceOrChargeId++;
            _allowanceOrCharges.Add(receiptAllowanceCharge);
        }
        public ReceiptAllowanceCharge AddGeneralDiscount(decimal amount, string description, TaxFee tax)
        {
            ReceiptAllowanceCharge discount = new ReceiptAllowanceCharge(
                "ALLOWANCE",
                Discount, CreditCustomerAccount, amount, description, tax);
            AddAllowanceOrCharge(discount);
            return discount;
        }

        public ReceiptAllowanceCharge AddTip(decimal amount, string description, TaxFee tax)
        {
            ReceiptAllowanceCharge tip = new ReceiptAllowanceCharge(
                "CHARGE",
                Tip, ChargeToBePaidByCustomer, amount, description, tax);

            AddAllowanceOrCharge(tip);
            return tip;
        }

        public ReceiptAllowanceCharge AddPackagingFee(decimal amount, string description, TaxFee tax)
        {
            ReceiptAllowanceCharge packingFee = new ReceiptAllowanceCharge(
                "CHARGE",
                PackagingFee, ChargeToBePaidByCustomer, amount, description, tax);
            AddAllowanceOrCharge(packingFee);
            return packingFee;
        }

        public ReceiptAllowanceCharge AddDeliveryFee(decimal amount, string description, TaxFee tax)
        {
            ReceiptAllowanceCharge deliveryFee = new ReceiptAllowanceCharge(
                "CHARGE",
                DeliveryFee, ChargeToBePaidByCustomer, amount, description, tax);
            AddAllowanceOrCharge(deliveryFee);
            return deliveryFee;
        }

        public ReceiptAllowanceCharge AddFrieghtFee(decimal amount, string description, TaxFee tax)
        {
            ReceiptAllowanceCharge tip = new ReceiptAllowanceCharge(
                "CHARGE",
                FrieghtCharge, ChargeToBePaidByCustomer, amount, description, tax);
            AddAllowanceOrCharge(tip);
            return tip;
        }
    }
}