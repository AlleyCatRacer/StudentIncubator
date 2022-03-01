using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Model;

namespace WebAPI.DataMediator.IRepos {
    public interface IUserRepository {

        Task<IList<User>> GetAllUsersAsync();
        Task<User> GetUserAsync(string username);
        Task<User> GetPublicUserAsync(string username);
        Task<User> SaveAsync(User user);
        Task<User> UpdateUserAsync(User user);
    }
}