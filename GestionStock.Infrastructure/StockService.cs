using GestionStock.Domain.Dto;
using GestionStock.Domain.Interfaces;
using GestionStock.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace GestionStock.Infrastructure
{
    public class StockService: IStockService
    {
        private readonly IApiService _apiService;

        public StockService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<List<ProductModel>?> GetProductAsync()
        {
             ApiResult<List<ProductDto>> resultProductsDto = await _apiService.GetAsync<List<ProductDto>>("api/Product");
             if (!resultProductsDto.IsSuccess || resultProductsDto.Data == null)
                 return null;

            // Map ProductDto to ProductModel
            var productModels = resultProductsDto.Data.Select(dto => new ProductModel
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
                ImageUrl = dto.ImageUrl,
                Price = dto.Price,
                Categories = dto.Categories.Select(c => new CategorieModel { Id = c.Id, Name = c.Name })
            }).ToList();

             return productModels;
        }

        public async Task<bool> CreateProductAsync(PostProductModel postProductModel)
        {

            // 1. Construction de la Query String
            List<string> queryParams = new List<string>
            {
                $"Name={HttpUtility.UrlEncode(postProductModel.Name)}",
                $"Description={HttpUtility.UrlEncode(postProductModel.Description)}",
                $"Price={postProductModel.Price.ToString(System.Globalization.CultureInfo.InvariantCulture)}",
                $"Stock={postProductModel.Stock}"
            };
             
            if (postProductModel.Categories != null)
            {
                foreach (int catId in postProductModel.Categories)
                {
                    queryParams.Add($"Categories={catId}");
                }
            }

            string queryString = string.Join("&", queryParams);
            string fullUri = $"api/Product?{queryString}"; 
            using MultipartFormDataContent content = new MultipartFormDataContent(); 
              
            if (postProductModel.Image != null)
            {
                // Limite de taille pour Blazor (ex: 5MB)
                int maxAllowedSize = 1024 * 1024 * 5;
                Stream stream = postProductModel.Image.OpenReadStream(maxAllowedSize);
                StreamContent fileContent = new StreamContent(stream);

                // On définit le type de contenu (image/jpeg, image/png...)
                fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(postProductModel.Image.ContentType);

                content.Add(fileContent, "Image", postProductModel.Image.Name);
            }

            // 4. Envoi à l'API 
            ApiResult<string> response = await _apiService.PostMultipartAsync<string>(fullUri, content);

            return response.IsSuccess;
        }
    }
}
