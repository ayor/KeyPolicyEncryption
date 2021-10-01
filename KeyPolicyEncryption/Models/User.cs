using System.ComponentModel.DataAnnotations;

namespace KeyPolicyEncryption.Models
{
    public class User
    {
        public int ID { get; set; }

        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string EmployeeType { get; set; }
        [Required]
        public string Department { get; set; }
        [Required]
        public string UserRole { get; set; }    
    }
}
