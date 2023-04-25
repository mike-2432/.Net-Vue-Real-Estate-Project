using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using serverUnitTests.Fixtures;
using serverUnitTests.Helpers;

namespace serverUnitTests.Systems.Controllers
{
  public class TestHousesController
  {

    // GetHouses //
    // ========= //
    /* 
      Test successful request of GetHouses
      Should return statuscode 200 with a list of houseDto's, if there are houses returned from the service
    */
    [Fact]
    public async Task GetHouses_OnSuccess_ReturnsStatusCode200()
    {
      // Arrange      
      var serviceResponse = new ServiceResponseDto<List<HouseDto>>();
      serviceResponse.Data = HousesDtoFixtures.GetHouses();

      var mockHousesService = new Mock<IHouseService>();
      mockHousesService
        .Setup(service => service.GetHouses())
        .ReturnsAsync(serviceResponse);

      var controller = new HousesController(mockHousesService.Object);

      // Act
      var actualResult = await controller.GetHouses();
      var resultObject = HelperMethods.GetObjectResultContent<List<HouseDto>>(actualResult);

      // Assert
      var listResult = Assert.IsType<List<HouseDto>>(resultObject);
      Assert.IsType<OkObjectResult>(actualResult.Result);
      Assert.Equal(serviceResponse.Data, resultObject);  
      Assert.Equal(2, listResult.Count());
    }

    /* 
      Test no content of GetHouses
      Should return statuscode 204, if there are no houses in the list returned from the service
    */
    [Fact]
    public async Task GetHouses_OnNoContent_ReturnsStatusCode204()
    {
      // Arrange      
      var serviceResponse = new ServiceResponseDto<List<HouseDto>>();      
      serviceResponse.Data = new List<HouseDto>();

      var mockHousesService = new Mock<IHouseService>();
      mockHousesService
        .Setup(service => service.GetHouses())
        .ReturnsAsync(serviceResponse);

      var controller = new HousesController(mockHousesService.Object);

      // Act
      var actualResult = await controller.GetHouses();

      // Assert
      Assert.IsType<NoContentResult>(actualResult.Result);
    }

    /*
      Test failed request of GetHouses
      Should return statuscode 400 with an error message, if the houseservice response with an error
    */
    [Fact]
    public async Task GetHouses_OnFailure_ReturnsStatusCode400()
    {
      // Arrange      
      var serviceResponse = new ServiceResponseDto<List<HouseDto>>();
      serviceResponse.Message = "Failed";
      serviceResponse.Success = false;

      var mockHousesService = new Mock<IHouseService>();
      mockHousesService
        .Setup(service => service.GetHouses())
        .ReturnsAsync(serviceResponse);

      var controller = new HousesController(mockHousesService.Object);

      // Act
      var actualResult = await controller.GetHouses();
      
      // Assert
      var response = Assert.IsType<BadRequestObjectResult>(actualResult.Result);
      Assert.Equal(serviceResponse.Message, response.Value);
    }

    // GetSingleHouses //
    // =============== //
    /*
      Test successful request of GetSingleHouse
      Should return statuscode 200 with a houseDto's, if there is a houses returned from the service
    */
    [Fact]
    public async Task GetSingleHouse_OnSuccess_ReturnsStatusCode200()
    {
      // Arrange      
      var serviceResponse = new ServiceResponseDto<HouseDto>();
      serviceResponse.Data = HousesDtoFixtures.GetSingleHouse();

      var mockHousesService = new Mock<IHouseService>();
      mockHousesService
        .Setup(service => service.GetSingleHouse(1))
        .ReturnsAsync(serviceResponse);

      var controller = new HousesController(mockHousesService.Object);

      // Act
      var actualResult = await controller.GetSingleHouse(1);
      var resultObject = HelperMethods.GetObjectResultContent<HouseDto>(actualResult);

      // Assert
      Assert.IsType<OkObjectResult>(actualResult.Result);
      Assert.Equal(serviceResponse.Data, resultObject);

    }

    /*
      Test failed request of GetHouses
      Should return statuscode 400 with an error message, if the houseservice response with an error
    */
    [Fact]
    public async Task GetSingleHouse_OnFailure_ReturnsStatusCode400()
    {
      // Arrange      
      var serviceResponse = new ServiceResponseDto<HouseDto>();
      serviceResponse.Message = "Failed";
      serviceResponse.Success = false;

      var mockHousesService = new Mock<IHouseService>();
      mockHousesService
        .Setup(service => service.GetSingleHouse(1))
        .ReturnsAsync(serviceResponse);

      var controller = new HousesController(mockHousesService.Object);

      // Act
      var actualResult = await controller.GetSingleHouse(1);

      // Assert
      var response = Assert.IsType<BadRequestObjectResult>(actualResult.Result);
      Assert.Equal(serviceResponse.Message, response.Value);
    }

    // CreateHouse //
    // ========= //
    /* 
      Test successful request of CreateHouse
      Should return statuscode 200 with the houseDto
    */
    [Fact]
    public async Task CreateHouse_OnSuccess_ReturnsStatusCode200()
    {
      // Arrange      
      HouseDto newHouseDto = HousesDtoFixtures.GetSingleHouse();

      var serviceResponse = new ServiceResponseDto<HouseDto>();
      serviceResponse.Data = newHouseDto;

      var mockHousesService = new Mock<IHouseService>();
      mockHousesService
        .Setup(service => service.CreateHouse(newHouseDto))
        .ReturnsAsync(serviceResponse);

      var controller = new HousesController(mockHousesService.Object);

      // Act
      var actualResult = await controller.CreateHouse(newHouseDto);

      // Assert
      var response = Assert.IsType<OkObjectResult>(actualResult.Result);
      Assert.Equal(newHouseDto, response.Value);
    }
  }
}