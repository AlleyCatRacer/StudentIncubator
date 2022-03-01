package mediator.Command;

import com.google.gson.Gson;
import db.DAO.AvatarDAO;
import model.Avatar;
import model.Status;

public class AvatarCommand implements CommandList {

    private AvatarDAO avatarDAO;
    private Gson json;

    public AvatarCommand() {
        avatarDAO = new AvatarDAO();
        json = new Gson();
    }

    @Override
    public String findById(String username) {
        return json.toJson(avatarDAO.readOne(username));
    }

    @Override
    public String findAll() {
        return json.toJson(avatarDAO.readAll());
    }

    @Override
    public String create(String jsonObject) {
        Avatar a = json.fromJson(jsonObject, Avatar.class);
        Status status = new Status(100, 100, 100, 100);
        return json.toJson(avatarDAO.create(a.getOwner(), a.getAvatarName(), status, a.getTimeBlock()));
    }
    
    @Override public String update(String jsonObject)
    {
        Avatar a = json.fromJson(jsonObject, Avatar.class);
        return json.toJson(avatarDAO.update(a));
    }

    @Override
    public void delete(String jsonObject) {
        avatarDAO.delete(jsonObject);
    }
}
