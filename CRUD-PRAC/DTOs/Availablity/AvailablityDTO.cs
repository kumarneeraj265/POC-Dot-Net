using CRUD_PRAC.Models;

namespace CRUD_PRAC.DTOs.AvailablityDTO
{
    public class AvailablityDTO 
    {
        public int PlayerId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public List<Slots>? Availablity { get; set; } = new List<Slots>();
    }

    public class FetchedPlayersList
    {
       public List<AvailablityDTO>? PlayersDetails { get; set; }
       public List<Slots> MatchedAvailablity { get; set; } =  new List<Slots>();
    }
}
