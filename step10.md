# Notifications Client Step 1.0: Creating the FCM project

[Previous article: Notifications Client Step 0: Specification](./step00.md)

[Main article: Building a notifications client with Xamarin and Azure](./building-notifications-client.md)

![Xamarin, Notification Hub, Functions, Cosmos DB](.\building-notifications-client\NotificationsClientIcons.png)

## Intro

I have decided to build a cross-platform Notifications Client used to receive push notifications when a long-running system needs it. This Notifications Client is generic, and not tied to a specific system or application, but serves as a receiver and repository for all kinds of notifications.

This article is part of **Step 1: Sending a notification from Azure Notification Hub**. In here, we will set the Firebase Cloud Messaging (FCM) service and prepare it to be an intermediate between Azure Notification Hub and our mobile app on Android.

## Creating a new FCM project

Android notifications are sent through a service called Firebase Cloud Messaging (this is a Google service). When you use an "aggregation service" like Azure Notification Hub, this service will communicate with FCM to send the notification to all the Android devices who registered for it. Similarly, Notification Hub will also communicate with corresponding services for Windows (we will set this later) and for iOS.

> Note: For this to work, the Android device needs to support Google Play Services. The Azure Notification Hub also supports Baidu as a target, but this won't be treated in this article.

To start, we need to [navigate to the Firebase console](https://console.firebase.google.com/). Then follow the steps:

1. Click on Create a project.

![Creating a new Firebase project](.\building-notifications-client\2020-03-20_11-33-58.png)

2. In the first step, enter a name for your new project. You can choose any name, this is just for management purposes.

![Setting up the new Firebase project](.\building-notifications-client\2020-03-20_11-34-21.png)

3. You can choose to enable analytics or not. In my case, I decided to turn this off, because I will be using Application Insights on Azure to monitor the system, and I don't really need a separate analytics solution just for the Android devices. But your mileage may vary...

![Setting up the project's analytics](.\building-notifications-client\2020-03-20_11-35-41.png)

4. The new project will be provisioned, so just wait for a moment until it is ready.

![Provisioning...](.\building-notifications-client\2020-03-20_11-35-59.png)

> Firebase can be used for quite a lot of things next to push notifications, but we are not going to use these features here. The main point of this project is to be a gateway between Azure Notification Hub and the Android devices.

5. Now we will configure the Android application. This is quite easy. First, in the "Get started by adding Firebase to your app" section, click on the Android icon.

![Starting the Android configuration](.\building-notifications-client\2020-03-20_11-42-20.png)

6. Next, we will link the Firebase project to the Android application that we will create later. You need to enter an Android package name. You can also enter a nickname for this app, this is just for management purpose.

> The Android package name is of the form `com.[yourcompanyname].[yourapplicationname]`. Make sure to write it down for later.

![Setting up the application's details](.\building-notifications-client\2020-03-20_12-45-45.png)

7. On the next step, download the `google-service.json` file and save it in a safe location. This file contains all the information needed by the Android devices to register with the Firebase system to get the notifications.

![Downloading the configuration file](.\building-notifications-client\2020-03-20_12-46-14.png)

8. On the next screen, you can just press `Next` and then `Continue to console`.

![Skipping to the next step](.\building-notifications-client\2020-03-20_12-46-54.png)

## Writing down the keys

Now we will write down the secret keys that we'll need to use in the Android application. Later we'll use these keys in the Xamarin code, and even later we'll save these keys to Azure Key Vault for safe keeping.

1. In the Firebase Console, select your application.

2. In the application dashboard, click on the button with the small Android icon, and the name of your app.

![Navigating to your application](.\building-notifications-client\2020-03-24_11-50-47.png)

3. Click on the Settings icon

![Navigating to the settings](.\building-notifications-client\2020-03-24_12-00-47.png)

4. Click on `Cloud Messaging`.

![Navigating to Cloud Messaging](.\building-notifications-client\2020-03-24_12-01-44.png)

5. Copy the Server Key and save it somewhere for later.

![Copying the Server Key](.\building-notifications-client\2020-03-24_12-02-43.png)

## Well done!!

That's it, you have created and configured a new Google Firebase Cloud Messaging project, and downloaded the file you need.

> If you lose the `google-services.json` file, you can always retrieve it [from the Firebase console](https://console.firebase.google.com/).

You have also retrieved the Server key which we will use to configure the Azure Notification Hub. Make sure to save it!

## In the next article...

In the next article (coming soon), we will see how to build an "empty" Xamarin Forms application and configure it to receive the notification from FCM.

Happy coding!

Laurent