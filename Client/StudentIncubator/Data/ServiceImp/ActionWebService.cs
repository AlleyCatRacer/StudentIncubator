using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using StudentIncubator.Data.ServiceInterface;
using Action = StudentIncubator.Models.Action;

namespace StudentIncubator.Data.ServiceImp {
    public class ActionWebService : IActionService {
        
        private readonly HttpClient client;
        private readonly string uri = "https://localhost:8080/Action";

        public ActionWebService() {
            client = new HttpClient();
        }
        
        public async Task SaveActionAsync(Action suggestedAction) {
            
            var actionJson = JsonSerializer.Serialize(suggestedAction);
            HttpContent content = new StringContent(actionJson, Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{uri}", content);
            
            if (!response.IsSuccessStatusCode) {
                throw new Exception($"Error, {response.StatusCode}, {response.ReasonPhrase}");
            }
        }
    }
}