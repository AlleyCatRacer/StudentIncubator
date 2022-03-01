package mediator.Command;

import com.google.gson.Gson;
import db.DAO.UserDAO;
import model.User;

public class UserCommand implements CommandList {

    private UserDAO userDAO;
    private Gson json;

    public UserCommand() {
        userDAO = new UserDAO();
        json = new Gson();
    }
    
    @Override
    public String findById(String username) {
        return json.toJson(userDAO.readOne(username));
    }

    @Override
    public String findAll() {
        return json.toJson(userDAO.readAll());
    }

    @Override
    public String create(String jsonObject) {
        User u = json.fromJson(jsonObject, User.class);
        return json.toJson(userDAO.create(u.getUsername(), u.getPassword(), u.getBio(), u.getHighscore(),
                u.getOnline(), u.hasHug()));
    }

    @Override public String update(String jsonObject)
    {
        User u = json.fromJson(jsonObject, User.class);

        return json.toJson(userDAO.update(u));
    }

    @Override
    public void delete(String jsonObject) {
        User u = json.fromJson(jsonObject, User.class);
        userDAO.delete(u.getUsername());
    }
}
