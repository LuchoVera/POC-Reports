using SalesStatistics.Models;

namespace SalesStatistics.Services;
public class ReportService
{
    public void GenerateReport(string outputPath, List<Purchase> purchases, List<PurchaseDetail> details)
    {
        var lines = new List<string>
        {
            "Purchase ID;Date;Product;Quantity;Total"
        };


        foreach (var detail in details)
        {
            var purchase = purchases.First(p => p.PurchaseId == detail.PurchaseId);
            lines.Add($"{detail.PurchaseId};{purchase.Date:yyyy-MM-dd};{detail.Product};{detail.Quantity};{detail.Total:F2}");
        }

        Directory.CreateDirectory(Path.GetDirectoryName(outputPath) ?? string.Empty);
        File.WriteAllLines(outputPath, lines);
    }
}
