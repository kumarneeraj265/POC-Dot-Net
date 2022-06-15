using CRUD_PRAC.Constants;
using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;

namespace CRUD_PRAC.Models
{
    public class Slots
    {
        public List<Slot>? Slot { get; set; }

        public Days? Day { get; set; }

    }
}
