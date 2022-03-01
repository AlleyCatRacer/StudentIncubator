package mediator;

import com.google.gson.Gson;
import mediator.Command.AvatarCommand;
import mediator.Command.CommandList;
import mediator.Command.FavoriteCommand;
import mediator.Command.Feedback.ActionCommand;
import mediator.Command.Feedback.SuggestionCommand;
import mediator.Command.UserCommand;

import java.util.HashMap;

public class RequestHandler
{
  private Gson json;
  private HashMap<String, CommandList> commandSet;
  private HashMap<String, Runnable> filter;
  private String reqObj;
  private String reqType;
  private String reqBod;
  private String response;

  public RequestHandler()
  {
    this.json = new Gson();
    commandSet = new HashMap<>();
    filter = new HashMap<>();

    commandSet.put("user", new UserCommand());
    commandSet.put("avatar", new AvatarCommand());
    commandSet.put("favorite", new FavoriteCommand());
    commandSet.put("suggestion", new SuggestionCommand());
    commandSet.put("action", new ActionCommand());

    filter.put("findById", this::findById);
    filter.put("findAll", this::findAll);
    filter.put("create", this::create);
    filter.put("update", this::update);
    filter.put("delete", this::delete);
  }

  public String filterRequest(String requestJson)
  {
    DataRequest request = json.fromJson(requestJson, DataRequest.class);
    reqObj = request.getObject();
    reqBod = request.getBody();
    reqType = request.getType();

    response = "";
    filter.get(reqType).run();
    return response;
  }

  private void findById() { response = commandSet.get(reqObj).findById(reqBod); }

  private void findAll()
  {
    response = commandSet.get(reqObj).findAll();
  }

  private void create()
  {
    response = commandSet.get(reqObj).create(reqBod);
  }

  private void update()
  {
    response = commandSet.get(reqObj).update(reqBod);
  }

  private void delete() { commandSet.get(reqObj).delete(reqBod); }
}