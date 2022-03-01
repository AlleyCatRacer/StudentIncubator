package mediator.Command.Feedback;

import com.google.gson.Gson;
import db.DAO.Feedback.SuggestionDAO;
import mediator.Command.CommandList;

public class SuggestionCommand implements CommandList {

    private SuggestionDAO suggestionDAO;
    private Gson json;

    public SuggestionCommand() {
        suggestionDAO = new SuggestionDAO();
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
        String[] suggestion = json.fromJson(jsonObject, String[].class);
        suggestionDAO.create(suggestion[0], suggestion[1]);
        return "Suggestion received";
    }

    @Override
    public String update(String jsonObject) {
        return null;
    }

    @Override
    public void delete(String jsonObject) {

    }
}
