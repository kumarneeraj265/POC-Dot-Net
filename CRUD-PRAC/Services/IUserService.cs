using CRUD_PRAC.DTOs.UserDTO;
using CRUD_PRAC.Models;

namespace CRUD_PRAC.Services
{
    public interface IUserService
    {
        Task<ServiceResponse<List<GetUserDTO>>> GetAllUsers();
        Task<ServiceResponse<GetUserDTO>> GetUserById(int id);
        Task<ServiceResponse<List<GetUserDTO>>> AddUser(AddUserDTO user);
    }
}
