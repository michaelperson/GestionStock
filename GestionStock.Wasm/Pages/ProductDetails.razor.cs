using GestionStock.Wasm.Services;
using Microsoft.AspNetCore.Components;

namespace GestionStock.Wasm.Pages
{
    public partial class ProductDetails
    {
        [Inject]
        public required IStateService stateService { get;set;  }

        [Inject]
        public required NavigationManager NavigationManager { get; set; }

        protected  override void  OnInitialized()
        {
            if (stateService.SelectedProduct == null) NavigationManager.NavigateTo("/products");
        }

        private void HandleReturn()
        {
            NavigationManager.NavigateTo("/products");
        }
    }
}
