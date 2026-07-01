using Fluxor;
using FluxorBlazorDataGrid.Services;
using FluxorBlazorDataGrid.Store.OrderState.Actions;

namespace FluxorBlazorDataGrid.Store.OrderState.Effects
{
    public class OrderEffects
    {
        private readonly OrderService _orderService;

        public OrderEffects(OrderService orderService)
        {
            _orderService = orderService;
        }

        [EffectMethod]
        public async Task HandleLoadOrdersAction(LoadOrdersAction action, IDispatcher dispatcher)
        {
            try
            {
                var orders = await _orderService.GetOrdersAsync();
                dispatcher.Dispatch(new LoadOrdersSuccessAction(orders));
            }
            catch (Exception ex)
            {
                dispatcher.Dispatch(new LoadOrdersFailureAction(ex.Message));
            }
        }

        [EffectMethod]
        public async Task HandleAddOrderAction(AddOrderAction action, IDispatcher dispatcher)
        {
            try
            {
                var addedOrder = await _orderService.AddOrderAsync(action.Order);
                dispatcher.Dispatch(new AddOrderSuccessAction(addedOrder));
            }
            catch (Exception ex)
            {
                dispatcher.Dispatch(new AddOrderFailureAction(ex.Message));
            }
        }

        [EffectMethod]
        public async Task HandleUpdateOrderAction(UpdateOrderAction action, IDispatcher dispatcher)
        {
            try
            {
                var updatedOrder = await _orderService.UpdateOrderAsync(action.Order);
                dispatcher.Dispatch(new UpdateOrderSuccessAction(updatedOrder));
            }
            catch (Exception ex)
            {
                dispatcher.Dispatch(new UpdateOrderFailureAction(ex.Message));
            }
        }

        [EffectMethod]
        public async Task HandleDeleteOrderAction(DeleteOrderAction action, IDispatcher dispatcher)
        {
            try
            {
                var success = await _orderService.DeleteOrderAsync(action.OrderId);
                if (success)
                    dispatcher.Dispatch(new DeleteOrderSuccessAction(action.OrderId));
                else
                    dispatcher.Dispatch(new DeleteOrderFailureAction("Failed to delete order."));
            }
            catch (Exception ex)
            {
                dispatcher.Dispatch(new DeleteOrderFailureAction(ex.Message));
            }
        }
    }
}