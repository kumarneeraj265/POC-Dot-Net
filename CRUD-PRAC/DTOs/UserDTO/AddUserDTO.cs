using CRUD_PRAC.Models;

namespace CRUD_PRAC.DTOs.UserDTO
{
    public class AddUserDTO
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public int Status { get; set; }

        public Roles Roles { get; set; } = Roles.User;
    }
}
