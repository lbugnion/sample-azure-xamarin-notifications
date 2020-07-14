using Android.App;
using Firebase.Messaging;
using LearnTvNotif.Model;
using WindowsAzure.Messaging;
using Xamarin.Forms;

namespace LearnTvNotif.Droid.Model
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class FirebaseMessagingServiceEx : FirebaseMessagingService
    {
        public override void OnMessageReceived(RemoteMessage remoteMessage)
        {
            base.OnMessageReceived(remoteMessage);

            var receiver = DependencyService.Get<INotificationsReceiver>();

            var message = remoteMessage.Data["body"];
            receiver.RaiseNotificationReceived(message);
        }

        public override async void OnNewToken(string token)
        {
            base.OnNewToken(token);

            System.Diagnostics.Debug.WriteLine(token);

            var hub = new NotificationHub(
                Constants.HubName,
                Constants.HubConnectionString,
                MainActivity.Context);

            // register device with Azure Notification Hub using the token from FCM
            var registration = hub.Register(token, Constants.HubTagName);

            var receiver = DependencyService.Get<INotificationsReceiver>();
            receiver.RaiseNotificationReceived("Ready and registered...");
        }
    }
}