using System.Text.Json.Serialization;

namespace CRUD_PRAC.Constants
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Days
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday,
    }
}
