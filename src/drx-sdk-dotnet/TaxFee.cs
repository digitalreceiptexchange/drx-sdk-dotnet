namespace Net.Dreceiptx
{
    public class TaxFee
    {
        protected TaxFee()
        {
        }

        public TaxFee(decimal taxAmount)
        {
            TaxAmount = taxAmount;
        }

        public decimal TaxAmount { get; set; }

        public override bool Equals(object obj)
        {
            TaxFee taxInfo = (TaxFee)obj;
            return Equals(taxInfo);
        }

        public bool Equals(TaxFee taxFee)
        {
            if (taxFee == null)
            {
                return false;
            }
            bool equals = TaxAmount.Equals(taxFee.TaxAmount);
            return equals;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = (13 * hash) + TaxAmount.GetHashCode();
                return hash;
            }

        }
    }
}