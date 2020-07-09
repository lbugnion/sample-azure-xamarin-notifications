# Notifications Client Step 0: Specification

![Xamarin, Notification Hub, Functions, Cosmos DB](https://i.imgur.com/Udszogi.png)

## Intro

> You can see me speaking to Frank Boucher about this application, and building it together. [Check the video](https://www.youtube.com/ZliwX9Hgy0c)!!

[Main page: Building a notifications client with Xamarin and Azure](./README.md)

I have decided to build a cross-platform Notifications Client used to receive push notifications when a long-running system needs it. This Notifications Client is generic, and not tied to a specific system or application, but serves as a receiver and repository for all kinds of notifications.

I have the following specifications in mind:

- The client will have different pages. The notifications can have a "channel", which will sort them to a specific page.

> Note: The "channel" here is a custom property I am adding to the notification for my own purpose. It's not the same at the Push Notification Topic in Firebase, which allows to push notifications only to selected devices, or the customer segments in Windows, which have the same goal. Here we just use the channel as a way to sort notifications in pages.

![App main page with all the channels](https://i.imgur.com/VgEF7yZ.png)

*App main page with all the channels*

- There should be one page showing all the notifications, and one page per channel with only the corresponding notifications.

![Channel page with the corresponding notifications](https://i.imgur.com/ltjgQbb.png)

*Channel page with the corresponding notifications*

![Detail of a notification template](https://i.imgur.com/hQSxBSb.png)

*Detail of a notification template*

- The client should save the notifications locally.

- The client should allow the notifications to be searched on the current page (or the "all notifications" page).

- Notifications can be received when the application is off, or when it is running.

- Eventually, the notifications should also be saved in the app for offline mode.

- Notifications can be deleted from the app.

![Detail page for a notification](https://i.imgur.com/XbETnSJ.png)

*Detail page for a notification*

- The client should be implemented in [Xamarin.Forms](http://gslb.ch/d424b) to be cross-platform, even though initially I only need Android and possibly Windows (UWP).

## Architecture

The architecture is composed of a few Azure services, as well as a Google service for Android notifications, a Windows service for Windows notifications, and later of an Apple service for iOS notifications.

![Notification system architecture](https://i.imgur.com/cPGS4vU.png)

*Notification system architecture*

1. Any system can send a simple POST request over HTTP to an [Azure Functions endpoint](http://gslb.ch/d10b). The payload is a simple JSON file containing a title, a message and an optional channel.

> If you are watching systems on Azure, a great way to connect a system to the Azure Functions endpoint [is to use EventGrid](http://gslb.ch/d430b).

2. The Azure Function passes the notification request to an [Azure Notification Hub](http://gslb.ch/d425b).

3. The Notification Hub communicates with [Firebase Cloud Messaging (FCM)](http://gslb.ch/d426b) (Android), the corresponding services for Windows ([Windows Push Notification Service WNS](http://gslb.ch/d427b)) and iOS ([Apple Push Notification Service APNS](http://gslb.ch/d428b)). The notification is enqueued on these systems and sent to registered devices.

> Azure Notification Hub also supports other systems such as Amazon (ADM), Baidu, and even good old Windows Phone!!

4. FCM sends the notification to registered Android devices. Similarly, APNS sends the notification to iOS devices, and WNS sends the notification to Windows devices. What happens on the device is described below in [When a device receives a notification](#NotificationOnDevice).

5. After the push notification has been successfully enqueued, it is saved to a cloud database. We will use the [new CosmosDB free tier for this](http://gslb.ch/d422b), as this allows for a powerful yet simple NoQSL database to be used.

<a id="NotificationOnDevice"></a>
## When a device receives a notification

There are three scenarios. 

- **The application is running in the foreground**: The new notification is shown on a page, the user's attention is attracted for example by a sound or a flashing.

- **The application is in the background, or not running**: The new notification is displayed as a push notification. The user taps the notification, which starts the app. The new notification is shown on a page.

- **Same scenario but the user dismisses the notification**: Nothing happens until the next time that the app starts.

6. When the running app receives a new notification, or when the app is started from a new notification, the notification is saved in a local database on the device ([SQLite](http://gslb.ch/d429b)). 

7. A synchronization can occur between the cloud database and the device database. The synchronization is started by the user, it also occurs when a notification is deleted on a device, or every time that the application starts.

## In the next article...

Hopefully these specs make sense for everyone! 

[In the next page, we will get started with the implementation of the notification system](./step10.md)!

[Main page: Building a notifications client with Xamarin and Azure](./README.md)

Happy coding

Laurent