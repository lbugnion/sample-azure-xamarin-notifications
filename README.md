# Building a notifications client with Xamarin and Azure

![Xamarin, Notification Hub, Functions, Cosmos DB](https://i.imgur.com/Udszogi.png)

## Intro

<iframe width="560" height="315" src="https://www.youtube.com/embed/ZliwX9Hgy0c" frameborder="0" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>

I often find myself needing to send a notification to my mobile device, for example when a job finishes on Azure, when one of my computers at home is done doing something, or when I just want to send an alert when something changes in a system I am observing. 

Normally I would go ahead and create a companion app for the system I am building, and then send notifications to this app, but sometimes it's really overkill to build a full-blown app for this. This is why I decided to build a generic notifications client that I can install on my phone.

What's really cool is that a lot of the development (with dotnet and Xamarin) can be done locally on my Surface Pro, which makes testing and debugging very much easier, before the services and applications are deployed to Azure and to my devices.

## Why Push Notifications? (aka why not SignalR?)

Why did I choose to use push notifications and not another messaging service like SignalR for example?

SignalR is an amazing feature that you can easily add to a Xamarin application [as shown in this sample](https://github.com/lbugnion/sample-xamarin-signalr). It is very well suited for high traffic, real-time communication. In our scenario, it doesn't quite fit, because we want the notifications to reach the device also when the application is not running, which is exactly what push notifications are made for. Also, even when the application is running, adding SignalR would be overkill because we don't really need real time, high traffic here. What we get from our watched systems is a notification from time to time, and even if this notification is delayed by a few seconds, it's not mission critical.

Finally the push notification system also includes retries, in case one device is offline for a period of time. This is very useful for our scenario too.

## Steps

I am planning to build this system in the following order:

### Step 0: Specification

In this step we will specify what we want the system to achieve, create a few sketches of the application and get ready :)

> [Go to step 0: Specification](./step00.md)

### Step 1: Sending a notification from the Firebase Cloud Messaging

The first step will include:

- Creating a new project in Google Firebase and setting up [Firebase Cloud Messaging (FCM)](http://gslb.ch/d426b).
- Building a client app just here to receive the Push Notification and test that it works.
- Test the notification with [with Postman](https://www.postman.com/).

> [Go to step 1.0: Creating the Firebase Cloud Messaging project](./step10.md)

<!-- [Go to step 1.1: Building and configuring the Xamarin client for Android](./step11.md) -->

<!-- ### [Step 2: Sending a notification from Azure Notification Hub](./building-notifications-client-2.md) -->

### Step 2: Sending a notification from Azure Notification Hub](./building-notifications-client-2.md)

So far I hadn't worked directly with [Azure Notification Hub](http://gslb.ch/d425b) (I was using App Center, [but this is being retired](http://gslb.ch/d423b)), so I need to figure out how the Notification Hub works. Thankfully there is good documentation available. Here is what we'll do in this step:

- Create a new Azure Notification Hub and connect it to FCM.
- Test the setup by sending a push notification [with Postman](https://www.postman.com/).

<!-- [Go to step 2: Sending a notification from Azure Notification Hub](./building-notifications-client-2.md) -->

### Step 3: Build the endpoint and connect to Notification Hub.

Once we have the FCM project and the Notification Hub set up, we can build an [Azure Function](http://gslb.ch/d10b) to serve as the main endpoint for the notification system. The Azure Function will communicate with the Azure Notification Hub by means of an SDK for .NET. This includes:

- Build the Azure Functions application locally.
- Build a test request in Postman to try things out.
- Create the Azure Functions application on Azure.
- Publish the function to the Azure Functions application.

> Not started yet

### Step: Sending a notification from the Windows Push Notification Services

> Not started yet.

### Step: Show the new notification in a page on the device.

> Not started yet.

### Step: Save the notifications in Cosmos DB.

> Not started yet.

### Step: Get the notifications from Cosmos DB into the app.

> Not started yet.

### Step: Save all the secrets to Azure KeyVault.

> Not started yet

### Step: Organize the notifications in the app in pages.

> Not started yet.

### Step: Save the notifications locally (synchronize).

> Not started yet.

### Step: Customize the application with icons, design etc...

> Not started yet.

## Building an ARM deployment template

In order to allow people to use this on their own, I am also planning to prepare an ARM deployment template for the Azure components.

- Azure Function connected to the Azure Functions application GitHub repo.
- CosmosDB.
- Notification Hub.
- KeyVault for the connection strings and other secrets.

In addition I will write detailed description on how to create the Firebase Cloud Messaging project, and how to configure all the Azure pieces.

**[Next page: Notifications Client Step 0: Specification](./step00.md)**

Happy coding!

Laurent