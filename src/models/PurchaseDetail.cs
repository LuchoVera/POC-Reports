namespace SalesStatistics.Models;
public class PurchaseDetail
{
    public int PurchaseId { get; set; }
    public required string Product { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Total { get; set; }
}
