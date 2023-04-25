using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace serverUnitTests.Fixtures
{
  public class HousesDtoFixtures
  {
    // Get a houseDto
    public static HouseDto GetSingleHouse()
    {
      HouseDto house = new HouseDto
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
        Description = "A test description"
      };
      return house;
    }

    // Get a list of houseDto's
    public static List<HouseDto> GetHouses()
    {
      List<HouseDto> housesList = new List<HouseDto>
        {
          new HouseDto 
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
            Description = "A test description"
          },
          new HouseDto
          {
            Id = 2,
            StreetName = "test street",
            HouseNumber = 2,
            Addition = "B",
            City = "Amsterdam",
            Zip = "1000 AA",
            Bedrooms = 3,
            Bathrooms = 2,
            Size = 120,
            Price = 500000,
            HasGarage = true,
            ConstructionYear = 2000,
            Description = "A test description"
          }
        };
      return housesList;
    }
  }
}