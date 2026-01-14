using GestionStock.Domain.Dto;
using GestionStock.Domain.Interfaces;
using GestionStock.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionStock.Infrastructure
{
    public class CategoryService : ICategoryService
    {
        private readonly IApiService _apiService;

        public CategoryService(IApiService apiService)
        {
            _apiService = apiService;
        }
        public async Task<List<CategorieModel>?> GetCategoriesAsync()
        {
            
            ApiResult<List<CategorieDto>> resultCategoriesDto = await _apiService.GetAsync<List<CategorieDto>>("api/Category");
            if (!resultCategoriesDto.IsSuccess || resultCategoriesDto.Data == null)
                return null;

            var CategorieModels = resultCategoriesDto.Data.Select(dto => new CategorieModel
            {
                Id = dto.Id,
                Name = dto.Name                 
            }).ToList();

            return CategorieModels;
        }
    }
}
