package mediator.Command;

import com.google.gson.Gson;
import db.DAO.FavoriteDAO;

public class FavoriteCommand implements CommandList
{
	FavoriteDAO favoriteDAO;
	private Gson json;
	
	public FavoriteCommand()
	{
		favoriteDAO = new FavoriteDAO();
		json = new Gson();
	}
	
	@Override public String findById(String username)
	{
		return json.toJson(favoriteDAO.getById(username));
	}
	
	@Override public String findAll()
	{
		return null;
	}
	
	@Override public String create(String jsonObject)
	{
		String[] favorite = json.fromJson(jsonObject, String[].class);
		return favoriteDAO.create(favorite[0], favorite[1]);
	}
	
	@Override public String update(String jsonObject)
	{
		return null;
	}
	
	@Override public void delete(String jsonObject)
	{
		String[] notFavorite = json.fromJson(jsonObject, String[].class);
		favoriteDAO.delete(notFavorite[0], notFavorite[1]);
	}
}
