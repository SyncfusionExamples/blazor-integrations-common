using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;

namespace MySecureBlazorApp.Controllers;

[ApiController]
[Route("api/proxy/[action]")]
public class ProxyController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;
    private readonly ILogger<ProxyController> _logger;

    public ProxyController(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<ProxyController> logger)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Orders()
    {
        var external = "https://blazor.syncfusion.com/services/production/api/Orders/";
        return await ForwardGetRequest(external);
    }

    [HttpGet]
    public async Task<IActionResult> Schedule()
    {
        var external = "https://blazor.syncfusion.com/services/production/api/schedule";
        return await ForwardGetRequest(external);
    }

    private async Task<IActionResult> ForwardGetRequest(string url)
    {
        var client = _httpClientFactory.CreateClient();

        // If you have a bearer token in configuration, attach it.
        var token = _configuration["ExternalApi:BearerToken"]; // optional
        if (!string.IsNullOrEmpty(token))
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        try
        {
            using var resp = await client.GetAsync(url);
            var content = await resp.Content.ReadAsStringAsync();
            _logger.LogInformation("Proxy GET {Url} -> {Status} {ContentType} {Prefix}", url, resp.StatusCode, resp.Content.Headers.ContentType?.ToString(), content?.Length > 0 ? content.Substring(0, Math.Min(64, content.Length)).Replace("\n","\\n") : "(empty)");

            // If the external API wraps the array in a top-level object (e.g. { "value": [...], "Count": n }),
            // extract the inner array so Syncfusion adapters that expect an array can parse it.
            try
            {
                var trimmed = (content ?? string.Empty).TrimStart();
                if (trimmed.StartsWith("{") && trimmed.Contains("\"value\""))
                {
                    using var doc = System.Text.Json.JsonDocument.Parse(content);
                    if (doc.RootElement.TryGetProperty("value", out var valueElem))
                    {
                        var arrJson = valueElem.GetRawText();
                        _logger.LogInformation("Extracted 'value' array from wrapper (length {Len})", arrJson.Length);
                        return new ContentResult { Content = arrJson, ContentType = "application/json", StatusCode = (int)resp.StatusCode };
                    }
                }
            }
            catch (System.Text.Json.JsonException je)
            {
                _logger.LogWarning(je, "Failed to parse wrapper JSON from external response");
            }

            return new ContentResult
            {
                Content = content,
                ContentType = resp.Content.Headers.ContentType?.ToString() ?? "application/json",
                StatusCode = (int)resp.StatusCode
            };
        }
        catch (Exception ex)
        {
            return Problem(detail: ex.Message);
        }
    }

    // Support POST/PUT/DELETE to the Orders endpoint at the same proxy path (/api/proxy/Orders)
    [HttpPost]
    [ActionName("Orders")]
    public async Task<IActionResult> OrdersPost()
    {
        var external = _configuration["ExternalApi:OrdersUrl"] ?? "https://blazor.syncfusion.com/services/production/api/Orders/";
        external += HttpContext.Request.QueryString.Value;
        return await ForwardBodyRequest(HttpMethod.Post, external);
    }

    [HttpPut]
    [ActionName("Orders")]
    public async Task<IActionResult> OrdersPut()
    {
        var external = _configuration["ExternalApi:OrdersUrl"] ?? "https://blazor.syncfusion.com/services/production/api/Orders/";
        external += HttpContext.Request.QueryString.Value;
        return await ForwardBodyRequest(HttpMethod.Put, external);
    }

    [HttpDelete]
    [ActionName("Orders")]
    public async Task<IActionResult> OrdersDelete()
    {
        var external = _configuration["ExternalApi:OrdersUrl"] ?? "https://blazor.syncfusion.com/services/production/api/Orders/";
        external += HttpContext.Request.QueryString.Value;
        var client = _httpClientFactory.CreateClient();
        var token = _configuration["ExternalApi:BearerToken"];
        if (!string.IsNullOrEmpty(token)) client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        try
        {
            using var resp = await client.DeleteAsync(external);
            var content = await resp.Content.ReadAsStringAsync();
            return new ContentResult { Content = content, ContentType = resp.Content.Headers.ContentType?.ToString() ?? "application/json", StatusCode = (int)resp.StatusCode };
        }
        catch (Exception ex)
        {
            return Problem(detail: ex.Message);
        }
    }

    // Support POST/PUT/DELETE to the Schedule endpoint at /api/proxy/Schedule
    [HttpPost]
    [ActionName("Schedule")]
    public async Task<IActionResult> SchedulePost()
    {
        var external = _configuration["ExternalApi:ScheduleUrl"] ?? "https://blazor.syncfusion.com/services/production/api/schedule";
        external += HttpContext.Request.QueryString.Value;
        return await ForwardBodyRequest(HttpMethod.Post, external);
    }

    [HttpPut]
    [ActionName("Schedule")]
    public async Task<IActionResult> SchedulePut()
    {
        var external = _configuration["ExternalApi:ScheduleUrl"] ?? "https://blazor.syncfusion.com/services/production/api/schedule";
        external += HttpContext.Request.QueryString.Value;
        return await ForwardBodyRequest(HttpMethod.Put, external);
    }

    [HttpDelete]
    [ActionName("Schedule")]
    public async Task<IActionResult> ScheduleDelete()
    {
        var external = _configuration["ExternalApi:ScheduleUrl"] ?? "https://blazor.syncfusion.com/services/production/api/schedule";
        external += HttpContext.Request.QueryString.Value;
        var client = _httpClientFactory.CreateClient();
        var token = _configuration["ExternalApi:BearerToken"];
        if (!string.IsNullOrEmpty(token)) client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        try
        {
            using var resp = await client.DeleteAsync(external);
            var content = await resp.Content.ReadAsStringAsync();
            return new ContentResult { Content = content, ContentType = resp.Content.Headers.ContentType?.ToString() ?? "application/json", StatusCode = (int)resp.StatusCode };
        }
        catch (Exception ex)
        {
            return Problem(detail: ex.Message);
        }
    }

    private async Task<IActionResult> ForwardBodyRequest(HttpMethod method, string target)
    {
        var client = _httpClientFactory.CreateClient();
        var token = _configuration["ExternalApi:BearerToken"];
        if (!string.IsNullOrEmpty(token)) client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        try
        {
            // Read the incoming request body so we can log and forward it reliably
            HttpContext.Request.EnableBuffering();
            using var reader = new StreamReader(HttpContext.Request.Body, leaveOpen: true);
            var bodyText = await reader.ReadToEndAsync();
            HttpContext.Request.Body.Position = 0;
            _logger.LogInformation("Incoming {Method} {Target} body prefix: {Prefix}", method, target, string.IsNullOrEmpty(bodyText) ? "(empty)" : bodyText.Substring(0, Math.Min(256, bodyText.Length)).Replace("\n","\\n"));

            using var request = new HttpRequestMessage(method, target)
            {
                Content = new StringContent(bodyText ?? string.Empty, System.Text.Encoding.UTF8, HttpContext.Request.ContentType ?? "application/json")
            };

            using var resp = await client.SendAsync(request);
            var content = await resp.Content.ReadAsStringAsync();
            _logger.LogInformation("Proxy {Method} {Target} -> {Status} {ContentType} {Prefix}", method, target, resp.StatusCode, resp.Content.Headers.ContentType?.ToString(), content?.Length > 0 ? content.Substring(0, Math.Min(64, content.Length)).Replace("\n","\\n") : "(empty)");

            // Some external endpoints return an empty body for POST; in that case fall back to GET
            if (string.IsNullOrWhiteSpace(content))
            {
                _logger.LogInformation("Empty response from POST to {Target}; performing fallback GET", target);
                using var getResp = await client.GetAsync(target);
                var getContent = await getResp.Content.ReadAsStringAsync();
                return new ContentResult { Content = getContent, ContentType = getResp.Content.Headers.ContentType?.ToString() ?? "application/json", StatusCode = (int)getResp.StatusCode };
            }

            return new ContentResult { Content = content, ContentType = resp.Content.Headers.ContentType?.ToString() ?? "application/json", StatusCode = (int)resp.StatusCode };
        }
        catch (Exception ex)
        {
            return Problem(detail: ex.Message);
        }
    }
}
