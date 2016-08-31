using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;
using System.Xml.Serialization;

namespace Net.Dreceiptx
{
    [DataContract]
    public class LineItem
    {
        //TODO: This should probably be a GUID?
        private static int _lineItemId;
        private string _gtin;
        private string _isbn;
        private bool _transactionalTradeItemSet = false;
        private bool _transactionalTradeDescriptionSet = false;
        private LineItemTradeInformation _transactionalTradeItem;
        private readonly List<TaxFee>  _taxes = new List<TaxFee>();
        private readonly List<ReceiptAllowanceCharge> _receiptAllowanceCharges = new List<ReceiptAllowanceCharge>();

        #region Constructors
        /// <summary> Required for XML serialization </summary>
        protected LineItem() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="quantity"></param>
        /// <param name="price"></param>
        /// <param name="total">TODO: What is Total?</param>
        public LineItem(long quantity, decimal price, decimal total)
        {
            Quantity = quantity;
            Price = price;
            Total = total;
            LineItemNumber = Interlocked.Increment(ref _lineItemId);
        }

        #endregion

        /// <summary>
        /// creditLineIndicator is an indicator that says if the lineitem is a return or exchange, this is important when we are added up the total as this would be deducted from the total
        /// </summary>
        [DataMember]
        public bool CreditLineIndicator { get; set; }

        [DataMember]
        public double AmountInclusiveAllowancesCharges { get; set; }

        /// <summary>
        /// This is required, as the merchant must state that there are no charges or taxes for the lineitem
        /// </summary>
        [DataMember]
        public double AmountExclusiveAllowancesCharges { get; set; }

        /// <summary>
        /// This is the price of the lineitem. So in this case it replaes price.
        /// </summary>
        [DataMember]
        public double ItemPriceExclusiveAllowancesCharges { get; set; }
        
        [DataMember]
        public int LineItemNumber { get; set; }
        [DataMember]
        public long Quantity { get; set; }

        [DataMember]
        public long InvoicedQuantity { get; set; }
        [DataMember]
        public decimal Price { get; set; }
        [DataMember]
        public decimal Total { get; set; }
        
        /// <summary>
        /// Gets and sets the Global Trade Item Number (<a href="ISBN">GTIN</a>).
        /// </summary>
        [DataMember]
        public string GTIN
        {
            get { return _gtin; }
            set
            {
                _gtin = value;
                _transactionalTradeItemSet = true;
            }
        }

        /// <summary>
        /// Gets and sets the International Standard Book Number (ISBN). An ISBN
        /// is a 13-digit number assigned by standard book numbering agencies 
        /// to control and facilitate activities within the publishing industry. 
        /// ISBNs used to be 10 digits until the end of 2006
        /// </summary>
        [DataMember]
        public string ISBN
        {
            get { return _isbn; }
            set
            {
                _isbn = value;
                _transactionalTradeItemSet = true;
                
            }
        }

        [DataMember]
        public LineItemTradeInformation TransactionalTradeItem
        {
            get { return _transactionalTradeItem; }
            set
            {
                _transactionalTradeItem = value;
                _transactionalTradeDescriptionSet = true;
                _transactionalTradeItemSet = true;
            }
        }

        public decimal UnAdjustedTotal
        {
            get { return Price*Total; }
        }

        public decimal TaxesTotal
        {
            get 
            { 
                return _taxes.Sum(x => x.TaxAmount) +
                _receiptAllowanceCharges.Sum(x=>x.TaxAmount); 
            }
        }

        public decimal AllowancesTotal
        {
            get { return _receiptAllowanceCharges.Sum(x => x.Amount); }
        }

        //TODO: Do not appear to set this value anywhere
        [XmlIgnore]
        public decimal ChargesTotal { get; private set; }

        public string Name
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(GTIN))
                {
                    return GTIN;
                }
                return TransactionalTradeItem.DescriptionShort;
            }
        }

        public void AddTaxFee(TaxFee taxFee)
        {
            _taxes.Add(taxFee);
        }

        public void AddMultibuyDiscount(decimal discountValue, string description, TaxFee tax)
        {
            AddDiscount(discountValue, description, "MB", tax);
        }

        public void AddGeneralDiscount(decimal discountValue, string description, TaxFee tax)
        {
            AddDiscount(discountValue, description, "DI", tax);
            
        }

        private void AddDiscount(decimal discountValue, string description, string type, TaxFee tax)
        {
            ReceiptAllowanceCharge receiptAllowanceCharge = new ReceiptAllowanceCharge(
                "ALLOWANCE",
                type, "CREDIT_CUSTOM_ACCOUNT", discountValue, description, tax);

            _receiptAllowanceCharges.Add(receiptAllowanceCharge);
        }

        public ReceiptValidation Validate()
        {
            ReceiptValidation receiptValidation = new ReceiptValidation();
            if (!_transactionalTradeItemSet)
            {
                receiptValidation.AddError("Transactional Trade Item must be set in object dRx.lineItem.");
            }
            return receiptValidation;
        }
    }


}
