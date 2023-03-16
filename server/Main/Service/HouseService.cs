using server.Main.Models;
using server.Main.DTO;
using server.Main.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace server.Main.Service
{
    public class HouseService : IHouseService
    {
        // INJECTIONS //
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public HouseService(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        // INTERFACE METHODS //
        // ================= //
        // GET USER ID //
        private int? GetUserId()
        {
            var user = _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (String.IsNullOrEmpty(user))
                return null;
            return int.Parse(user);
        }

        // GET HOUSES //
        public async Task<ServiceResponseDto<List<HouseDto>>> GetHouses()
        {
            var response = new ServiceResponseDto<List<HouseDto>>();
            try
            {
                // Gets the user, is null if there is no user logged in
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());

                // Gets a list of the houses table
                var housesTableList = await _context.Houses.ToListAsync();

                // Maps the houses from the table to a list of houseDto's
                List<HouseDto> houseDtoList = new List<HouseDto>();
                foreach (House house in housesTableList)
                {
                    var houseDto = new HouseDto();
                    houseDto.Id = house.Id;
                    houseDto.StreetName = house.StreetName;
                    houseDto.HouseNumber = house.HouseNumber;
                    houseDto.Addition = house.Addition;
                    houseDto.City = house.City;
                    houseDto.Zip = house.Zip;
                    houseDto.Bedrooms = house.Bedrooms;
                    houseDto.Bathrooms = house.Bathrooms;
                    houseDto.Size = house.Size;
                    houseDto.Price = house.Price;
                    houseDto.HasGarage = house.HasGarage;
                    houseDto.ConstructionYear = house.ConstructionYear;
                    houseDto.Description = house.Description;
                    if (user?.Id is not null && house.User?.Id is not null)
                        if (user.Id == house.User.Id)
                            houseDto.MadeByMe = true;
                    houseDtoList.Add(houseDto);
                }

                // Returns a list of houseDto's
                response.Data = houseDtoList;
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        // GET SINGLE HOUSE //
        public async Task<ServiceResponseDto<HouseDto>> GetSingleHouse(int id)
        {
            var response = new ServiceResponseDto<HouseDto>();
            try
            {
                // Gets the house, throws an error if no house is found //
                var house = await _context.Houses.FirstOrDefaultAsync(i => i.Id == id);
                if (house is null) throw new Exception($"House with id '{id}' not found.");

                // Creates an houseDto object
                var houseDto = new HouseDto();

                // Gets the user and check if the user owns the house listing
                // Is null if there is no user logged in
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());
                if (user?.Id is not null && house.User?.Id is not null)
                    if (user.Id == house.User.Id)
                        houseDto.MadeByMe = true;

                // Maps the house to the houseDto      
                houseDto.Id = house.Id;
                houseDto.StreetName = house.StreetName;
                houseDto.HouseNumber = house.HouseNumber;
                houseDto.Addition = house.Addition;
                houseDto.City = house.City;
                houseDto.Zip = house.Zip;
                houseDto.Bedrooms = house.Bedrooms;
                houseDto.Bathrooms = house.Bathrooms;
                houseDto.Size = house.Size;
                houseDto.Price = house.Price;
                houseDto.HasGarage = house.HasGarage;
                houseDto.ConstructionYear = house.ConstructionYear;
                houseDto.Description = house.Description;

                // Returns houseDto
                response.Data = houseDto;
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        // CREATE HOUSE //
        public async Task<ServiceResponseDto<HouseDto>> CreateHouse(HouseDto houseDto)
        {
            var response = new ServiceResponseDto<HouseDto>();
            var house = new House();
            try
            {
                // Gets the user that is creating the house, throws an error if no user is found
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());
                if (user is null) throw new Exception("No user found.");

                // Maps the DTO to house object 
                house.StreetName = houseDto.StreetName;
                house.HouseNumber = houseDto.HouseNumber;
                house.Addition = houseDto.Addition;
                house.City = houseDto.City;
                house.Zip = houseDto.Zip;
                house.Bedrooms = houseDto.Bedrooms;
                house.Bathrooms = houseDto.Bathrooms;
                house.Size = houseDto.Size;
                house.Price = houseDto.Price;
                house.HasGarage = houseDto.HasGarage;
                house.ConstructionYear = houseDto.ConstructionYear;
                house.Description = houseDto.Description;
                house.User = user;

                // Saves model to the database //
                _context.Houses.Add(house);
                await _context.SaveChangesAsync();

                // Adds the new ID and returns the houseDto
                houseDto.Id = house.Id;
                response.Data = houseDto;
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        // UPDATE HOUSE //
        public async Task<ServiceResponseDto<HouseDto>> UpdateHouse(HouseDto houseDto)
        {
            var response = new ServiceResponseDto<HouseDto>();
            try
            {
                // Gets the user that is updating the house, throws an error if no user is found
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());
                if (user is null) throw new Exception("No user found.");

                // Gets the house, throws an error if no house is found //
                var house = await _context.Houses.FirstOrDefaultAsync(i => i.Id == houseDto.Id);
                if (house is null) throw new Exception($"House with id '{houseDto.Id}' not found.");

                // Throws an error if the wrong user tries to update the house
                if (house.User?.Id != user.Id) throw new Exception("Incorrect user.");

                // Mapping DTO to house object //
                house.StreetName = houseDto.StreetName;
                house.HouseNumber = houseDto.HouseNumber;
                house.Addition = houseDto.Addition;
                house.City = houseDto.City;
                house.Zip = houseDto.Zip;
                house.Bedrooms = houseDto.Bedrooms;
                house.Bathrooms = houseDto.Bathrooms;
                house.Size = houseDto.Size;
                house.Price = houseDto.Price;
                house.HasGarage = houseDto.HasGarage;
                house.ConstructionYear = houseDto.ConstructionYear;
                house.Description = houseDto.Description;

                // Saves model to the database and returns the houseDto //
                await _context.SaveChangesAsync();
                response.Data = houseDto;
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }

        // DELETE HOUSE //
        public async Task<ServiceResponseDto<string>> DeleteHouse(int id)
        {
            var response = new ServiceResponseDto<string>();
            try
            {
                // Gets the user that is deleting the house, throws an error if no user is found
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());
                if (user is null) throw new Exception("No user found.");

                // Finds the house that needs to be deleted, throws an error if no house is found
                var house = await _context.Houses.FirstOrDefaultAsync(i => i.Id == id);
                if (house is null) throw new Exception($"House with id '{id}' not found.");

                // Throws an error if the wrong user tries to delete the house
                if (house.User?.Id != user.Id) throw new Exception("Incorrect user.");

                // Deletes house from the database and returns a success message
                _context.Houses.Remove(house);
                await _context.SaveChangesAsync();
                response.Data = "Success";
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }
    }
}