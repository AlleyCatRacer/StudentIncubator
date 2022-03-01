using System.Threading.Tasks;

namespace WebAPI.DataMediator.IRepos {
    public interface ISuggestionRepository {

        Task CreateSuggestionAsync(string[] suggestion);
    }
}