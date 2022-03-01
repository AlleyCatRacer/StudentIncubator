using System.Threading.Tasks;

namespace StudentIncubator.Data.ServiceInterface {
    public interface ISuggestionService {

        Task SaveSuggestionAsync(string username, string suggestion);
    }
}