﻿using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mango.MessageBus
{
    public class MessageBus : IMessageBus
    {
        //private string connectionString = "Endpoint=sb://mangowebsrc.servicebus.windows.net/;SharedAccessKeyName=SharedAccessPolicy1;SharedAccessKey=fCptc11m9RUTyShSAgQL5k2a41jxYgeOs+ASbBODlj8=;EntityPath=emailshoppingcart";

        private string connectionString = "Endpoint=sb://mangowebsrc.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=yg5AwjSUlxbYKExXOxLRC3YqZCOYqpuGY+ASbM0KDpw=";
        public async Task PublishMessage(object message, string topic_queue_Name)
        {
            await using var client = new ServiceBusClient(connectionString);

            ServiceBusSender sender = client.CreateSender(topic_queue_Name);

            var jsonMessage = JsonConvert.SerializeObject(message);
            ServiceBusMessage finalMessage = new ServiceBusMessage(Encoding
                .UTF8.GetBytes(jsonMessage))
            {
                CorrelationId = Guid.NewGuid().ToString(),
            };

            await sender.SendMessageAsync(finalMessage);
            await client.DisposeAsync();
        }
    }
}
