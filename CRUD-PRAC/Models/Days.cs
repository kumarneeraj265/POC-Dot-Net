using System.Text.Json.Serialization;

namespace CRUD_PRAC.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Days
    {
        Monday = 1,
        Tuesday = 2,
        Wednesday = 3,
        Thursday = 4,
        Friday = 5,
        Saturday = 6,
        Sunday = 7,
    }
}
