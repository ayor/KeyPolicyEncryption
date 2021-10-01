using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeyPolicyEncryption.Models
{
    public class Department
    {
        public int ID { get; set; }
        public bool IsHR { get; set; }
        public bool IsSCM { get; set; }
        public bool IsIT { get; set; }
    }
}
