using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using server.Main.Data;
using server.Main.Models;
using serverUnitTests.Fixtures;
using Xunit.Abstractions;

namespace serverUnitTests.Systems.Services
{
  public class TestHouseService
  {
    private readonly ITestOutputHelper output;
    public TestHouseService(ITestOutputHelper output)
    {
      this.output = output;
    }

    // GetHouses //
    // ========= //
    /* 
      Test successful fetch of all houses from the database
      Should return a serviceResponseDto with in the data a list of houseDto's
    */
    [Fact]
    public async Task GetHouses_OnSuccess_ReturnsServiceResponseWithListOfHouseDTOs()
    {
      // Arrange
      var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
      // User with id 1 should own the house
      var userIdClaim = new Claim(ClaimTypes.NameIdentifier, "1"); 
      mockHttpContextAccessor
        .Setup(x => x.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier))
        .Returns(userIdClaim);
      
      var options = new DbContextOptionsBuilder<DataContext>()
          .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
          .Options;
      var dbContext = new DataContext(options);

      var service = new HouseService(dbContext, mockHttpContextAccessor.Object);

      // Add houses to the database
      House houseOne = HousesFixtures.GetSingleHouseWithUserId1();
      House houseTwo = HousesFixtures.GetSingleHouseWithUserId2();
      dbContext.Houses.Add(houseOne);
      dbContext.Houses.Add(houseTwo);
      dbContext.SaveChanges();

      // Assert 
        // Should return a serviceResponseDto with a list of houses
      var serviceResponseDtoResult = await service.GetHouses();

      // Act
      Assert.IsType<ServiceResponseDto<List<HouseDto>>>(serviceResponseDtoResult);
      Assert.True(serviceResponseDtoResult.Success);
      Assert.Equal(houseOne.StreetName, serviceResponseDtoResult.Data?[0].StreetName);
      Assert.Equal(true, serviceResponseDtoResult.Data?[0].MadeByMe);
      Assert.Equal(false, serviceResponseDtoResult.Data?[1].MadeByMe);
    }

    /* 
      Test successful fetch of all houses from the database if no houses are present
      Should return a serviceResponseDto with in the data an empty list of houseDto's
    */
    [Fact]
    public async Task GetHouses_OnEmptyDatabase_ReturnsServiceResponseWithEmptyListOfHouseDTOs()
    {
      // Arrange
      var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
      var userIdClaim = new Claim(ClaimTypes.NameIdentifier, "1");
      mockHttpContextAccessor
        .Setup(x => x.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier))
        .Returns(userIdClaim);

      var options = new DbContextOptionsBuilder<DataContext>()
          .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
          .Options;
      var dbContext = new DataContext(options);

      var service = new HouseService(dbContext, mockHttpContextAccessor.Object);

      // Assert 
        // Should return a serviceResponseDto with an empty list of houses
      var serviceResponseDtoResult = await service.GetHouses();

      // Act
      Assert.IsType<ServiceResponseDto<List<HouseDto>>>(serviceResponseDtoResult);
      Assert.True(serviceResponseDtoResult.Success);
      Assert.Empty(serviceResponseDtoResult.Data!);
    }

    // GetSingleHouse //
    // ========= //
    /* 
      Test successful fetch of a single house from the database
      Should return a serviceResponseDto with in the data a houseDto object
    */
    [Fact]
    public async Task GetSingleHouse_OnSuccess_ShouldReturnServiceResponseDtoWithHouseDto()
    {
      // Arrange
      var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
      // User with id 1 should own the house
      var userIdClaim = new Claim(ClaimTypes.NameIdentifier, "1");
      mockHttpContextAccessor
        .Setup(x => x.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier))
        .Returns(userIdClaim);

      var options = new DbContextOptionsBuilder<DataContext>()
          .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
          .Options;
      var dbContext = new DataContext(options);

      var service = new HouseService(dbContext, mockHttpContextAccessor.Object);

      // Add houses to the database
      House houseOne = HousesFixtures.GetSingleHouseWithUserId1();
      House houseTwo = HousesFixtures.GetSingleHouseWithUserId2();
      dbContext.Houses.Add(houseOne);
      dbContext.Houses.Add(houseTwo);
      dbContext.SaveChanges();

      // Assert 
        // Should return a serviceResponseDto with a houseDto that has an ID of 1
        // The houseDto should have the variable "madeByMe" on true, since the userClaim is 1
      var serviceResponseDtoResult = await service.GetSingleHouse(1);
      // Act
      Assert.IsType<ServiceResponseDto<HouseDto>>(serviceResponseDtoResult);
      Assert.True(serviceResponseDtoResult.Success);
      Assert.Equal(1, serviceResponseDtoResult.Data?.Id);
      Assert.Equal(houseOne.StreetName, serviceResponseDtoResult.Data?.StreetName);
      Assert.Equal(true, serviceResponseDtoResult.Data?.MadeByMe);

      // Assert
        // Should return a serviceResponseDto with a houseDto that has an ID of 3
        // The houseDto should have the variable "madeByMe" on false, since the userClaim is 1 and not 2
      var serviceResponseDtoResultTwo = await service.GetSingleHouse(2);
      // Act
      Assert.IsType<ServiceResponseDto<HouseDto>>(serviceResponseDtoResultTwo);
      Assert.True(serviceResponseDtoResultTwo.Success);
      Assert.Equal(2, serviceResponseDtoResultTwo.Data?.Id);
      Assert.Equal(houseOne.StreetName, serviceResponseDtoResultTwo.Data?.StreetName);
      Assert.Equal(false, serviceResponseDtoResultTwo.Data?.MadeByMe);
    }

    /* 
      Test failed fetch of a single house from the database
      Should return a serviceResponseDto with the success variable on false
      Should return a serviceResponseDto with a message why it failed
    */
    [Fact]
    public async Task GetSingleHouse_OnFail_ShouldReturnServiceResponseDtoWithFailMessage()
    {
      // Arrange
      var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
      // User with id 1 should own the house
      var userIdClaim = new Claim(ClaimTypes.NameIdentifier, "1");
      mockHttpContextAccessor
        .Setup(x => x.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier))
        .Returns(userIdClaim);

      var options = new DbContextOptionsBuilder<DataContext>()
          .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
          .Options;
      var dbContext = new DataContext(options);

      var service = new HouseService(dbContext, mockHttpContextAccessor.Object);

      // Add houses to the database
      House houseOne = HousesFixtures.GetSingleHouseWithUserId1();
      dbContext.Houses.Add(houseOne);
      dbContext.SaveChanges();

      // Assert 
        // Should return a serviceResponseDto with the success variable on false and a message of the failed reason
      var serviceResponseDtoResult = await service.GetSingleHouse(2);
      // Act
      Assert.IsType<ServiceResponseDto<HouseDto>>(serviceResponseDtoResult);
      Assert.False(serviceResponseDtoResult.Success);
      Assert.Equal("House with id '2' not found.", serviceResponseDtoResult.Message);
    }

    // CreateHouse //
    // ========= //
    /* 
      Test successful creation of a house to the database
      Should return a serviceResponseDto with in the data a the created houseDto
    */
    [Fact]
    public async Task CreateHouse_OnSuccess_ReturnsServiceResponseWithHouseDto()
    {
      // Arrange
      var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
      var userIdClaim = new Claim(ClaimTypes.NameIdentifier, "1");
      mockHttpContextAccessor
        .Setup(x => x.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier))
        .Returns(userIdClaim);

      var options = new DbContextOptionsBuilder<DataContext>()
          .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
          .Options;
      var dbContext = new DataContext(options);

      var service = new HouseService(dbContext, mockHttpContextAccessor.Object);
      
      // Add user to the database
      User user = UsersFixtures.GetUserWithId1();
      dbContext.Users.Add(user);
      dbContext.SaveChanges();

      // Assert 
        // Should return a serviceResponseDto with a list of houses
      HouseDto newHouse = HousesDtoFixtures.GetSingleHouse();
      var serviceResponseDtoResult = await service.CreateHouse(newHouse);

      var expectedStreetNameInDB = dbContext.Houses
        .Where(i => i.Id == newHouse.Id)
        .Select(item => item.StreetName)
        .FirstOrDefault();

      var expectedStreetNameReturned = serviceResponseDtoResult.Data!.StreetName;

      // Act
      Assert.IsType<ServiceResponseDto<HouseDto>>(serviceResponseDtoResult);
      Assert.True(serviceResponseDtoResult.Success);
      Assert.Equal(expectedStreetNameInDB, expectedStreetNameReturned);
    }

    /* 
      Test failed creation of a house to the database because of no user
      Should return a serviceResponseDto with a message why it failed
    */
    [Fact]
    public async Task CreateHouse_OnFail_ReturnsServiceResponseWithFailMessage()
    {
      // Arrange
      var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
      // No user in the database with userIdClaim 2
      var userIdClaim = new Claim(ClaimTypes.NameIdentifier, "2");
      mockHttpContextAccessor
        .Setup(x => x.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier))
        .Returns(userIdClaim);

      var options = new DbContextOptionsBuilder<DataContext>()
          .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
          .Options;
      var dbContext = new DataContext(options);

      var service = new HouseService(dbContext, mockHttpContextAccessor.Object);

      // Add user to the database
      User user = UsersFixtures.GetUserWithId1();
      dbContext.Users.Add(user);
      dbContext.SaveChanges();

      // Assert 
        // Should return a serviceResponseDto with a message why it failed
      HouseDto newHouse = HousesDtoFixtures.GetSingleHouse();
      var serviceResponseDtoResult = await service.CreateHouse(newHouse);

      // Act
      Assert.IsType<ServiceResponseDto<HouseDto>>(serviceResponseDtoResult);
      Assert.False(serviceResponseDtoResult.Success);
      Assert.Equal("No user found.", serviceResponseDtoResult.Message);
    }
  }
}