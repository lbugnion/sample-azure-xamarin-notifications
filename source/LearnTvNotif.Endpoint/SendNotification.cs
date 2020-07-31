using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.NotificationHubs;
using System.Collections.Generic;

namespace LearnTvNotif.Endpoint
{
    public static class SendNotification
    {
        [FunctionName("SendNotification")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(
                AuthorizationLevel.Function, 
                "post", 
                Route = "send")] 
            HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            string body = data.body;
            string title = data.title;

            if (string.IsNullOrEmpty(body)
                || string.IsNullOrEmpty(title))
            {
                return new BadRequestObjectResult(
                    "Please pass a body and a title in the request");
            }

            var connectionString = Environment.GetEnvironmentVariable("HubConnectionString");
            var hubName = Environment.GetEnvironmentVariable("HubName");

            var hub = NotificationHubClient.CreateClientFromConnectionString(
                connectionString,
                hubName);

            var properties = new Dictionary<string, string>
            {
                {
                    "body",
                    body
                },
                {
                    "title",
                    title
                }
            };

            var outcome = await hub
                .SendTemplateNotificationAsync(properties);

            string result;

            if (outcome.State == NotificationOutcomeState.Completed)
            {
                if (outcome.Success > 0)
                {
                    result = $"Sent notification to {outcome.Success} devices";
                }
                else
                {
                    result = "Notification was sent to 0 device";
                }
            }
            else if (outcome.State == NotificationOutcomeState.Enqueued)
            {
                result = "Notification enqueued for sending";
            }
            else
            {
                result = "Couldn't complete the operation";
            }

            return new OkObjectResult(result);
        }
    }
}
