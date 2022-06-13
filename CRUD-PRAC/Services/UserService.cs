using AutoMapper;
using CRUD_PRAC.Data;
using CRUD_PRAC.DTOs.UserDTO;
using CRUD_PRAC.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUD_PRAC.Services
{
    public class UserService : IUserService
    {
        
        private IMapper _mapper;
        private DataContext _context;

        public UserService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;

        }
        public async Task<ServiceResponse<List<GetUserDTO>>> AddUser(AddUserDTO newUser)
        {
            var serviceResponse = new ServiceResponse<List<GetUserDTO>>();
            Member user = _mapper.Map<Member>(newUser);
            _context.Players.Add(user);
            await _context.SaveChangesAsync();
            serviceResponse.Data = null; //await _context.Users.Select(user => _mapper.Map<GetUserDTO>(user)).ToListAsync();
            serviceResponse.Message = "Saved Successfully";
            serviceResponse.Success = 200;
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetUserDTO>>> GetAllUsers()
        {
             var serviceResponse = new ServiceResponse<List<GetUserDTO>>();
            // database context acess  
            var dbUsers = await _context.Players.ToListAsync();
             serviceResponse.Data= dbUsers.Select(user => _mapper.Map<GetUserDTO>(user)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetUserDTO>> GetUserById(int id)
        {
            var serviceResponse = new ServiceResponse<GetUserDTO>();
            var dbUsers = await _context.Players.FirstOrDefaultAsync(user => user.Id == id);
            serviceResponse.Data = _mapper.Map<GetUserDTO>(dbUsers);
            return serviceResponse;
        }
    }
}
