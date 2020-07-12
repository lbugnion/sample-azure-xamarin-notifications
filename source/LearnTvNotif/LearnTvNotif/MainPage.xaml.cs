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
        private INotificationsReceiver _notificationsReceiver;

        public MainPage()
        {
            _notificationsReceiver = DependencyService.Get<INotificationsReceiver>();
            _notificationsReceiver.NotificationReceived += _notificationsReceiver_NotificationReceived;
            _notificationsReceiver.ErrorReceived += _notificationsReceiver_ErrorReceived;

            InitializeComponent();
        }

        private void _notificationsReceiver_ErrorReceived(object sender, string e)
        {
            Dispatcher.BeginInvokeOnMainThread(() =>
            {
                MainLabel.TextColor = Color.Red;
                MainLabel.Text = e;
            });
        }

        private void _notificationsReceiver_NotificationReceived(object sender, string e)
        {
            Dispatcher.BeginInvokeOnMainThread(() =>
            {
                MainLabel.TextColor = Color.Black;
                MainLabel.Text = e;
            });
        }
    }
}