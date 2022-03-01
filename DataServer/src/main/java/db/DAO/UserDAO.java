package db.DAO;

import db.DataMapper;
import db.DatabaseHelper;
import model.User;

import java.sql.ResultSet;
import java.sql.SQLException;

public class UserDAO
{
	private DatabaseHelper<User> helper;

	public UserDAO()
	{
		helper = new DatabaseHelper<>();
	}

	public User create(String username, String password, String bio, int highscore, boolean online, boolean hasHug)
	{
		helper.executeUpdate("INSERT INTO student_user VALUES (?, ?, ?, ?, ?, ?)", username,
				password, bio, highscore, online, hasHug);
		return new User(username, password, bio, highscore, online, hasHug);
	}

	private static class UserMapper implements DataMapper<User>
	{
		public User create(ResultSet rs) throws SQLException
		{
			String username = rs.getString("username");
			String password = rs.getString("password");
			String bio = rs.getString("bio");
			int highscore = rs.getInt("highscore");
			boolean online = rs.getBoolean("online");
			boolean hasHug = rs.getBoolean("has_hug");
			return new User(username, password, bio, highscore, online, hasHug);
		}
	}

	public User[] readAll()
	{
		return helper.map(new UserMapper(), "SELECT * FROM student_user")
				.toArray(new User[0]);
	}

	public User readOne(String username)
	{
		return helper.mapSingle(new UserMapper(),
				"SELECT * FROM student_user WHERE username = ?", username);
	}
	
	public User update(User u)
	{
		if (u.getPassword() == null)
		{
			helper.updateSingle("UPDATE student_user SET bio = ?, highscore = ?, online = ? WHERE username = ?",
					u.getBio(), u.getHighscore(), u.getOnline(), u.getUsername());
		}
		if(u.getPassword().equals("Hug"))
		{
			helper.updateSingle("UPDATE student_user SET has_hug = ? WHERE username = ?", u.hasHug(), u.getUsername());
		}
		else
		{
			helper.updateSingle("UPDATE student_user SET password = ?, bio = ?, online = ? WHERE username = ?",
					u.getPassword(), u.getBio(), u.getOnline(), u.getUsername());
		}

		return readOne(u.getUsername());
	}

	public void delete(String username)
	{
		helper.executeUpdate("DELETE FROM avatar WHERE username = ?", username);
		helper.executeUpdate("DELETE FROM student_user WHERE username = ?", username);
	}
}
