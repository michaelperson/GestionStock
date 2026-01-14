using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GestionStock.Domain.ViewModels
{
    public class PostProductModel
    {
        [MinLength(4)]
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required double Price { get; set; }
        public required int Stock { get; set; }
        public int[] Categories { get; set; }

        public IBrowserFile? Image { get; set; }
    }
}
