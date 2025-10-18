using Azure;
using Azure.Messaging.EventGrid;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Data;

namespace OliEngine.EventGrid
{
    internal class OliEventGrid
    {
        static readonly Lazy<EventGridPublisherClient> client = new Lazy<EventGridPublisherClient>(() =>
        new EventGridPublisherClient(
             new Uri(ConfigurationManager.AppSettings["EventGridEndpoint"]),
             new AzureKeyCredential(ConfigurationManager.AppSettings["EventGridKey"])
            ));

        internal static void SendEvent(EventType eventType, DataRow dataRow)
        {
      

            var json = JsonConvert.SerializeObject(dataRow.Table);

            // Add EventGridEvents to a list to publish to the topic
            EventGridEvent egEvent =
                new EventGridEvent(
                    $"{dataRow.Table.TableName}-{ConfigurationManager.AppSettings["slot"]}",
                    eventType.Value,
                    "1.0",
                    json);
            

            // Send the event, TODO: send it async
            client.Value.SendEvent(egEvent);
        }
    }
}
