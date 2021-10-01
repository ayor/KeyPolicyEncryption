using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeyPolicyEncryption.Classes
{
    public class KeyGen
    {
        private string _accessPolicy;
        private string _mk;
        private string _pk;

        public KeyGen(string accessPolicy, string MK, string PK)
        {
            _accessPolicy = accessPolicy;
            _mk = MK;
            _pk = PK;
        }

        /*
         *- Convert the attributes to string \
         * get the bytes of the string
         * convert the bytes to a single string   
         */
        public string DecryptionKey() {

            return _accessPolicy + ":" + _mk + ":" + _pk;
        }
    }
}
