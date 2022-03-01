package db.DAO;

import db.DataMapper;
import db.DatabaseHelper;
import model.Avatar;
import model.Status;

import java.sql.ResultSet;
import java.sql.SQLException;

public class AvatarDAO
{
	private DatabaseHelper<Avatar> helper;
	
	public AvatarDAO()
	{
		helper = new DatabaseHelper<>();
	}
	
	public Avatar create(String username, String name, Status status, int timeBlock)  {
		helper.executeUpdate("INSERT INTO avatar VALUES (?, ?)", username, name);
		return new Avatar(username, name, status, timeBlock);
	}

	private static class AvatarMapper implements DataMapper<Avatar>
	{
		public Avatar create(ResultSet rs) throws SQLException
		{
			String username = rs.getString("username");
			String name = rs.getString("name");
			int academic = rs.getInt("academic");
			int financial = rs.getInt("financial");
			int health = rs.getInt("health");
			int social = rs.getInt("social");
			int time = rs.getInt("time");
			Status status = new Status(academic, financial, health, social);
			return new Avatar(username, name, status, time);
		}
	}
	
	public Avatar[] readAll() {
		return helper.map(new AvatarDAO.AvatarMapper(), "SELECT * FROM avatar").toArray(new Avatar[0]);
	}
	
	public Avatar readOne(String username) {
		return helper.mapSingle(new AvatarDAO.AvatarMapper(), "SELECT * FROM avatar WHERE username = ?", username);
	}
	
	public Avatar update(Avatar avatar)
	{
		helper.updateSingle("UPDATE avatar SET academic = ?, financial = ?, health = ?, social = ?, time = ? WHERE " +
									"username = ?",
				avatar.getAcademic(), avatar.getFinancial(), avatar.getHealth(), avatar.getSocial(),
			avatar.getTimeBlock(), avatar.getOwner());
		Avatar a = readOne(avatar.getOwner());
		return a;
	}
	
	public void delete(String username) {
		helper.executeUpdate("DELETE FROM avatar WHERE username = ?", username);
	}
}
