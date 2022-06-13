using CRUD_PRAC.DTOs.AvailablityDTO;
using CRUD_PRAC.Models;

namespace CRUD_PRAC.Services
{
    public interface IAvailablityService
    {
        //Task<ServiceResponse<List<GetAvailablityDTO>>> GetAllAvailablities();
        Task<ServiceResponse<AvailablityEntity>> AddAvailablity(AvailablityEntity availablity);
        Task<ServiceResponse<AvailablityDTO>> GetAvailablityByPlayerId(int playerId);
        Task<ServiceResponse<FetchedPlayersList>> GetCommonAvaildAsync(int[] playerIds, string type);
    }
}
