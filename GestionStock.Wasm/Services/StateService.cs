using GestionStock.Domain.ViewModels;

namespace GestionStock.Wasm.Services
{
    public class StateService : IStateService
    {
        public ProductModel? SelectedProduct { get; set; }
    }
}
