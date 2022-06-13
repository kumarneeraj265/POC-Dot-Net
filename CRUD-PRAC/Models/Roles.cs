using System.Text.Json.Serialization;

namespace CRUD_PRAC.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Roles { 
        Admin,
        User,
        Player
    }
}
