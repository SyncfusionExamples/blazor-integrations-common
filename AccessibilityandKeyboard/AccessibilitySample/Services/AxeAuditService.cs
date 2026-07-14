using AccessibilitySample.Models;
using Microsoft.JSInterop;

namespace AccessibilitySample.Services;

public class AxeAuditService
{
    // Generous timeout for axe-core to scan a fully populated Syncfusion grid.
    private static readonly TimeSpan AuditTimeout = TimeSpan.FromSeconds(120);

    private readonly IJSRuntime _jsRuntime;

    public AxeAuditService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task<AxeResults?> RunAuditAsync(string selector)
    {
        try
        {
            return await _jsRuntime.InvokeAsync<AxeResults?>(
                "axeAudit.run",
                AuditTimeout,
                selector);
        }
        catch (JSException jsEx)
        {
            // axe-core failed to run (not loaded or selector not found).
            Console.Error.WriteLine($"axe-core audit failed: {jsEx.Message}");
            return null;
        }
        catch (JSDisconnectedException)
        {
            // SignalR circuit dropped mid-call, the caller can prompt a retry
            return null;
        }
    }
}