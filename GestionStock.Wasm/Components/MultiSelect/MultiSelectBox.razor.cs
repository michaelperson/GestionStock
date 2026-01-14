using GestionStock.Wasm.Components.MultiSelect.Models;
using Microsoft.AspNetCore.Components;

namespace GestionStock.Wasm.Components.MultiSelect
{
    public partial class MultiSelectBox
    {
        [Parameter] public List<MultiSelectItem> Items { get; set; } = new();
        [Parameter] public List<MultiSelectItem> SelectedItems { get; set; } = new();
        [Parameter] public EventCallback<List<MultiSelectItem>> SelectedItemsChanged { get; set; }

        private bool isOpen = false;

        private void ToggleDropdown() => isOpen = !isOpen;

        private bool IsSelected(MultiSelectItem item) => SelectedItems.Any(c => c.Value == item.Value);

        private async Task OnCheckboxChange(MultiSelectItem item, object? checkedValue)
        {
            bool isChecked = (bool)(checkedValue ?? false);

            if (isChecked)
            {
                if (!SelectedItems.Any(c => c.Value == item.Value))
                    SelectedItems.Add(item);
            }
            else
            {
                var itemToRemove = SelectedItems.FirstOrDefault(c => c.Value == item.Value);
                if (itemToRemove != null)
                    SelectedItems.Remove(itemToRemove);
            }

            await SelectedItemsChanged.InvokeAsync(SelectedItems);
        }
    }
}
