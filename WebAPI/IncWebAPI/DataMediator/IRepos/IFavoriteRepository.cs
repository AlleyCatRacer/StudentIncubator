using System.Threading.Tasks;

namespace WebAPI.DataMediator.IRepos {
    public interface IFavoriteRepository {

        Task AddFavoriteAsync(string[] favorite);

        Task RemoveFavoriteAsync(string[] favorite);

        Task<string[]> FindFavoritesAsync(string username);
    }
}