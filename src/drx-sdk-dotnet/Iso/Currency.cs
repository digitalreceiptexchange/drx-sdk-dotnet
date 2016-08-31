namespace Net.Dreceiptx.Iso
{
    public class Currency
    {
        public Currency(string entity, string name, string alphabeticCode,
            string numericCode, int minorUnit)
        {
            Entity = entity;
            Name = name;
            AlphabeticCode = alphabeticCode;
            NumericCode = numericCode;
            MinorUnit = minorUnit;
        }

        public string Entity { get; set; }
        public string Name { get; set; }
        public string AlphabeticCode { get; set; }
        public string NumericCode { get; set; }
        public int MinorUnit { get; set; }
    }
}
