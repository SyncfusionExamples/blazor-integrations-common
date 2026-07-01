using FluxorBlazorDataGrid.Models;

namespace FluxorBlazorDataGrid.Store.OrderState
{
    public record OrderState
    {
        public List<Order> Orders { get; init; } = new();
        public bool IsLoading { get; init; }
        public string? ErrorMessage { get; init; }
    }
}