using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Main.DTO;
using server.Main.Models;
using server.Main.Service;

namespace server.Main.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HousesController : ControllerBase
    {
        private readonly IHouseService _iHouseService;
        public HousesController(IHouseService iHouseService)
        {
            _iHouseService = iHouseService;
        }


        // ROUTES //
        [HttpGet]
        public async Task<ActionResult<List<HouseDto>>> GetHouses()
        {
            var response = await _iHouseService.GetHouses();
            if (!response.Success) return BadRequest(response.Message);
            return Ok(response.Data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HouseDto>> GetSingleHouse([FromRoute] int id)
        {
            var response = await _iHouseService.GetSingleHouse(id);
            if (!response.Success) return BadRequest(response.Message);
            return Ok(response.Data);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<HouseDto>> CreateHouse([FromBody] HouseDto houseDto)
        {
            var response = await _iHouseService.CreateHouse(houseDto);
            if (!response.Success) return BadRequest(response.Message);
            return Ok(response.Data);
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<HouseDto>> UpdateHouse([FromBody] HouseDto houseDto)
        {
            var response = await _iHouseService.UpdateHouse(houseDto);
            if (!response.Success) return BadRequest(response.Message);
            return Ok(response.Data);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteHouse([FromRoute] int id)
        {
            var response = await _iHouseService.DeleteHouse(id);
            if (!response.Success) return BadRequest(response.Message);
            return Ok(response.Data);
        }
    }
}
