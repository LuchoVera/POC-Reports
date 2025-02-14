using SalesStatistics.Services;
using Spectre.Console;

namespace SalesStatistics.UI;
public class ConsoleUiService
{
    private readonly StatisticsService _statisticsService;
    private readonly ReportService _reportService;

    public ConsoleUiService(StatisticsService statisticsService, ReportService reportService)
    {
        _statisticsService = statisticsService;
        _reportService = reportService;
    }

    public void Run()
    {
        AnsiConsole.Write(new FigletText("Sales Statistics").Centered().Color(Color.Blue));

        var startDate = AnsiConsole.Prompt(
            new TextPrompt<DateTime>("Enter start date (yyyy-MM-dd):")
                .ValidationErrorMessage("[red]Please enter a valid date[/]"));

        var endDate = AnsiConsole.Prompt(
            new TextPrompt<DateTime>("Enter end date (yyyy-MM-dd):")
                .ValidationErrorMessage("[red]Please enter a valid date[/]"));

        AnsiConsole.Status()
            .Start("Processing sales data...", ctx =>
            {
                var (purchases, details) = _statisticsService.GetSalesForDateRange(startDate, endDate);

                var table = new Table()
                    .AddColumn("Metric")
                    .AddColumn("Value");

                table.AddRow("Total Sales", details.Sum(d => d.Total).ToString("F2"));
                table.AddRow("Total Products", details.Sum(d => d.Quantity).ToString());
                table.AddRow("Unique Products", details.Select(d => d.Product).Distinct().Count().ToString());

                AnsiConsole.Write(table);

                var outputPath = Path.Combine("reports", $"sales_report_{DateTime.Now:yyyyMMddHHmmss}.csv");
                _reportService.GenerateReport(outputPath, purchases, details);

                AnsiConsole.MarkupLine($"[green]Report generated: {outputPath}[/]");
            });
    }
}
