namespace webMyStoreApp.Components.Models
{
    public class QuoteBillLine
    {
        public int Id { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public decimal ItemQuantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal ItemDiscount { get; set; }
        public string ItemNotes { get; set; }
        public QuoteBillLine() 
        { 
        
        }
    }
}
