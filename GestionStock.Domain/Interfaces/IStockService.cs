using GestionStock.Domain.ViewModels;

namespace GestionStock.Domain.Interfaces
{
    public interface IStockService
    {
        Task<List<ProductModel>?> GetProductAsync();
        Task<bool> CreateProductAsync(PostProductModel postProductDto);  
    }
}
