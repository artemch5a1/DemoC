using CommunityToolkit.Mvvm.ComponentModel;
using DemoC.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace DemoC.ViewModels
{
    public partial class ProductViewModel : ViewModelBase
    {
        [ObservableProperty]
        private ObservableCollection<Product> _products = new();

        public ProductViewModel()
        {
            _ = LoadProduct();
        }

        private async Task LoadProduct()
        {
            List<Product> products = await _context.Products
                .Include(x => x.Manufacturer)
                .Include(x => x.Supplier)
                .Include(x => x.Category)
                .ToListAsync();

            Products = new ObservableCollection<Product>(products);
        }
    }
}
