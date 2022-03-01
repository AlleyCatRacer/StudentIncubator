using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
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
    public class UserRepository : IUserRepository
    {
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
        
        
        public async Task<IList<User>> GetAllUsersAsync() {
            var users = await SendRequestAsync("findAll", "");
            IList<User> usersList = JsonSerializer.Deserialize<IList<User>>(users, options);
            if (usersList is not {Count: > 0}) return usersList;
            foreach (var user in usersList) {
                user.Password = "";
            }
            return usersList;
        }

        public async Task<User> GetUserAsync(string username)
        {
            var resultJson = await SendRequestAsync("findById", username);
            return JsonSerializer.Deserialize<User>(resultJson, options);
        }

        public async Task<User> GetPublicUserAsync(string username)
        {
            var user = await GetUserAsync(username);
            user.Password = null;
            return user;
        }

        public async Task<User> SaveAsync(User user)
        {
            var userJson = JsonSerializer.Serialize(user, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            var resultJson = await SendRequestAsync("create", userJson);

            return JsonSerializer.Deserialize<User>(resultJson, options);
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            var userJson = JsonSerializer.Serialize(user, options);
            var resultJson = await SendRequestAsync("update", userJson);
            return JsonSerializer.Deserialize<User>(resultJson, options);
        }

        private async Task<string> SendRequestAsync(string type, string body)
        {
            RabbitMqSetup();

            var request = new DataRequest("user", type, body);
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