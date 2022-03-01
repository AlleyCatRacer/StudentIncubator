import db.DAO.AvatarDAO;
import db.DAO.UserDAO;
import model.Status;
import org.junit.jupiter.api.*;

import static org.junit.jupiter.api.Assertions.*;

public class AvatarTest
{
  private UserDAO userDao;
  private AvatarDAO avatarDao;

  @BeforeEach public void setUp()
  {
    userDao = new UserDAO();
    avatarDao = new AvatarDAO();
    if (userDao.readOne("testUser")!=null)
    {
     tearDown();
    }
    
    userDao.create("testUser", "testPassword",null, 0, false, false);
  }

  @AfterEach public void tearDown()
  {
    userDao.delete("testUser");
  }

  //test for uncreated avatar
  @Test public void createAvatar_Zero()
  {
    assertNull(avatarDao.readOne("testUser"));
  }

  //test for avatar with standard values
  @Test public void createAvatar_One()
  {
    avatarDao.create("testUser", "testAvatar", null,0);
    assertEquals("testAvatar", avatarDao.readOne("testUser").getAvatarName());
    assertEquals("testUser", avatarDao.readOne("testUser").getOwner());
  }

  //test for avatar name with null value and with too many characters
  @Test public void createAvatar_Exceptions()
  {
    //checked for at API
     assertThrows(RuntimeException.class, () -> avatarDao.create("testUser", null, null, 0));
     assertThrows(RuntimeException.class, () -> avatarDao.create("testUser", "testAvatar1testAvatar2testAvatar3testAvatar4", null,0));
  }

  //test for avatar created with chosen status values
  //creating new avatar always defaults status to 100,100,100,100
  @Test public void createAvatar_Status()
  {
    avatarDao.create("testUser", "testAvatar", new Status(0,-1,50,100000000),800);
    assertEquals(100,avatarDao.readOne("testUser").getAcademic());
     assertEquals(100,avatarDao.readOne("testUser").getFinancial());
     assertEquals(100,avatarDao.readOne("testUser").getHealth());
    assertEquals(100,avatarDao.readOne("testUser").getSocial());
    assertEquals(80,avatarDao.readOne("testUser").getTimeBlock());
  }
}
