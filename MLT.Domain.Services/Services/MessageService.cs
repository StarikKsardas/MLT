using MLT.Domain.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;
using Newtonsoft.Json;

namespace MLT.Domain.Services.Services
{
    public class MessageService : IMessageService
    {       

        public void SendMessage<T>(T message, string queueName)
        {
            var connectionFactory = new ConnectionFactory()
            {
                HostName = "localhost",
                Port = 5672
                
            };
            var connection = connectionFactory.CreateConnection();
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: queueName, durable: false, exclusive: true);
                var json = JsonConvert.SerializeObject(message);
                var body = Encoding.UTF8.GetBytes(json);
                channel.BasicPublish(exchange: "", routingKey: queueName, body: body);
            }
        }
    }
}
