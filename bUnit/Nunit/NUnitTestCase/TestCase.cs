using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Blazor;
public abstract class TestBase : BunitContext
{
    protected TestBase()
    {
        Services.AddSyncfusionBlazor();
        Services.AddOptions();

        // Avoid JS interop errors
        JSInterop.Mode = JSRuntimeMode.Loose;
    }
}
