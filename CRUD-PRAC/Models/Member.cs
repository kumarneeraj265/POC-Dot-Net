using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUD_PRAC.Models
{
    public class Member
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Name { get; set; }
        
        public string Email { get; set; }

        public int Status { get; set; }
        public string? StripeCustId { get; set; }

        public Roles Roles { get; set; } = Roles.User;

    }
}
