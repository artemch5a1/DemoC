using CommunityToolkit.Mvvm.ComponentModel;
using DemoC.Models;

namespace DemoC.ViewModels
{
    public class ViewModelBase : ObservableObject
    {
        protected readonly PostgresContext _context = new PostgresContext();

        protected Account? currentAccount = null;
    }
}
