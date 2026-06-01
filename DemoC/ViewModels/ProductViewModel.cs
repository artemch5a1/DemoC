using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DemoC.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace DemoC.ViewModels
{
    public partial class ProductViewModel : ViewModelBase
    {
        [ObservableProperty]
        private ObservableCollection<Product> _products = new();

        [ObservableProperty]
        private ObservableCollection<Supplier> _suppliers = new();

        [ObservableProperty]
        private Supplier? _selectedSupplier = null;

        [ObservableProperty]
        private string _searchText = string.Empty;

        [ObservableProperty]
        private ObservableCollection<string> sortedParametrs = new() { "По возрастанию кол-ва на складе", "По уменьшению кол-ва на складе" };

        [ObservableProperty]
        private string? selectedSortedParametrs = null;

        [ObservableProperty]
        public bool canFilter;

        [ObservableProperty]
        public bool canCrud;

        public ProductViewModel()
        {
            _ = LoadProduct();
            CanFilter = currentAccount?.Role == Enums.Role.Manager || currentAccount?.Role == Enums.Role.Admin;
            CanCrud = currentAccount?.Role == Enums.Role.Admin;
        }

        private async Task LoadProduct()
        {
            List<Product> products = await _context.Products
                .Include(x => x.Manufacturer)
                .Include(x => x.Supplier)
                .Include(x => x.Category)
                .ToListAsync();

            Products = new ObservableCollection<Product>(products);

            List<Supplier> suppliers = await _context.Suppliers.ToListAsync();

            Suppliers.Add(new Supplier() { Supplierid = -2, Title = "Сбросить фильтр" });

            foreach (Supplier supplier in suppliers) 
            {
                Suppliers.Add(supplier);
            }
        }

        public async Task Filter() 
        {
            List<Product> products = await FilterBySuppliers(SearchByText(Sort(_context.Products))).ToListAsync();

            Products = new ObservableCollection<Product>(products);
        }

        private IQueryable<Product> FilterBySuppliers(IQueryable<Product> productQuery) 
        {
            if (SelectedSupplier is null || SelectedSupplier.Supplierid == -2)
                return productQuery;

            return productQuery
                .Where(x => x.Supplier.Supplierid == SelectedSupplier.Supplierid);
        }

        private IQueryable<Product> SearchByText(IQueryable<Product> productQuery) 
        {
            if(string.IsNullOrEmpty(SearchText))
                return productQuery;

            return productQuery
                .Where(x => x.Manufacturer.Title.Contains(SearchText)
                || x.Supplier.Title.Contains(SearchText)
                || x.Category.Title.Contains(SearchText)
                || x.Title.Contains(SearchText)
                || x.Description.Contains(SearchText)
                || x.UnitOfMeasurement.Contains(SearchText)
                || x.Price.ToString().Contains(SearchText)
                || x.Sale.ToString().Contains(SearchText)
                || x.Countin.ToString().Contains(SearchText));
        }

        private IQueryable<Product> Sort(IQueryable<Product> productQuery) 
        {
            if (string.IsNullOrEmpty(SelectedSortedParametrs))
                return productQuery;

            if(SelectedSortedParametrs == "По возрастанию кол-ва на складе")
                return productQuery.OrderBy(x => x.Countin);
            else
                return productQuery.OrderByDescending(x => x.Countin);
        }

        partial void OnSearchTextChanged(string value)
        {
            _ = Filter();
        }

        partial void OnSelectedSupplierChanged(Supplier? oldValue, Supplier? newValue)
        {
            _ = Filter();
        }

        partial void OnSelectedSortedParametrsChanged(string? value)
        {
            _ = Filter();
        }

        [RelayCommand]
        private void CreateNav() 
        {
            MainWindowViewModel.Instance.CurrentViewModel = new CreateOrEditProductViewModel();
        }
    }
}
