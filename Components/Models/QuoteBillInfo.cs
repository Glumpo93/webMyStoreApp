namespace webMyStoreApp.Components.Models
{
    public class QuoteBillInfo
    {
        public int Number { get; set; }
        public DateTime DateTime { get; set; }
        public string CurrencyCode { get; set; }
        public int CostumerCode { get; set; }
        public decimal SubtotalTaxed { get; set; }
        public decimal SubtotalExcemption { get; set; }
        public decimal SubtotalDiscount { get; set; }
        public int TransportType { get; set; }
        public decimal TransportCharge { get; set; }
        public decimal Taxes { get; set; }
        public decimal Total {  get; set; }
        public string Notes { get; set; }
    }
}
