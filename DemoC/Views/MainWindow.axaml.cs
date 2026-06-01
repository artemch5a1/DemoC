using Avalonia.Controls;
using Avalonia.Controls.Notifications;

namespace DemoC.Views
{
    public partial class MainWindow : Window
    {
        public static INotificationManager? NotificationManager;

        public static MainWindow Instance = null!;

        public MainWindow()
        {
            InitializeComponent();

            NotificationManager = new WindowNotificationManager(this)
            {
                Position = NotificationPosition.TopRight,
                MaxItems = 3,
            };

            Instance = this;
        }
    }
}