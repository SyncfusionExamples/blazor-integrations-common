using ShoppingCart.Models;

namespace ShoppingCart.Services
{
    public interface IStripePaymentService
    {
        /// <summary>
        /// Creates (or reuses) a Stripe PaymentIntent for the order and returns the
        /// client secret used by Stripe.js on the client to confirm the payment.
        /// </summary>
        Task<PaymentIntentResult> CreateOrUpdatePaymentIntentAsync(Order order);

        /// <summary>
        /// Verifies the signature on a Stripe webhook payload and returns the parsed
        /// Stripe Event object. Throws if the signature is invalid.
        /// </summary>
        Stripe.Event? ConstructWebhookEvent(string json, string stripeSignature);

        /// <summary>
        /// Look up an order by the PaymentIntentId stored on it (used by webhook handler).
        /// </summary>
        Order? GetOrderByPaymentIntentId(string paymentIntentId);
    }

    public record PaymentIntentResult(string PaymentIntentId, string ClientSecret, string Status);
}
