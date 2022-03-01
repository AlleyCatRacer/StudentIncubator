package db.DAO.Feedback;

import db.DatabaseHelper;

public class SuggestionDAO {

    private DatabaseHelper<String> helper;

    public SuggestionDAO() {
        helper = new DatabaseHelper<>();
    }

    public void create(String username, String suggestion)  {
        helper.executeUpdate("INSERT INTO suggestions VALUES (?, ?)", username, suggestion);
    }
}
