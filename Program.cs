namespace SalesStatistics
{
    class Program
    {
        static void Main(string[] args)
        {
            var resourcePath = Path.Combine("src", "resources");
            var csvReader = new CsvReaderService(resourcePath);
            var statisticsService = new StatisticsService(csvReader);
            var reportService = new ReportService();
            var consoleUi = new ConsoleUiService(statisticsService, reportService);

            consoleUi.Run();
        }
    }
}