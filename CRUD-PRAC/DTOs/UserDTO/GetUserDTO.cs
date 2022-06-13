using CRUD_PRAC.Models;

namespace CRUD_PRAC.DTOs.UserDTO
{
    public class GetUserDTO
    {
        public string FName { get; set; }

        public string PEmail { get; set; }

        public int Status { get; set; }

        public Roles Roles { get; set; } = Roles.User;
    }
}
