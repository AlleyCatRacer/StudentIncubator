using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using StudentIncubator.Data.ServiceInterface;
using StudentIncubator.Models;

namespace StudentIncubator.Data.ServiceImp
{
    public class AvatarWebService :IAvatarService
    {
        private readonly HttpClient client;
        private readonly string uri = "https://localhost:8080/Avatars";
        private JsonSerializerOptions camelCasing;
        public Avatar CachedAvatar { get; set; }

        public AvatarWebService()
        {
            client = new HttpClient();
            camelCasing = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        public async Task<Avatar> CreateAvatarAsync(string owner, string avatarName)
        {
            var newAvatar = new Avatar(owner, avatarName);
            var avatarJson = JsonSerializer.Serialize(newAvatar, camelCasing);
            HttpContent content = new StringContent(avatarJson, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync($"{uri}/{owner}", content);
            
            if (!responseMessage.IsSuccessStatusCode)
            {
                if (responseMessage.StatusCode == HttpStatusCode.BadRequest)
                {
                    throw new Exception("Invalid Avatar Name");
                }
                throw new Exception(responseMessage.ReasonPhrase);
            }

            CachedAvatar = newAvatar;

            return newAvatar;
        }

        public async Task<Avatar> GetAvatarAsync(string owner)
        {
            var responseMessage = await client.GetAsync($"{uri}?owner={owner}");

            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception("Please create an avatar.");
            }

            var jsonAvatar = await responseMessage.Content.ReadAsStringAsync();

            Avatar returnedAvatar = null;

            try
            {
                returnedAvatar = JsonSerializer.Deserialize<Avatar>(jsonAvatar, new JsonSerializerOptions{PropertyNameCaseInsensitive = true});
            }
            catch (Exception e)
            {
                CachedAvatar = null;
                throw new Exception("No avatar to display.");
            }

            CachedAvatar = returnedAvatar;
            
            return returnedAvatar;
        }
        
        public async Task<Avatar> UpdateAvatarAsync(Avatar avatar, string key)
        {
            var avatarJson = JsonSerializer.Serialize(avatar);
            HttpContent content = new StringContent(avatarJson, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await client.PatchAsync($"{uri}/{avatar.Owner}?key={key}", content);
            if (!responseMessage.IsSuccessStatusCode) {
                throw new Exception(responseMessage.Content.ReadAsStringAsync().Result);
            }
            
            var jsonAvatar = await responseMessage.Content.ReadAsStringAsync();

            CachedAvatar = JsonSerializer.Deserialize<Avatar>(jsonAvatar, new JsonSerializerOptions{PropertyNameCaseInsensitive = true});

            return CachedAvatar;
        }

        public async Task DeleteAvatarAsync(string owner)
        {
            var responseMessage = await client.DeleteAsync($"{uri}/{owner}");

            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception(responseMessage.ReasonPhrase);
            }
            
            CachedAvatar = null;
        }
    }
}