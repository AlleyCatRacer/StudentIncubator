using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using WebAPI.DataMediator.IRepos;
using Action = WebAPI.Model.Action;

namespace WebAPI.DataMediator.ReposImp {
    public class ActionRepository : IActionRepository {
        
        private IConnection connection;
        private IModel channel;
        private string replyQueueName;
        private EventingBasicConsumer consumer;
        private readonly BlockingCollection<string> respQueue = new BlockingCollection<string>();
        private IBasicProperties props;
        private JsonSerializerOptions options = new JsonSerializerOptions 
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        
        public async Task CreateActionAsync(Action suggestedAction) {
            var actionJson = JsonSerializer.Serialize(suggestedAction, options);
            await SendRequestAsync("create", actionJson);
        }
        
        
        private async Task<string> SendRequestAsync(string type, string body)
        {
            RabbitMqSetup();

            var request = new DataRequest("action", type, body);
            var requestJson = JsonSerializer.Serialize(request);

            channel.BasicPublish("", "data_queue", props, Encoding.UTF8.GetBytes(requestJson));

            var response = respQueue.Take();
            connection.Close();
            return response;
        }

        private void RabbitMqSetup() 
        {
            var factory = new ConnectionFactory {HostName = "localhost"};

            connection = factory.CreateConnection();
            channel = connection.CreateModel();
            replyQueueName = channel.QueueDeclare().QueueName;
            consumer = new EventingBasicConsumer(channel);

            props = channel.CreateBasicProperties();
            var correlationId = Guid.NewGuid().ToString();
            props.CorrelationId = correlationId;
            props.ReplyTo = replyQueueName;

            consumer.Received += (model, ea) => 
            {
                var body = ea.Body.ToArray();
                var response = Encoding.UTF8.GetString(body);
                if (ea.BasicProperties.CorrelationId == correlationId) 
                {
                    respQueue.Add(response);
                }
            };

            channel.BasicConsume(consumer: consumer, queue: replyQueueName, autoAck: true);
        }
    }
}