using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using WebAPI.DataMediator;
using WebAPI.DataMediator.IRepos;
using WebAPI.Model;

namespace WebAPI.DataMediator.ReposImp
{
    public class AvatarRepository : IAvatarRepository
    {
        private IConnection connection;
        private IModel channel;
        private string replyQueueName;
        private EventingBasicConsumer consumer;
        private BlockingCollection<string> respQueue = new BlockingCollection<string>();
        private IBasicProperties props;
        private JsonSerializerOptions options = new JsonSerializerOptions 
                {
                    PropertyNameCaseInsensitive = true //deserialization case insensitive - different naming conventions
                };

        
        public async Task<Avatar> GetAvatarAsync(string owner)
        {
            var resultJson = await SendRequestAsync("findById", owner);
            return JsonSerializer.Deserialize<Avatar>(resultJson, options);
        }

        public async Task<Avatar[]> GetAllAvatarsAsync() {
            var resultJson = await SendRequestAsync("findAll", "");
            return JsonSerializer.Deserialize<Avatar[]>(resultJson, options);
        }
        
        public async Task<Avatar> CreateAvatarAsync(Avatar newAvatar)
        {
            var avatarJson = JsonSerializer.Serialize(newAvatar);
            var resultJson = await SendRequestAsync("create", avatarJson);
            return JsonSerializer.Deserialize<Avatar>(resultJson, options);
        }

        public async Task<Avatar> UpdateAvatarAsync(Avatar avatar)
        {
            var avatarJson = JsonSerializer.Serialize(avatar);
            var resultJson = await SendRequestAsync("update", avatarJson);
            return JsonSerializer.Deserialize<Avatar>(resultJson, options);
        }

        public async Task DeleteAvatarAsync(string owner)
        {
            await SendRequestAsync("delete", owner);
        }

        private async Task<string> SendRequestAsync(string type, string body) 
        {
            RabbitMqSetup();

            DataRequest request = new DataRequest("avatar", type, body);
            string requestJson = JsonSerializer.Serialize(request);

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