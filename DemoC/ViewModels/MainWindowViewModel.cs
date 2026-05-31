using CommunityToolkit.Mvvm.ComponentModel;

namespace DemoC.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        [ObservableProperty]
        private ViewModelBase _currentViewModel = new LoginViewModel();

        public static MainWindowViewModel Instance = null!;

        public MainWindowViewModel()
        {
            Instance = this;
        }
    }
}
