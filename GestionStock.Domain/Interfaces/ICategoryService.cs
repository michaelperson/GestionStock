using GestionStock.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionStock.Domain.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategorieModel>?> GetCategoriesAsync();
    }
}
