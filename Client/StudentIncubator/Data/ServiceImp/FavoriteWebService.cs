using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using StudentIncubator.Data.ServiceInterface;

namespace StudentIncubator.Data.ServiceImp {
    public class FavoriteWebService : IFavoriteService {
        
        private readonly HttpClient client;
        private readonly string uri = "https://localhost:8080/Favorite";

        public FavoriteWebService() {
            client = new HttpClient();
        }
        
        public async Task<string> AddFavoriteAsync(string username, string favorite) {
            string[] values = {username, favorite};
            var valuesJson = JsonSerializer.Serialize(values);
            HttpContent content = new StringContent(valuesJson, Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{uri}", content);
            
            if (!response.IsSuccessStatusCode) {
                throw new Exception($"Error, {response.StatusCode}, {response.ReasonPhrase}");
            }

            return "Favorite added.";
        }

        public async Task<string> RemoveFavoriteAsync(string username, string nonFavorite) {

            var responseMessage = await client.DeleteAsync($"{uri}/{username}?favorite={nonFavorite}");

            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception(responseMessage.ReasonPhrase);
            }

            return "Favorite removed.";
        }

        public async Task<string[]> GetFavoritesAsync(string username) {

            var response = await client.GetAsync($"{uri}/{username}");
            
            if (!response.IsSuccessStatusCode) {
                throw new Exception("No favorites to display.");
            }
            var jsonFavorites = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<string[]>(jsonFavorites);
        }
    }
}