using LearnTvNotif.Droid.Model;
using LearnTvNotif.Model;
using System;

[assembly: Xamarin.Forms.Dependency(typeof(NotificationsReceiver))]

namespace LearnTvNotif.Droid.Model
{
    public class NotificationsReceiver : INotificationsReceiver
    {
        public event EventHandler<string> ErrorReceived;

        public event EventHandler<string> NotificationReceived;

        public void RaiseErrorReceived(string message)
        {
            ErrorReceived?.Invoke(this, message);
        }

        public void RaiseNotificationReceived(string message)
        {
            NotificationReceived?.Invoke(this, message);
        }
    }
}