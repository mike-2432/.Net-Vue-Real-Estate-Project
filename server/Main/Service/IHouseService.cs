using server.Main.Models;
using server.Main.DTO;

namespace server.Main.Service
{
    public interface IHouseService
    {
        /// <summary>
        /// This method returns a list with all rows from the houses table
        /// </summary>
        /// <returns>A serviceResponseDto with in the data a list of houseDto's</returns>
        Task<ServiceResponseDto<List<HouseDto>>> GetHouses();

        /// <summary>
        /// This method returns a single house with all rows from the houses table
        /// </summary>
        /// <param name = "id" >Id to find the house</param>
        /// <returns>A serviceResponseDto with in the data the houseDto</returns>
        Task<ServiceResponseDto<HouseDto>> GetSingleHouse(int id);

        /// <summary>
        /// This method uploads a house to the houses table
        /// </summary>
        /// <param name="houseDto">Dto for the house</param>
        /// <returns>A serviceResponseDto with in the data the houseDto</returns>
        Task<ServiceResponseDto<HouseDto>> CreateHouse(HouseDto houseDto);

        /// <summary>
        /// This method updates an existing house
        /// </summary>
        /// <param name="houseDto">Dto for the house</param>
        /// <returns>A serviceResponseDto with in the data the houseDto</returns>
        Task<ServiceResponseDto<HouseDto>> UpdateHouse(HouseDto houseDto);

        /// <summary>
        /// This method deletes an existing house
        /// </summary>
        /// <param name="id">Id of the house</param>
        /// <returns>A serviceResponseDto with in the data a string with the result</returns>
        Task<ServiceResponseDto<string>> DeleteHouse(int id);
    }
}