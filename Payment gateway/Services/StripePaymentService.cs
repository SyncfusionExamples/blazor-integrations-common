using Microsoft.Extensions.Options;
using ShoppingCart.Models;
using Stripe;

namespace ShoppingCart.Services
{
    public class StripePaymentService : IStripePaymentService
    {
        private readonly StripeOptions _options;
        private readonly IOrderService _orderService;

        public StripePaymentService(IOptions<StripeOptions> options, IOrderService orderService)
        {
            _options = options.Value;
            _orderService = orderService;

            // Set the global API key for the Stripe.net SDK once.
            StripeConfiguration.ApiKey = _options.SecretKey;
        }

        public async Task<PaymentIntentResult> CreateOrUpdatePaymentIntentAsync(Order order)
        {
            if (order == null) throw new ArgumentNullException(nameof(order));

            var amountInCents = (long)Math.Round(order.TotalAmount * 100m, MidpointRounding.AwayFromZero);
            var service = new PaymentIntentService();

            if (!string.IsNullOrWhiteSpace(order.PaymentIntentId))
            {
                // Update existing PaymentIntent
                var updateOptions = new PaymentIntentUpdateOptions
                {
                    Amount = amountInCents,
                    Currency = order.Currency,
                    Description = $"Order #{order.OrderId} for {order.Shipping.FullName}"
                };
                var intent = await service.UpdateAsync(order.PaymentIntentId, updateOptions);
                order.PaymentStatus = intent.Status;
                await _orderService.UpdateAsync(order);
                return new PaymentIntentResult(intent.Id, intent.ClientSecret, intent.Status);
            }
            else
            {
                // Create new PaymentIntent
                var createOptions = new PaymentIntentCreateOptions
                {
                    Amount = amountInCents,
                    Currency = order.Currency,
                    AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
                    {
                        Enabled = true
                    },
                    Metadata = new Dictionary<string, string>
                    {
                        { "order_id", order.OrderId.ToString() },
                        { "customer_name", order.Shipping.FullName },
                        { "customer_email", order.Shipping.Email ?? "" }
                    },
                    Description = $"Order #{order.OrderId} for {order.Shipping.FullName}"
                };

                var intent = await service.CreateAsync(createOptions);

                order.PaymentIntentId = intent.Id;
                order.PaymentStatus = intent.Status;
                await _orderService.UpdateAsync(order);

                return new PaymentIntentResult(intent.Id, intent.ClientSecret!, intent.Status);
            }
        }

        public Stripe.Event? ConstructWebhookEvent(string json, string stripeSignature)
        {
            if (string.IsNullOrWhiteSpace(_options.WebhookSecret))
            {
                // Without a webhook secret we can't safely verify. Return null so caller
                // can decide (e.g. log + skip in development).
                return null;
            }

            try
            {
                return Stripe.EventUtility.ConstructEvent(
                    json,
                    stripeSignature,
                    _options.WebhookSecret,
                    tolerance: 300);
            }
            catch (StripeException)
            {
                return null;
            }
        }

        public Order? GetOrderByPaymentIntentId(string paymentIntentId)
        {
            var orders = _orderService.GetOrdersAsync().GetAwaiter().GetResult();
            return orders.FirstOrDefault(o => o.PaymentIntentId == paymentIntentId);
        }
    }
}
