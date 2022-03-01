package mediator.Command;

public interface CommandList {

    String findById(String username);

    String findAll();

    String create(String jsonObject);
    
    String update(String jsonObject);

    void delete(String jsonObject);
}
