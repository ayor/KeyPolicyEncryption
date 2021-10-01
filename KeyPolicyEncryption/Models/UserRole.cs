using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeyPolicyEncryption.Models
{
    public class UserRole
    {
        public int ID { get; set; }
        public bool IsDataOwner { get; set; }
        public bool IsDataUser { get; set; }
        public bool IsAdmin { get; set; }
    }
}
