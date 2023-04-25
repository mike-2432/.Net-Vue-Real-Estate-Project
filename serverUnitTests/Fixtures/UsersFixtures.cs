using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using server.Main.Models;

namespace serverUnitTests.Fixtures
{
  public class UsersFixtures
  {
    // Get a house
    public static User GetUserWithId1()
    {
      User user = new User
      {
          Id = 1,
          Username = "testUser",
          Email = "test@mail.nl"
      };
      return user;
    }
  }
}