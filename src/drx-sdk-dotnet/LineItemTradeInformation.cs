namespace Net.Dreceiptx
{
    public class LineItemTradeInformation
    {
        public string DescriptionShort { get; set; }
        public string TradeItemDescription { get; set; }
        public bool IsTradeItemAService { get; set; }
        public bool IsTradeItemReconditioned { get; set; }
        public string BrandName { get; set; }

        protected LineItemTradeInformation() { }

        public LineItemTradeInformation(string descriptionShort, string tradeItemDescription,
            bool isAService, bool isReconditioned, string brandName)
        {
            DescriptionShort = descriptionShort;
            TradeItemDescription = tradeItemDescription;
            IsTradeItemAService = isAService;
            IsTradeItemReconditioned = isReconditioned;
            BrandName = brandName;
        }
    }
}