using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using server.Main.Models;

namespace serverUnitTests.Fixtures
{
  public class HousesFixtures
  {
    // Get a house
    public static House GetSingleHouseWithUserId1()
    {
      House house = new House
      {
        Id = 1,
        StreetName = "test street",
        HouseNumber = 1,
        Addition = "B",
        City = "Rotterdam",
        Zip = "1000 AA",
        Bedrooms = 3,
        Bathrooms = 2,
        Size = 100,
        Price = 500000,
        HasGarage = true,
        ConstructionYear = 1990,
        Description = "A test description",
        User = new User()
        {
          Id = 1,
          Username = "testUser",
          Email = "test@mail.nl",
        }
      };
      return house;
    }

    public static House GetSingleHouseWithUserId2()
    {
      House house = new House
      {
        Id = 2,
        StreetName = "test street",
        HouseNumber = 2,
        Addition = "C",
        City = "Amsterdam",
        Zip = "5000 BB",
        Bedrooms = 3,
        Bathrooms = 2,
        Size = 110,
        Price = 600000,
        HasGarage = false,
        ConstructionYear = 1980,
        Description = "A test description",
        User = new User()
        {
          Id = 2,
          Username = "testUser",
          Email = "test@mail.nl",
        }
      };
      return house;
    }
  }
}