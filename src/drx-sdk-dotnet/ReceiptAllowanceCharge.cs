namespace Net.Dreceiptx
{
    public class ReceiptAllowanceCharge
    {
        protected ReceiptAllowanceCharge()
        { }

        public ReceiptAllowanceCharge(string allowanceOrCharge, string type,
            string settlement, decimal amount, string description,
            TaxFee tax)
        {
            AllowanceOrCharge = allowanceOrCharge;
            Type = type;
            Settlement = settlement;
            Amount = amount;
            Description = description;
            Tax = tax;
        }

        public string AllowanceOrCharge { get; set; }
        public string Type { get; set; }
        public string Settlement { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public TaxFee Tax { get; set; }
        public int ReceiptAllowanceChargeId { get; set; }

        public decimal TaxAmount
        {
            get { return Tax?.TaxAmount ?? 0; }
        }

        protected bool Equals(ReceiptAllowanceCharge other)
        {
            return string.Equals(AllowanceOrCharge, other.AllowanceOrCharge)
                && string.Equals(Type, other.Type)
                && string.Equals(Settlement, other.Settlement)
                && Amount == other.Amount
                && string.Equals(Description, other.Description)
                && Equals(Tax, other.Tax)
                && ReceiptAllowanceChargeId == other.ReceiptAllowanceChargeId;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((ReceiptAllowanceCharge)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = AllowanceOrCharge?.GetHashCode() ?? 0;
                hashCode     = (hashCode * 397) ^ (Type?.GetHashCode() ?? 0);
                hashCode     = (hashCode * 397) ^ (Settlement?.GetHashCode() ?? 0);
                hashCode     = (hashCode * 397) ^ Amount.GetHashCode();
                hashCode     = (hashCode * 397) ^ (Description?.GetHashCode() ?? 0);
                hashCode     = (hashCode * 397) ^ (Tax?.GetHashCode() ?? 0);
                hashCode     = (hashCode * 397) ^ ReceiptAllowanceChargeId;
                return hashCode;
            }
        }

    }
}