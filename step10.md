# Notifications Client Step 1.0: Creating the FCM project

[Previous article: Notifications Client Step 0: Specification](./step00.md)

[Main article: Building a notifications client with Xamarin and Azure](./README.md)

![Xamarin, Notification Hub, Functions, Cosmos DB](https://i.imgur.com/Udszogi.png)

## Intro

I have decided to build a cross-platform Notifications Client used to receive push notifications when a long-running system needs it. This Notifications Client is generic, and not tied to a specific system or application, but serves as a receiver and repository for all kinds of notifications.

This article is part of **Step 1: Sending a notification from Azure Notification Hub**. In here, we will set the Firebase Cloud Messaging (FCM) service and prepare it to be an intermediate between Azure Notification Hub and our mobile app on Android.

## Creating a new FCM project

Android notifications are sent through a service called Firebase Cloud Messaging (this is a Google service). When you use an "aggregation service" like Azure Notification Hub, this service will communicate with FCM to send the notification to all the Android devices who registered for it. Similarly, Notification Hub will also communicate with corresponding services for Windows (we will set this later) and for iOS.

> Note: For this to work, the Android device needs to support Google Play Services. The Azure Notification Hub also supports Baidu as a target, but this won't be treated in this article.

To start, we need to [navigate to the Firebase console](https://console.firebase.google.com/). Then follow the steps:

1. Click on Create a project.

![Creating a new Firebase project](https://i.imgur.com/mjrG1JF.png)

2. In the first step, enter a name for your new project. You can choose any name, this is just for management purposes.

![Setting up the new Firebase project](https://i.imgur.com/13WNJ3d.png)

3. You can choose to enable analytics or not. In my case, I decided to turn this off, because I will be using Application Insights on Azure to monitor the system, and I don't really need a separate analytics solution just for the Android devices. But your mileage may vary...

![Setting up the project's analytics](https://i.imgur.com/alwdREa.png)

4. The new project will be provisioned, so just wait for a moment until it is ready.

![Provisioning...](https://i.imgur.com/gVOtzFA.png)

> Firebase can be used for quite a lot of things next to push notifications, but we are not going to use these features here. The main point of this project is to be a gateway between Azure Notification Hub and the Android devices.

5. Now we will configure the Android application. This is quite easy. First, in the "Get started by adding Firebase to your app" section, click on the Android icon.

![Starting the Android configuration](https://i.imgur.com/suETI8y.png)

6. Next, we will link the Firebase project to the Android application that we will create later. You need to enter an Android package name. You can also enter a nickname for this app, this is just for management purpose.

> The Android package name is of the form `com.[yourcompanyname].[yourapplicationname]`. Make sure to write it down for later.

![Setting up the application's details](https://i.imgur.com/65bND7M.png)

7. On the next step, download the `google-service.json` file and save it in a safe location. This file contains all the information needed by the Android devices to register with the Firebase system to get the notifications.

![Downloading the configuration file](https://i.imgur.com/9DfgiPL.png)

8. On the next screen, you can just press `Next` and then `Continue to console`.

![Skipping to the next step](https://i.imgur.com/jn0Nh3q.png)

## Writing down the keys

Now we will write down the secret keys that we'll need to use in the Android application. Later we'll use these keys in the Xamarin code, and even later we'll save these keys to Azure Key Vault for safe keeping.

1. In the Firebase Console, select your application.

2. In the application dashboard, click on the button with the small Android icon, and the name of your app.

![Navigating to your application](https://i.imgur.com/REMB0et.png)

3. Click on the Settings icon

![Navigating to the settings](https://i.imgur.com/Ex04Xjw.png)

4. Click on `Cloud Messaging`.

![Navigating to Cloud Messaging](https://i.imgur.com/rWu1cv6.png)

5. Copy the Server Key and save it somewhere for later.

![Copying the Server Key](https://i.imgur.com/BAGbApk.png)

## Well done!!

That's it, you have created and configured a new Google Firebase Cloud Messaging project, and downloaded the file you need.

> If you lose the `google-services.json` file, you can always retrieve it [from the Firebase console](https://console.firebase.google.com/).

You have also retrieved the Server key which we will use to configure the Azure Notification Hub. Make sure to save it!

## In the next article...

In the next article (coming soon), we will see how to build an "empty" Xamarin Forms application and configure it to receive the notification from FCM.

Happy coding!

Laurent