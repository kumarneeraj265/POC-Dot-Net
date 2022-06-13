using System.Text.Json.Serialization;

namespace CRUD_PRAC.Constants
{
    public class Roles
    {
        public static Roles Admin { get; } = new Roles(0, "admin");
        public static Roles User { get; } = new Roles(1, "user");
        public static Roles Player { get; } = new Roles(2, "player");       

        public Roles(int id, string name)
        {
        }
        
    }
}
