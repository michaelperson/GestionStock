using GestionStock.Wasm.Components.MultiSelect.Models;
using GestionStock.Domain.Interfaces;
using GestionStock.Domain.ViewModels;
using GestionStock.Infrastructure;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace GestionStock.Wasm.Pages
{
    public partial class ProductAdd
    {
        [Inject]
        public required IStockService StockService { get; set; }
        [Inject]
        public required ICategoryService CategoryService { get; set; }
        [Inject]
        public required NavigationManager NavigationManager { get; set; }
        private PostProductModel _formModel;
        private string? _imagePreview;
        private bool _isSubmitting;
        private string? _errorMessage;

        private List<MultiSelectItem> AllCategories = new();
        private List<MultiSelectItem> MySelectedCategories = new();

        public ProductAdd()
        {
            _formModel = new()
            {
                Name = string.Empty,
                Description = string.Empty,
                Price = 0.0,
                Stock = 0,
                Categories = Array.Empty<int>(),
                Image = null
            };
        }

        protected async override Task OnInitializedAsync()
        {
           try
            {
                List<CategorieModel> ? cats = await CategoryService.GetCategoriesAsync();
                
                if (cats == null)
                {
                    _errorMessage = "Impossible de récupérer les données du serveur.";
                }
                else
                {
                    AllCategories = cats.Select(c=> new MultiSelectItem { Display=c.Name, Value=c.Id }).ToList();
                }
            }
            catch (Exception ex)
            {
                _errorMessage = "Une erreur inattendue est survenue lors du chargement des catégories.";
                Console.WriteLine(ex.Message);
            }
            

             
        } 

        // Gestion du fichier image
        private async Task OnInputFileChange(InputFileChangeEventArgs e)
        {
            if (_formModel == null) return;
            _formModel.Image = e.File;
 
            string format = "image/png";
            IBrowserFile resizedImage = await e.File.RequestImageFileAsync(format, 300, 300);
            byte[] buffer = new byte[resizedImage.Size];
            await resizedImage.OpenReadStream().ReadAsync(buffer);
            _imagePreview = $"data:{format};base64,{Convert.ToBase64String(buffer)}";
        }

        private async Task HandleSubmit()
        {
            _isSubmitting = true;
            _errorMessage = null;

            try
            {
                /*mapping des sélectioncat vers les modèle*/
                _formModel.Categories = MySelectedCategories.Select(m => m.Value).ToArray();
                bool success = await StockService.CreateProductAsync(_formModel);
                Console.WriteLine(success);
                if (success)
                {
                    NavigationManager.NavigateTo("/products");
                }
                else
                {
                    _errorMessage = "Une erreur est survenue lors de la création sur le serveur.";
                }
            }
            catch (Exception ex)
            {
                _errorMessage = "Erreur de connexion : " + ex.Message;
            }
            finally
            {
                _isSubmitting = false;
            }
        }

        private void GoBack() => NavigationManager.NavigateTo("/products");

        
         
    }
}
