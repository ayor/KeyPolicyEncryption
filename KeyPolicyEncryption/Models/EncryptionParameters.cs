using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeyPolicyEncryption.Models
{
    public class EncryptionParameters
    {
        public string filepath { get; set; }
        public string fileContent { get; set; }
        public AttributeName Attributes { get; set; }
    }
}
