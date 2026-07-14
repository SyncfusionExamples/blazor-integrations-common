using Fluxor;

namespace FluxorBlazorDataGrid.Store.OrderState
{
    public class OrderFeature : Feature<OrderState>
    {
        public override string GetName() => nameof(OrderState);

        protected override OrderState GetInitialState()
        {
            return new OrderState
            {
                Orders = new List<Models.Order>(),
                IsLoading = false,
                ErrorMessage = null
            };
        }
    }
}