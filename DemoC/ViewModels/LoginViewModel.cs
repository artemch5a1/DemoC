using Avalonia.Controls.Notifications;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DemoC.Models;
using DemoC.Views;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DemoC.ViewModels
{
    public partial class LoginViewModel : ViewModelBase
    {
        [ObservableProperty]
        private string _loginValue = "94d5ous@gmail.com";

        [ObservableProperty]
        private string _password = "uzWC67";

        [RelayCommand]
        private async Task Login()
        {
            Account? account = await _context.Accounts.FirstOrDefaultAsync(a => a.Login == LoginValue && a.Password == Password);

            if (account == null) 
            {
                MainWindow.NotificationManager?.Show(new Notification("Ошибка", "Неверный логин иили пароль", NotificationType.Error));
                return;
            }

            currentAccount = account;
            MainWindowViewModel.Instance.CurrentViewModel = new ProductViewModel(); 
        }

    }
}
