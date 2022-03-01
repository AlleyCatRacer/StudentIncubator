package db;

import org.postgresql.Driver;

import java.sql.*;
import java.util.LinkedList;
import java.util.List;

public class DatabaseHelper<T> {
	private final String jdbcURL = "jdbc:postgresql://localhost:5432/postgres?currentSchema=student_incubator";
	private final String username = "postgres";
	private final String password = "mypassword";
	
	public DatabaseHelper() {
		try {
			DriverManager.registerDriver(new Driver());
		} catch (SQLException e) {
			throw new RuntimeException("No JDBC driver", e);
		}
	}

	protected Connection getConnection() throws SQLException {
		if (username == null) {
			return DriverManager.getConnection(jdbcURL);
		} else {
			return DriverManager.getConnection(jdbcURL, username, password);
		}
	}

	private PreparedStatement prepare(Connection connection, String sql, Object[] parameters) throws SQLException {
		PreparedStatement stat = connection.prepareStatement(sql);
		for(int i = 0; i < parameters.length; i++) {
			stat.setObject(i + 1, parameters[i]);
		}
		return stat;
	}

	public int executeUpdate(String sql, Object... parameters) {
		try (Connection connection = getConnection()) {
			PreparedStatement stat = prepare(connection, sql, parameters);
			return stat.executeUpdate();
		} catch (SQLException e) {
			throw new RuntimeException(e.getMessage(), e);
		}
	}
	
	public T mapSingle(DataMapper<T> mapper, String sql, Object... parameters) {
		try (Connection connection = getConnection()) {
			PreparedStatement stat = prepare(connection, sql, parameters);
			ResultSet rs = stat.executeQuery();
			if(rs.next()) {
				return mapper.create(rs);
			} else {
				return null;
			}
		} catch (SQLException e) {
			throw new RuntimeException(e.getMessage(), e);
		}
	}
	
	public void updateSingle(String sql, Object... parameters)
	{
		try (Connection connection = getConnection())
		{
			PreparedStatement stat = prepare(connection, sql, parameters);
			stat.executeUpdate();

		}
		catch (SQLException e)
		{
			throw new RuntimeException(e.getMessage(), e);
		}
	}

	public List<T> map(DataMapper<T> mapper, String sql, Object... parameters) {
		try (Connection connection = getConnection()) {
			PreparedStatement stat = prepare(connection, sql, parameters);
			ResultSet rs = stat.executeQuery();
			LinkedList<T> allUsers = new LinkedList<>();
			while(rs.next()) {
				allUsers.add(mapper.create(rs));
			}
			return allUsers;
		} catch (SQLException e) {
			throw new RuntimeException(e.getMessage(), e);
		}
	}
}
