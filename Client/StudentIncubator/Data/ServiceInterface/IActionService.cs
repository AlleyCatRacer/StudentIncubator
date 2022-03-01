using System.Threading.Tasks;
using StudentIncubator.Models;

namespace StudentIncubator.Data.ServiceInterface {
    public interface IActionService {

        Task SaveActionAsync(Action suggestedAction);
    }
}