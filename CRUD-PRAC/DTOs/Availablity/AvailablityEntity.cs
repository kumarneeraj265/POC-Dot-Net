using CRUD_PRAC.Constants;
using CRUD_PRAC.Models;

namespace CRUD_PRAC.DTOs.AvailablityDTO
{
    public class AvailablityEntity 
    {
        public int PlayerId { get; set; }
        public List<Slots>? Availablity { get; set; } = new List<Slots>();
    }
}
