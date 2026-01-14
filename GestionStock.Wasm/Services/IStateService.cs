using GestionStock.Domain.ViewModels;

namespace GestionStock.Wasm.Services
{
    public interface IStateService
    {
        ProductModel? SelectedProduct { get; set; }
    }
}
