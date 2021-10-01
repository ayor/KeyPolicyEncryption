using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace KeyPolicyEncryption.Classes
{
    public class Enc
    {
        public static string EncryptPassword(string password)
        {
            
            //hash the password using sha256

            //create a Sha256 object 
            using (SHA256 sHA256 = SHA256.Create())
            {
                //convert string to bytes array
                byte[] passInBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashedPass;
                string encryptedPassword;
                hashedPass = sHA256.ComputeHash(passInBytes);

                encryptedPassword = Convert.ToBase64String(hashedPass);
                return encryptedPassword;
            }
            
            
            
        }
    }
}
