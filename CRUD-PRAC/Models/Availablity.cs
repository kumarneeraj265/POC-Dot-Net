
using CRUD_PRAC.Constants;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CRUD_PRAC.Models
{
    public class Availablity : ModifiedDateEntity
    {
        
        public int Id { get; set; }
       
        public int PlayerId { get; set; }
        //public Dictionary<string, int> Slots { get; set; } = new Dictionary<string, int>();
        
        public string Slot { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Days Day { get; set; }

    }    
}
