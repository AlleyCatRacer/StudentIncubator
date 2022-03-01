using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using StudentIncubator.Data.ServiceInterface;
using StudentIncubator.Models;

namespace StudentIncubator.Data.ServiceImp
{
    public class UserWebService : IUserService
    {
        public User CachedUser { get; set; }
        private readonly HttpClient client;
        private readonly string uri = "https://localhost:8080/Users";
        private JsonSerializerOptions camelCasing;
        private JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        
        public UserWebService()
        {
            client = new HttpClient();
            camelCasing = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }
        
        public async Task<User> ValidateLoginAsync(string username, string password)
        {
            var response = await client.GetAsync($"{uri}/{username}?password={password}");
           
            var userAsJson = await response.Content.ReadAsStringAsync();
            var resultUser = JsonSerializer.Deserialize<User>(userAsJson,options);

            if (response.StatusCode == HttpStatusCode.Conflict) throw new ArgumentException("Wrong password.");
            if (response.StatusCode != HttpStatusCode.OK) throw new Exception("User not found");
            
            CachedUser = resultUser;
            CachedUser.Online = true;
            await UpdateUserAsync(CachedUser);
            return resultUser;
        }

        public async Task<User> GetPublicUserAsync(string username)
        {
            var response = await client.GetAsync($"{uri}/{username}/Details");
           
            var userAsJson = await response.Content.ReadAsStringAsync();
            var resultUser = JsonSerializer.Deserialize<User>(userAsJson,options);

            if (response.StatusCode != HttpStatusCode.OK) throw new Exception("User not found");

            return resultUser;
        }

        public async Task<User> CreateAccountAsync(User newUser)
        {
            var userJson = JsonSerializer.Serialize(newUser, camelCasing);
            HttpContent content = new StringContent(userJson, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync(uri, content);
            
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception($"Error, {responseMessage.StatusCode}, {responseMessage.ReasonPhrase}");
            }

            return newUser;
        }

        public async Task<IList<User>> GetAllUsersAsync()
        {
            var response = await client.GetAsync(uri);
           
            var usersAsJson = await response.Content.ReadAsStringAsync();
            var resultUsers = JsonSerializer.Deserialize<IList<User>>(usersAsJson, options);
            
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error, {response.StatusCode}, {response.ReasonPhrase}");
            }

            return resultUsers;
        }
        
        public async Task<User> UpdateUserAsync(User user)
        {
            var userJson = JsonSerializer.Serialize(user, camelCasing);
            HttpContent content = new StringContent(userJson, Encoding.UTF8, "application/json");
            var responseMessage = await client.PatchAsync($"{uri}/{user.Username}", content);

            var userAsJson = await responseMessage.Content.ReadAsStringAsync();
            var resultUser = JsonSerializer.Deserialize<User>(userAsJson, options);
            
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception($"Error, {responseMessage.StatusCode}, {responseMessage.ReasonPhrase}");
            }

            if (resultUser != null && !resultUser.Password.Equals("Hug")) {
                CachedUser = resultUser;   
            }

            return resultUser;
        }
    }
}