using Microsoft.Extensions.AI;
using Syncfusion.Blazor.AI;
using Syncfusion.Blazor.Grids;
using System.Text.Json;

namespace DataGridAI.Components.Pages;

public partial class Home
{
    private List<SalesOrder> orders = new();
    private AiInsights? insight;
    private bool isLoading;
    private string errorMessage = string.Empty;
    private HashSet<string> highlightedOrderIds = new(StringComparer.OrdinalIgnoreCase);
    private SfGrid<SalesOrder>? GridRef;

    protected override void OnInitialized()
    {
        orders = new List<SalesOrder>
        {
            new() { OrderId = "SO-1001", Customer = "Northwind", Region = "West", Category = "Bikes", Sales = 12000, Profit = 3200, OrderDate = new DateTime(2026, 1, 5) },
            new() { OrderId = "SO-1002", Customer = "Contoso", Region = "East", Category = "Accessories", Sales = 3400, Profit = 980, OrderDate = new DateTime(2026, 1, 8) },
            new() { OrderId = "SO-1003", Customer = "Adventure Works", Region = "West", Category = "Clothing", Sales = 8600, Profit = 1400, OrderDate = new DateTime(2026, 1, 11) },
            new() { OrderId = "SO-1004", Customer = "Fabrikam", Region = "South", Category = "Bikes", Sales = 1500, Profit = -220, OrderDate = new DateTime(2026, 1, 13) },
            new() { OrderId = "SO-1005", Customer = "Northwind", Region = "North", Category = "Electronics", Sales = 24200, Profit = 6700, OrderDate = new DateTime(2026, 1, 15) },
            new() { OrderId = "SO-1006", Customer = "Contoso", Region = "East", Category = "Clothing", Sales = 1900, Profit = 120, OrderDate = new DateTime(2026, 1, 18) },
            new() { OrderId = "SO-1007", Customer = "Adventure Works", Region = "West", Category = "Accessories", Sales = 5400, Profit = 1650, OrderDate = new DateTime(2026, 1, 22) },
            new() { OrderId = "SO-1008", Customer = "Fabrikam", Region = "South", Category = "Electronics", Sales = 7800, Profit = 400, OrderDate = new DateTime(2026, 1, 24) },
            new() { OrderId = "SO-1009", Customer = "Northwind", Region = "Central", Category = "Bikes", Sales = 18900, Profit = 5100, OrderDate = new DateTime(2026, 1, 27) },
            new() { OrderId = "SO-1010", Customer = "Contoso", Region = "North", Category = "Accessories", Sales = 2200, Profit = 180, OrderDate = new DateTime(2026, 1, 29) },
            new() { OrderId = "SO-1011", Customer = "Adventure Works", Region = "South", Category = "Clothing", Sales = 4600, Profit = 760, OrderDate = new DateTime(2026, 2, 2) },
            new() { OrderId = "SO-1012", Customer = "Fabrikam", Region = "West", Category = "Electronics", Sales = 30500, Profit = 9200, OrderDate = new DateTime(2026, 2, 5) }
        };
    }

    private async Task GenerateInsightsAsync()
    {
        isLoading = true;
        errorMessage = string.Empty;
        insight = null;
        highlightedOrderIds.Clear();

        try
        {
            string payload = JsonSerializer.Serialize(orders);

            string prompt =
                "Analyze the following sales orders and return JSON only in this exact schema:\n\n" +
                "{\n" +
                "  \"summary\": \"string\",\n" +
                "  \"keyTrends\": [\"string\"],\n" +
                "  \"recommendations\": [\"string\"],\n" +
                "  \"flaggedOrderIds\": [\"SO-1001\"]\n" +
                "}\n\n" +
                "Writing requirements:\n" +
                "- Use clear, professional, customer-friendly language that is easy to understand.\n" +
                "- Avoid vague wording such as 'varied performance' or 'some trends'.\n" +
                "- Avoid confusing shorthand or bracketed value pairs like 'SO-1012 ($30,500, $9,200 profit)'.\n" +
                "- When referencing an order, always write it in a readable format like:\n" +
                "  'Order SO-1012: Sales $30,500 and profit $9,200.'\n" +
                "- If profit is negative, write it clearly as a loss, for example:\n" +
                "  'Order SO-1004: Sales $1,500 and a loss of $220.'\n" +
                "- The summary must clearly state the top-performing category, the weakest category, and any profit concerns.\n" +
                "- keyTrends must contain 3 to 5 specific, easy-to-read bullets.\n" +
                "- Each key trend must explain what the numbers mean in plain language and must reference exact categories, regions, customers, or order IDs.\n" +
                "- recommendations must contain 3 to 5 specific, actionable bullets.\n" +
                "- Each recommendation must be practical, specific, and tied to a category, region, customer, or order ID.\n" +
                "- flaggedOrderIds is MANDATORY and must ALWAYS be included.\n" +
                "- flaggedOrderIds must contain ALL Order IDs with negative profit or clearly low profit.\n" +
                "- flaggedOrderIds values MUST exactly match the OrderId values from the data (for example: \"SO-1004\").\n" +
                "- If no orders are negative or low-profit, return flaggedOrderIds as an empty array: [].\n" +
                "- Never omit flaggedOrderIds.\n" +
                "- Include sales and profit amounts where relevant, but always explain them clearly.\n" +
                "- Keep the content concise, polished, and suitable for business review.\n" +
                "- Return JSON only. Do not include markdown, code fences, or any extra text.\n\n" +
                "Data:\n" +
                payload;

            ChatParameters parameters = new()
            {
                Messages =
                [
                    new ChatMessage(ChatRole.User, prompt)
                ]
            };

            string result = await AIService.GenerateResponseAsync(parameters);
            result = CleanJson(result);

            insight = JsonSerializer.Deserialize<AiInsights>(
                result,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (insight is not null)
            {
                highlightedOrderIds = new HashSet<string>(insight.FlaggedOrderIds, StringComparer.OrdinalIgnoreCase);
                if (GridRef is not null)
                {
                    await GridRef.Refresh();
                }
            }
        }
        catch (Exception ex)
        {
            errorMessage = $"AI insight generation failed: {ex.Message}";
        }
        finally
        {
            isLoading = false;
            await InvokeAsync(StateHasChanged);
        }
    }

    private void OnQueryCellInfo(QueryCellInfoEventArgs<SalesOrder> args)
    {
        if (highlightedOrderIds.Contains(args.Data.OrderId))
        {
            args.Cell.AddClass(new[] { "ai-highlight-cell" });
        }
    }

    private static string CleanJson(string value)
    {
        return value.Replace("```json", string.Empty, StringComparison.OrdinalIgnoreCase)
                    .Replace("```", string.Empty, StringComparison.OrdinalIgnoreCase)
                    .Trim();
    }

    public class SalesOrder
    {
        public string OrderId { get; set; } = string.Empty;
        public string Customer { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public decimal Sales { get; set; }
        public decimal Profit { get; set; }
        public DateTime OrderDate { get; set; }
    }

    public class AiInsights
    {
        public string Summary { get; set; } = string.Empty;
        public List<string> KeyTrends { get; set; } = new();
        public List<string> Recommendations { get; set; } = new();
        public List<string> FlaggedOrderIds { get; set; } = new();
    }
}