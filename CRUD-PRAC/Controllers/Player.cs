using CRUD_PRAC.DTOs.UserDTO;
using CRUD_PRAC.Models;
using CRUD_PRAC.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace CRUD_PRAC.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class Player : ControllerBase
    {
        private IUserService _userService;

        private static List<Member> user = new List<Member>() {
           
        };

        public Player(IUserService userService)
        {
            _userService = userService;

        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetUserDTO>>>> Get()
        {
            return Ok(await _userService.GetAllUsers());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetUserDTO>>> GetByid(int id)
        {
            return Ok(await _userService.GetUserById(id));
        }

        [HttpPost("Add")]
        public async Task<ActionResult<ServiceResponse<List<GetUserDTO>>>> AddUser(AddUserDTO user)
        {
           
            return Ok(await _userService.AddUser(user));
        }

       
    }
}
