// wwwroot/js/payment.js
// Stripe Elements interop for Blazor Server.
// Loaded with: await JS.InvokeAsync<IJSObjectReference>("import", "/js/payment.js");

let stripe = null;
let elements = null;
let paymentElement = null;

// Lazy-load Stripe.js (https://js.stripe.com/v3) once per page load.
function loadStripeJs(publishableKey) {
    return new Promise((resolve, reject) => {
        if (window.Stripe) {
            resolve(window.Stripe(publishableKey));
            return;
        }
        const script = document.createElement("script");
        script.src = "https://js.stripe.com/v3/";
        script.onload = () => resolve(window.Stripe(publishableKey));
        script.onerror = () => reject(new Error("Failed to load Stripe.js"));
        document.head.appendChild(script);
    });
}

export async function mountPaymentElement(publishableKey, clientSecret, elementId) {
    if (!publishableKey || !clientSecret) {
        throw new Error("publishableKey and clientSecret are required");
    }
    stripe = await loadStripeJs(publishableKey);
    elements = stripe.elements({
        clientSecret,
        appearance: { theme: "stripe" }
    });
    paymentElement = elements.create("payment", { layout: "tabs" });
    const mountNode = document.getElementById(elementId);
    if (!mountNode) {
        throw new Error(`Element with id "${elementId}" not found`);
    }
    paymentElement.mount(mountNode);
    return { ok: true };
}

// Triggers Stripe's confirmation flow.
// Returns { ok: true, paymentIntent } OR { ok: false, error: "..." }.
export async function confirmPayment(returnUrl) {
    if (!stripe || !elements) {
        return { ok: false, error: "Stripe has not been initialized" };
    }
    const { error, paymentIntent } = await stripe.confirmPayment({
        elements,
        confirmParams: {
            return_url: returnUrl
        },
        redirect: "if_required"   // stay in-page for card payments, redirect for 3DS
    });

    if (error) {
        return { ok: false, error: error.message };
    }
    return { ok: true, paymentIntentId: paymentIntent.id, status: paymentIntent.status };
}

export function unmountPaymentElement() {
    if (paymentElement) {
        paymentElement.unmount();
        paymentElement = null;
    }
    elements = null;
    stripe = null;
}
