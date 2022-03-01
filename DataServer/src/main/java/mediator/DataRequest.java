package mediator;

public class DataRequest
{
  private String Object;
  private String Type;
  private String Body;

  public DataRequest(String object, String type, String body)
  {
    this.Object = object;
    this.Type = type;
    this.Body = body;
  }

  public String getObject()
  {
    return Object;
  }
  
  public String getType()
  {
    return Type;
  }

  public String getBody()
  {
    return Body;
  }
}
