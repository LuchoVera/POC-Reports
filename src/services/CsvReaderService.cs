using SalesStatistics.Models;

namespace SalesStatistics.Services;
public class CsvReaderService
{
    private readonly string _resourcePath;

    public CsvReaderService(string resourcePath)
    {
        _resourcePath = resourcePath;
    }

    public List<Purchase> ReadPurchases()
    {
        var purchases = new List<Purchase>();
        var lines = File.ReadAllLines(Path.Combine(_resourcePath, "compras.csv")).Skip(1);

        foreach (var line in lines)
        {
            var values = line.Split(',');
            purchases.Add(new Purchase
            {
                PurchaseId = int.Parse(values[0]),
                UserId = int.Parse(values[1]),
                Date = DateTime.Parse(values[2])
            });
        }

        return purchases;
    }

    public List<PurchaseDetail> ReadPurchaseDetails()
    {
        var details = new List<PurchaseDetail>();
        var lines = File.ReadAllLines(Path.Combine(_resourcePath, "detalle_compras.csv")).Skip(1);

        foreach (var line in lines)
        {
            var values = line.Split(',');
            details.Add(new PurchaseDetail
            {
                PurchaseId = int.Parse(values[0]),
                Product = values[1],
                Quantity = int.Parse(values[2]),
                UnitPrice = decimal.Parse(values[3]),
                Total = decimal.Parse(values[4])
            });
        }

        return details;
    }
}
