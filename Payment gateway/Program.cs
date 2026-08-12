using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using ShoppingCart.Components;
using ShoppingCart.Models;
using ShoppingCart.Services;
using Stripe;
using Syncfusion.Blazor;
using ProductService = ShoppingCart.Services.ProductService;

var builder = WebApplication.CreateBuilder(args);

// ---------- Configuration ----------
builder.Services.Configure<StripeOptions>(
    builder.Configuration.GetSection(StripeOptions.SectionName));

// ---------- Services ----------
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddSyncfusionBlazor();

builder.Services.AddSingleton<ICartService, CartService>();  // Singleton to persist cart across requests
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddSingleton<IOrderService, OrderService>();  // Singleton to persist orders across requests
builder.Services.AddScoped<IWishlistService, WishlistService>();
builder.Services.AddScoped<IStripePaymentService, StripePaymentService>();

// ---------- Syncfusion license ----------
Syncfusion.Licensing.SyncfusionLicenseProvider
    .RegisterLicense("Ngo9BigBOggjHTQxAR8/V1JAaF1cX2hBYVF1WmFZfVhgdV9DaFZVQmYuP1ZhSXxVdk1jX39ecXBQQmNbUEx9XEY=");

var app = builder.Build();

// ---------- Pipeline ----------
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// ---------- Stripe webhook ----------
// IMPORTANT: this must be registered BEFORE any auth middleware that buffers the body.
// Stripe requires the raw request body to validate the signature.
app.MapPost("/api/stripe/webhook", async (HttpRequest request,
                                         IStripePaymentService stripeService,
                                         IOrderService orderService,
                                         ILogger<Program> logger) =>
{
    // Read raw JSON
    request.EnableBuffering();
    using var reader = new StreamReader(request.Body, leaveOpen: true);
    var json = await reader.ReadToEndAsync();
    request.Body.Position = 0;

    var signature = request.Headers["Stripe-Signature"].ToString();
    var stripeEvent = stripeService.ConstructWebhookEvent(json, signature);

    if (stripeEvent == null)
    {
        logger.LogWarning("Stripe webhook signature verification failed.");
        return Results.BadRequest(new { error = "Invalid signature" });
    }

    switch (stripeEvent.Type)
    {
        case "payment_intent.succeeded":
        {
            var intent = stripeEvent.Data.Object as PaymentIntent;
            if (intent != null)
            {
                var order = stripeService.GetOrderByPaymentIntentId(intent.Id);
                if (order != null)
                {
                    order.PaymentStatus = intent.Status;
                    order.Status = "Paid";
                    order.PaymentMethodId = intent.PaymentMethodId;
                    await orderService.UpdateAsync(order);
                    logger.LogInformation("Order {OrderId} marked as Paid (PaymentIntent {IntentId})",
                        order.OrderId, intent.Id);
                }
            }
            break;
        }
        case "payment_intent.payment_failed":
        {
            var intent = stripeEvent.Data.Object as PaymentIntent;
            if (intent != null)
            {
                var order = stripeService.GetOrderByPaymentIntentId(intent.Id);
                if (order != null)
                {
                    order.PaymentStatus = intent.Status;
                    order.Status = "Payment Failed";
                    await orderService.UpdateAsync(order);
                }
            }
            break;
        }
        case "charge.refunded":
        {
            var charge = stripeEvent.Data.Object as Charge;
            if (charge?.PaymentIntentId != null)
            {
                var order = stripeService.GetOrderByPaymentIntentId(charge.PaymentIntentId);
                if (order != null)
                {
                    order.Status = "Refunded";
                    await orderService.UpdateAsync(order);
                }
            }
            break;
        }
        default:
            logger.LogInformation("Unhandled Stripe event type: {Type}", stripeEvent.Type);
            break;
    }

    return Results.Ok();
});

app.Run();
