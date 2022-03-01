using System.Threading.Tasks;
using WebAPI.Model;

namespace WebAPI.DataMediator.IRepos {
    public interface IActionRepository {

        Task CreateActionAsync(Action suggestedAction);
    }
}