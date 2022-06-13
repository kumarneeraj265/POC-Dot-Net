using CRUD_PRAC.DTOs;
using CRUD_PRAC.DTOs.AvailablityDTO;
using CRUD_PRAC.DTOs.UserDTO;
using CRUD_PRAC.Models;
using CRUD_PRAC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CRUD_PRAC.Controllers
{
    [ApiController]
    [AllowCrossSite]
    public class Availabltiy : Controller
    {
        private IAvailablityService _availablityService;

       public Availabltiy(IAvailablityService availablityService)
        {
            _availablityService = availablityService;

        }

        [HttpPost("AddAvailablity")]
        [AllowAnonymous]
        public async Task<ActionResult<ServiceResponse<AvailablityEntity>>> AddAvailablityAsync([FromBody] AvailablityEntity newAvail)
        {
             return Ok(await _availablityService.AddAvailablity(newAvail));
        }

        [HttpPut("Availablity")]
        [AllowAnonymous]
        public async Task<ActionResult<ServiceResponse<AvailablityEntity>>> UpdateAvailablityAsync([FromBody] AvailablityEntity newAvail)
        {
            return Ok(await _availablityService.AddAvailablity(newAvail));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetUserDTO>>> GetByPlayerIdAsync(int id)
        {
            return Ok(await _availablityService.GetAvailablityByPlayerId(id));
        }

        [HttpPost("GetMatchedAvailablitiesByIds")]
        public async Task<ActionResult<ServiceResponse<FetchedPlayersList>>> GetCommonAvaildAsync( [FromBody] int[] playerIds, string eventType)
        {
            
            if (!string.IsNullOrEmpty(eventType) && !Constants.Constants.validTypes.Contains(eventType.ToLowerInvariant()))
            {
                var message =  $"EventType must be 'singles' or 'doubles'. Values of 's' and 'd' can be used as shorthand. {eventType} is not a valid option";
                return StatusCode( 400, new { status= 400 , message = message });
                 
            }
            else {
                return Ok(await _availablityService.GetCommonAvaildAsync(playerIds, eventType));
            }
            
        }
    }
}
