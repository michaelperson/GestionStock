using System;
using System.Collections.Generic;
using System.Text;

namespace GestionStock.Domain.ViewModels
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public double Price { get;set;  }
        public IEnumerable<CategorieModel> Categories { get; set; }
    }
}
