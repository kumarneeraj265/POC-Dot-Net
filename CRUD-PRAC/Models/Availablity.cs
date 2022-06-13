
using CRUD_PRAC.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUD_PRAC.Models
{
    public class Availablity : ModifiedDateEntity
    {
        
        public int Id { get; set; }
       
        public int PlayerId { get; set; }
        //public Dictionary<string, int> Slots { get; set; } = new Dictionary<string, int>();
        
        public string Slot { get; set; }       
        public Days Day { get; set; }

    }    
}
