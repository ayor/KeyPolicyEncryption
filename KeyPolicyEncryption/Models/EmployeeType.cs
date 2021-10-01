using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeyPolicyEncryption.Models
{
    public class EmployeeType
    {
        public int ID { get; set; }
        public bool IsEmployee { get; set; } 
        public bool IsContractor { get; set; }
        public bool IsLabor { get; set; }
    }
}
