using System;
using System.Text;

namespace KeyPolicyEncryption.Classes
{
    public class Setup
    {
        /* Define fields
	 * security parameter
	 * public key
	 * master key 
	 * and the hexadecimal keys
	 */
        private byte[] _securityParameter;

        private readonly string _masterKey = "0x1.1c9f4c22c4a1p0";

        /*
         * Setup constuctor takes in the security paramter (byte)
         * it is converted to long and then to string
         * the string is then assigned to the security parameter 
         */

        public Setup(string k)
        {
	//convert the string to an array of bytes
            byte[] securityPameter = Encoding.UTF8.GetBytes(k);
            _securityParameter = securityPameter;
        }
 
        public string PublicKey()
        {
            //conver the array of bytes to base 64 string and return as string 
            return  Convert.ToBase64String(_securityParameter);           
        }

        public string MasterKey()
        {
            //conver the array of bytes to base 64 string and return as string
            byte[] keys = Encoding.UTF8.GetBytes(_masterKey);           
            return Convert.ToBase64String(keys);
        }
    }
}
