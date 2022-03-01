using System.Threading.Tasks;
using StudentIncubator.Models;

namespace StudentIncubator.Data.ServiceInterface
{
    public interface IAvatarService
    {
        Avatar CachedAvatar { get; set; }
        Task<Avatar> CreateAvatarAsync(string owner, string avatarName);
        Task<Avatar> GetAvatarAsync(string owner);
        Task<Avatar> UpdateAvatarAsync(Avatar avatar, string actionType);
        Task DeleteAvatarAsync(string owner);
    }
}