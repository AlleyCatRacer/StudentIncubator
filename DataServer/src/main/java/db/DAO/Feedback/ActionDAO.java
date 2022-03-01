package db.DAO.Feedback;

import db.DatabaseHelper;
import model.Action;

public class ActionDAO {

    private DatabaseHelper<String> helper;

    public ActionDAO() {
        helper = new DatabaseHelper<>();
    }

    public void create(Action a)  {
        helper.executeUpdate("INSERT INTO suggested_actions VALUES (?, ?, ?, ?, ?, ?, ?)",
                a.getCreator(), a.getActivity(), a.getAcademic(), a.getFinancial(), a.getHealth(), a.getSocial(), a.getTimeCost());
    }
}
