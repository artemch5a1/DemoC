using Avalonia.Controls.Notifications;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DemoC.Models;
using DemoC.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DemoC.ViewModels
{
    public partial class CreateOrEditProductViewModel : ViewModelBase
    {
        [ObservableProperty]
        private Product productValue;

        [ObservableProperty]
        private ObservableCollection<Manufacturer> manufacturers = new();

        [ObservableProperty]
        private ObservableCollection<Supplier> suppliers = new();

        [ObservableProperty]
        private ObservableCollection<Category> categories = new();

        [ObservableProperty]
        private bool isEdit = false;

        [ObservableProperty]
        private string priceString = string.Empty;

        [ObservableProperty]
        private string saleString = string.Empty;

        [ObservableProperty]
        private string couninString = string.Empty;

        public CreateOrEditProductViewModel(int? productId = null)
        {
            ProductValue = productId is null ? 
                CreateProduct() : 
                _context.Products.FirstOrDefault(x => x.Productid == productId) ?? CreateProduct();

            IsEdit = productId is not null; 

            PriceString = ProductValue.Price.ToString();
            CouninString = ProductValue.Countin.ToString();
            SaleString = ProductValue.Sale.ToString();

            LoadAll();
        }

        private Product CreateProduct() 
        {
            return new Product() { Categoryid = -1, Manufacturerid = -1, Supplierid = -1 };
        }

        private void LoadAll() 
        {
            List<Supplier> suppliers = _context.Suppliers.ToList();

            List<Manufacturer> manufacturers = _context.Manufacturers.ToList();

            List<Category> categories = _context.Categories.ToList();

            Manufacturers = new ObservableCollection<Manufacturer>(manufacturers);

            Suppliers = new ObservableCollection<Supplier>(suppliers);

            Categories = new ObservableCollection<Category>(categories);
        }

        [RelayCommand]
        private void Save() 
        {
            if (string.IsNullOrWhiteSpace(ProductValue.Title) ||
                string.IsNullOrWhiteSpace(ProductValue.Description) ||
                string.IsNullOrWhiteSpace(ProductValue.UnitOfMeasurement) ||
                string.IsNullOrWhiteSpace(ProductValue.Articul) ||
                ProductValue.Categoryid == -1 ||
                ProductValue.Manufacturerid == -1 ||
                ProductValue .Supplierid == -1)
            {
                MainWindow.NotificationManager?.Show(new Notification("Ошибка", "Есть незаполненые поля", NotificationType.Error));
                return;
            }

            if (decimal.TryParse(PriceString, out decimal price) &&
                int.TryParse(CouninString, out int countin) &&
                int.TryParse(SaleString, out int sale))
            {
                ProductValue.Price = price;
                ProductValue.Countin = countin;
                ProductValue.Sale = sale;
            }
            else 
            {
                MainWindow.NotificationManager?.Show(new Notification("Ошибка", "Неверный формат числа", NotificationType.Error));
                return;
            }

            if (IsEdit)
                Edit();
            else
                Create();
        }

        private void Edit() 
        {
            try
            {
                _context.Products.Update(ProductValue);

                _context.SaveChanges();

                MainWindow.NotificationManager?.Show(new Notification("Успех", $"Данные обновлены", NotificationType.Success));

                MainWindowViewModel.Instance.CurrentViewModel = new ProductViewModel();
            }
            catch (Exception ex)
            {
                MainWindow.NotificationManager?.Show(new Notification("Ошибка", $"{ex.Message}", NotificationType.Error));
            }
        }

        private void Create() 
        {
            try 
            {
                _context.Products.Add(ProductValue);

                _context.SaveChanges();

                MainWindow.NotificationManager?.Show(new Notification("Успех", $"Данные обновлены", NotificationType.Success));

                MainWindowViewModel.Instance.CurrentViewModel = new ProductViewModel();
            } 
            catch (Exception ex) 
            {
                MainWindow.NotificationManager?.Show(new Notification("Ошибка", $"{ex.Message}", NotificationType.Error));
            }
        }

        [RelayCommand]
        private static void Back() => MainWindowViewModel.Instance.CurrentViewModel = new ProductViewModel();
    }
}
