using System.Threading.Tasks;

namespace StudentIncubator.Data.ServiceInterface {
    public interface IFavoriteService {

        Task<string> AddFavoriteAsync(string username, string favorite);

        Task<string> RemoveFavoriteAsync(string username, string nonFavorite);

        Task<string[]> GetFavoritesAsync(string username);
    }
}