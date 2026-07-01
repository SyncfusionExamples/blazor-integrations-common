using FluxorBlazorDataGrid.Models;

namespace FluxorBlazorDataGrid.Store.OrderState.Actions
{
    // Load Actions
    public record LoadOrdersAction;
    public record LoadOrdersSuccessAction(List<Order> Orders);
    public record LoadOrdersFailureAction(string ErrorMessage);

    // Add Actions
    public record AddOrderAction(Order Order);
    public record AddOrderSuccessAction(Order Order);
    public record AddOrderFailureAction(string ErrorMessage);

    // Update Actions
    public record UpdateOrderAction(Order Order);
    public record UpdateOrderSuccessAction(Order Order);
    public record UpdateOrderFailureAction(string ErrorMessage);

    // Delete Actions
    public record DeleteOrderAction(int OrderId);
    public record DeleteOrderSuccessAction(int OrderId);
    public record DeleteOrderFailureAction(string ErrorMessage);
}