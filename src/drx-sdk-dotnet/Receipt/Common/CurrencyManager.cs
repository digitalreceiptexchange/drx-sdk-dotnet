namespace Net.Dreceiptx.Receipt.Common
{
    public class CurrencyManager
    {
        private static readonly EnumExtensions.DrxEnumExtendedInformationHelper<Currency> Converter = new EnumExtensions.DrxEnumExtendedInformationHelper<Currency>();
        public static Currency GetCurrency(string code)
        {
            return Converter.GetByValue(code);
        }
    }
}