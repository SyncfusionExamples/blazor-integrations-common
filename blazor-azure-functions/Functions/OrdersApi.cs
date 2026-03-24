using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using System.Globalization;
using System.Net;
using Microsoft.AspNetCore.Mvc;

public static class OrdersApi
{
    [Function("GetOrders")]
    public static async Task<HttpResponseData> GetOrders(
        [HttpTrigger(AuthorizationLevel.Function, "get", "options", Route = "orders")] HttpRequestData req,
        FunctionContext ctx)
    {
        // Handle CORS preflight
        if (req.Method.Equals("OPTIONS", StringComparison.OrdinalIgnoreCase))
        {
            var preflight = req.CreateResponse(HttpStatusCode.NoContent);
            preflight.Headers.Add("Access-Control-Allow-Origin", "*");
            preflight.Headers.Add("Access-Control-Allow-Methods", "GET,POST,OPTIONS");
            preflight.Headers.Add("Access-Control-Allow-Headers", "Content-Type,Authorization");
            return preflight;
        }

        var baseDate = DateTime.UtcNow.Date;

        // Generate demo orders
        var allOrders = Enumerable.Range(1, 10).Select(i => new OrderDto
        {
            Id = i,
            Date = baseDate.AddDays(-(i % 7)),
            Customer = i % 3 == 0 ? "Contoso" : i % 2 == 0 ? "Tailspin" : "ACME",
            Total = Math.Round(20 + i * 15.75, 2)
        }).ToArray();

        // Parse optional query parameters (expected yyyy-MM-dd)
        DateTime? from = null; DateTime? to = null;
        try
        {
            var query = (req.Url.Query ?? string.Empty).TrimStart('?');
            if (!string.IsNullOrEmpty(query))
            {
                var pairs = query.Split('&', StringSplitOptions.RemoveEmptyEntries);
                foreach (var p in pairs)
                {
                    var kv = p.Split('=', 2);
                    if (kv.Length == 0) continue;
                    var key = WebUtility.UrlDecode(kv[0]).Trim();
                    var val = kv.Length > 1 ? WebUtility.UrlDecode(kv[1]).Trim() : string.Empty;
                    if (string.Equals(key, "from", StringComparison.OrdinalIgnoreCase) && DateTime.TryParse(val, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var f))
                        from = f.Date;
                    if (string.Equals(key, "to", StringComparison.OrdinalIgnoreCase) && DateTime.TryParse(val, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var t))
                        to = t.Date;
                }
            }
        }
        catch
        {
            // ignore parse errors and return unfiltered results
        }

        var orders = allOrders.Where(o =>
            (!from.HasValue || o.Date.Date >= from.Value) &&
            (!to.HasValue || o.Date.Date <= to.Value)).ToArray();

        try
        {
            var logger = ctx.GetLogger("GetOrders");
            logger.LogInformation($"Returning {orders.Length} orders (from={from?.ToString("yyyy-MM-dd") ?? ""}, to={to?.ToString("yyyy-MM-dd") ?? ""})");
        }
        catch
        {
            // ignore logging failures
        }

        var resp = req.CreateResponse(HttpStatusCode.OK);
        resp.Headers.Add("Content-Type", "application/json");
        resp.Headers.Add("Access-Control-Allow-Origin", "*");
        await resp.WriteStringAsync(JsonSerializer.Serialize(orders));
        return resp;
    }

    [Function("PostOrder")]
    public static async Task<HttpResponseData> PostOrder(
        [HttpTrigger(AuthorizationLevel.Function, "post", "options", Route = "orders/add")] HttpRequestData req,
        FunctionContext ctx)
    {
        if (req.Method.Equals("OPTIONS", StringComparison.OrdinalIgnoreCase))
        {
            var preflight = req.CreateResponse(HttpStatusCode.NoContent);
            preflight.Headers.Add("Access-Control-Allow-Origin", "*");
            preflight.Headers.Add("Access-Control-Allow-Methods", "GET,POST,OPTIONS");
            preflight.Headers.Add("Access-Control-Allow-Headers", "Content-Type,Authorization");
            return preflight;
        }

        var body = await new StreamReader(req.Body).ReadToEndAsync();
        object? order = null;
        try
        {
            order = JsonSerializer.Deserialize<object>(body);
        }
        catch
        {
            // keep behavior: if deserialization fails, return the raw body as string
            order = body;
        }

        try
        {
            var logger = ctx.GetLogger("PostOrder");
            logger.LogInformation("Received PostOrder request");
        }
        catch
        {
        }

        var resp = req.CreateResponse(HttpStatusCode.Created);
        resp.Headers.Add("Access-Control-Allow-Origin", "http://localhost:5298");
        await resp.WriteStringAsync(JsonSerializer.Serialize(order));
        return resp;
    }

    private sealed class OrderDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string? Customer { get; set; }
        public double Total { get; set; }
    }
}
