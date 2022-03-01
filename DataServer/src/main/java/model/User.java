package model;

public class User
{
  private String username;
  private String password;
  private String bio;
  public int highscore;
  public boolean online;
  private boolean hasHug;
  
  public User(String username, String password, String bio, int highscore, boolean online, boolean hasHug)
  {
    this.username = username;
    this.password = password;
    this.bio = bio;
    this.highscore = highscore;
    this.online = online;
    this.hasHug = hasHug;
  }

  public String getUsername()
  {
    return username;
  }

  public String getPassword()
  {
    return password;
  }
  
  public String getBio(){return bio;}
  
  public int getHighscore()
  {
    return highscore;
  }
  
  public boolean getOnline()
  {
    return online;
  }
  
  public boolean hasHug()
  {
    return hasHug;
  }
}
