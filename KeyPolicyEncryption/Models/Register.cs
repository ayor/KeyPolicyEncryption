using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KeyPolicyEncryption.Models
{
    public class Register
    {
        public int ID { get; set; }

        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public EmployeeType EmployeeType { get; set; }
        [Required]
        public Department Department { get; set; }
        [Required]
        public UserRole UserRole { get; set; }
    }
}
