using Fluxor;
using FluxorBlazorDataGrid.Store.OrderState.Actions;

namespace FluxorBlazorDataGrid.Store.OrderState.Reducers
{
    public static class OrderReducers
    {
        // Load Reducers
        [ReducerMethod]
        public static OrderState ReduceLoadOrdersAction(OrderState state, LoadOrdersAction action)
            => state with { IsLoading = true, ErrorMessage = null };

        [ReducerMethod]
        public static OrderState ReduceLoadOrdersSuccessAction(OrderState state, LoadOrdersSuccessAction action)
            => state with { Orders = action.Orders, IsLoading = false, ErrorMessage = null };

        [ReducerMethod]
        public static OrderState ReduceLoadOrdersFailureAction(OrderState state, LoadOrdersFailureAction action)
            => state with { IsLoading = false, ErrorMessage = action.ErrorMessage };

        // Add Reducers
        [ReducerMethod]
        public static OrderState ReduceAddOrderAction(OrderState state, AddOrderAction action)
            => state with { IsLoading = true, ErrorMessage = null };

        [ReducerMethod]
        public static OrderState ReduceAddOrderSuccessAction(OrderState state, AddOrderSuccessAction action)
        {
            var updatedOrders = new List<Models.Order>(state.Orders) { action.Order };
            return state with { Orders = updatedOrders, IsLoading = false, ErrorMessage = null };
        }

        [ReducerMethod]
        public static OrderState ReduceAddOrderFailureAction(OrderState state, AddOrderFailureAction action)
            => state with { IsLoading = false, ErrorMessage = action.ErrorMessage };

        // Update Reducers
        [ReducerMethod]
        public static OrderState ReduceUpdateOrderAction(OrderState state, UpdateOrderAction action)
            => state with { IsLoading = true, ErrorMessage = null };

        [ReducerMethod]
        public static OrderState ReduceUpdateOrderSuccessAction(OrderState state, UpdateOrderSuccessAction action)
        {
            var updatedOrders = state.Orders
                .Select(o => o.Id == action.Order.Id ? action.Order : o)
                .ToList();
            return state with { Orders = updatedOrders, IsLoading = false, ErrorMessage = null };
        }

        [ReducerMethod]
        public static OrderState ReduceUpdateOrderFailureAction(OrderState state, UpdateOrderFailureAction action)
            => state with { IsLoading = false, ErrorMessage = action.ErrorMessage };

        // Delete Reducers
        [ReducerMethod]
        public static OrderState ReduceDeleteOrderAction(OrderState state, DeleteOrderAction action)
            => state with { IsLoading = true, ErrorMessage = null };

        [ReducerMethod]
        public static OrderState ReduceDeleteOrderSuccessAction(OrderState state, DeleteOrderSuccessAction action)
        {
            var updatedOrders = state.Orders.Where(o => o.Id != action.OrderId).ToList();
            return state with { Orders = updatedOrders, IsLoading = false, ErrorMessage = null };
        }

        [ReducerMethod]
        public static OrderState ReduceDeleteOrderFailureAction(OrderState state, DeleteOrderFailureAction action)
            => state with { IsLoading = false, ErrorMessage = action.ErrorMessage };
    }
}