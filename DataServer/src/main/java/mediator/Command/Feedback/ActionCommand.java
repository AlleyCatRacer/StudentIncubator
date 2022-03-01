package mediator.Command.Feedback;

import com.google.gson.Gson;
import db.DAO.Feedback.ActionDAO;
import mediator.Command.CommandList;
import model.Action;

public class ActionCommand implements CommandList {

    private ActionDAO actionDAO;
    private Gson json;

    public ActionCommand() {
        actionDAO = new ActionDAO();
        json = new Gson();
    }

    @Override
    public String findById(String username) {
        return null;
    }

    @Override
    public String findAll() {
        return null;
    }

    @Override
    public String create(String jsonObject) {

        Action a = json.fromJson(jsonObject, Action.class);
        actionDAO.create(a);
        return "Action suggestion persisted";
    }

    @Override
    public String update(String jsonObject) {
        return null;
    }

    @Override
    public void delete(String jsonObject) {

    }
}
