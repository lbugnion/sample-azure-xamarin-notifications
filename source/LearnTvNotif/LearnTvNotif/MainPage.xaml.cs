using LearnTvNotif.Model;
using System.ComponentModel;
using Xamarin.Forms;

namespace LearnTvNotif
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private readonly INotificationsReceiver _notificationsReceiver;

        public MainPage()
        {
            _notificationsReceiver = DependencyService.Get<INotificationsReceiver>();
            _notificationsReceiver.NotificationReceived += NotificationsReceiver_NotificationReceived;
            _notificationsReceiver.ErrorReceived += NotificationsReceiver_ErrorReceived;

            InitializeComponent();
        }

        private void NotificationsReceiver_ErrorReceived(object sender, string e)
        {
            Dispatcher.BeginInvokeOnMainThread(() =>
            {
                MainLabel.TextColor = Color.Red;
                MainLabel.Text = e;
            });
        }

        private void NotificationsReceiver_NotificationReceived(object sender, string e)
        {
            Dispatcher.BeginInvokeOnMainThread(() =>
            {
                MainLabel.TextColor = Color.Black;
                MainLabel.Text = e;
            });
        }
    }
}