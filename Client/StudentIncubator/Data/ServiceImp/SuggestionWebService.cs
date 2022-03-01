using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using StudentIncubator.Data.ServiceInterface;

namespace StudentIncubator.Data.ServiceImp {
    public class SuggestionWebService: ISuggestionService {
        
        private readonly HttpClient client;
        private readonly string uri = "https://localhost:8080/Suggestion";

        public SuggestionWebService() {
            client = new HttpClient();
        }
        
        public async Task SaveSuggestionAsync(string username, string suggestion) {
            string[] values = {username, suggestion};
            var valuesJson = JsonSerializer.Serialize(values);
            HttpContent content = new StringContent(valuesJson, Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{uri}", content);
            
            if (!response.IsSuccessStatusCode) {
                throw new Exception($"Error, {response.StatusCode}, {response.ReasonPhrase}");
            }
        }
    }
}