using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionStock.Wasm.Components.MultiSelect.Models
{
    public class MultiSelectItem
    {
        public required int Value { get; set; }

        public required string Display { get; set; }
    }
}
