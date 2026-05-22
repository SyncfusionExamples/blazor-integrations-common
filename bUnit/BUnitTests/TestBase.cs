using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Blazor;

public abstract class TestBase : TestContext
{
    protected TestBase()
    {
        Services.AddSyncfusionBlazor();
        Services.AddOptions();
        
        // ✅ OR (best approach) - allow ANY JS call
        JSInterop.Mode = JSRuntimeMode.Loose;

    }
}
