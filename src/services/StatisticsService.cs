    public class StatisticsService
    {
        private readonly CsvReaderService _csvReader;

        public StatisticsService(CsvReaderService csvReader)
        {
            _csvReader = csvReader;
        }

        public (List<Purchase>, List<PurchaseDetail>) GetSalesForDateRange(DateTime startDate, DateTime endDate)
        {
            var purchases = _csvReader.ReadPurchases()
                .Where(p => p.Date >= startDate && p.Date <= endDate)
                .ToList();

            var purchaseDetails = _csvReader.ReadPurchaseDetails()
                .Where(pd => purchases.Any(p => p.PurchaseId == pd.PurchaseId))
                .ToList();

            return (purchases, purchaseDetails);
        }
    }
