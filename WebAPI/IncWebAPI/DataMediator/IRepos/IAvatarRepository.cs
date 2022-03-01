using System.Threading.Tasks;
using WebAPI.Model;

namespace WebAPI.DataMediator.IRepos
{
    public interface IAvatarRepository
    {
        Task<Avatar> GetAvatarAsync(string owner);
        Task<Avatar[]> GetAllAvatarsAsync();
        Task<Avatar> CreateAvatarAsync(Avatar newAvatar);
        Task<Avatar> UpdateAvatarAsync(Avatar avatar);
        Task DeleteAvatarAsync(string owner);
    }
}