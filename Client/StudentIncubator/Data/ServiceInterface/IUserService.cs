using System.Collections.Generic;
using System.Threading.Tasks;
using StudentIncubator.Models;

namespace StudentIncubator.Data.ServiceInterface
{
    public interface IUserService
    {
        User CachedUser { get; set; }
        Task<User> ValidateLoginAsync(string username, string password);
        Task<User> GetPublicUserAsync(string username);
        Task<User> CreateAccountAsync(User newUser);
        Task<IList<User>> GetAllUsersAsync();
        Task<User> UpdateUserAsync(User user);
    }
}