namespace UniversityCafeteria.Models
{
    public class SupplyRequest
    {
        public int RequestID { get; set; }
        public int NumberOfProducts { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Fee { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; } = "";
    }
}