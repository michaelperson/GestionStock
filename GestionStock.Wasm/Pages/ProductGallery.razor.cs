using GestionStock.Domain.Interfaces;
using GestionStock.Domain.ViewModels;
using GestionStock.Wasm.Services;
using Microsoft.AspNetCore.Components;

namespace GestionStock.Wasm.Pages
{
    public partial class ProductGallery
    {
        [Inject]
        public required IStateService stateService { get; set; }
        [Inject]
        public required IStockService StockService { get;set;  }
        [Inject]
        public required NavigationManager NavigationManager { get; set;  }
        private List<ProductModel>? _products;
        private string? _errorMessage;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                _products = await StockService.GetProductAsync();
                stateService.SelectedProduct = null;
                if (_products == null)
                {
                    _errorMessage = "Impossible de récupérer les données du serveur.";
                }
            }
            catch (Exception ex)
            { 
                _errorMessage = "Une erreur inattendue est survenue lors du chargement des produits.";
                Console.WriteLine(ex.Message);
            }
        }

        private void HandleAddProduct()
        {
            // Redirige vers la page de création (à créer ensuite)
            NavigationManager.NavigateTo("/products/add");
        }

        private void HandleSelectProduct(int id)
        {
            stateService.SelectedProduct=_products.FirstOrDefault(p=>p.Id == id);
            NavigationManager.NavigateTo("/product/details");
        }
    }
}
