package db.DAO;

import db.DataMapper;
import db.DatabaseHelper;
import java.sql.ResultSet;
import java.sql.SQLException;

public class FavoriteDAO
{
	private DatabaseHelper<String> helper;
	
	public FavoriteDAO()
	{
		helper = new DatabaseHelper<>();
	}
	
	private static class FavoriteMapper implements DataMapper<String>
	{
		public String create(ResultSet rs) throws SQLException
		{
			return rs.getString("favorite");
		}
	}
	
	public String create(String username, String favorite)
	{
		helper.executeUpdate("INSERT INTO favorites VALUES (?, ?)", username, favorite);
		return "Favorite added.";
	}
	
	public String[] getById(String username)
	{
		Object[] array = helper.map(new FavoriteMapper(),
				"SELECT favorite FROM favorites WHERE username = ?", username).toArray();
		String[] favorites = new String[array.length];
		for (int i = 0; i < array.length; i++)
		{
			favorites[i] = array[i].toString();
		}
		
		return favorites;
	}
	
	public void delete(String username, String favorite)
	{
		helper.executeUpdate("DELETE FROM favorites WHERE username = ? AND favorite = ?", username, favorite);
	}
}
