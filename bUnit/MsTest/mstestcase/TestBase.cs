using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Blazor;

public abstract class TestBase : BunitContext
{
    protected TestBase()
    {
        Services.AddSyncfusionBlazor();
        Services.AddOptions();

        // ✅ Prevent JS errors
        JSInterop.Mode = JSRuntimeMode.Loose;
    }
}