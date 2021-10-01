using System;
using System.Collections.Generic;
using System.Text;

namespace KeyPolicyEncryption.Classes
{
    public class Decryption
    {
        private string _cipherText;

        private string _decryptionKey;

        private string _publicKey;

        private List<byte> cipherbyte = new List<byte>();

        private string Message = "";

        public Decryption(string ciphertext, string decryptionKey, string publicKey)
        {
            _cipherText = ciphertext;
            _decryptionKey = decryptionKey;
            _publicKey = publicKey;

        }
        /*
         * 
         */
        public string getMessage(List<string> userAttribute)
        {
            string[] keys = _decryptionKey.Split(":");

            string UserAttributesInbytes = getEncAttributes(userAttribute);
            //UserAttributesInbytes = keys[0];
            if (AuthAttributes(UserAttributesInbytes, keys[0]))
            {
                //decrypt the ciphertext

                //split cipher into array of strings
                string[] cipher = _cipherText.Split(".");

                byte[] cipherInBytes = Convert.FromBase64String(cipher[1]);

                
                    Message += Encoding.UTF8.GetString(cipherInBytes);
                
                
            };
            return Message;
            //System.out.println(Message);
        }


        private string getEncAttributes(List<string> userAttribute)
        {
            //takes in a list of user attributes
            string attributeInByte = "";

            foreach(string attr in userAttribute)
            {
                attributeInByte += Enc.EncryptPassword(attr);
            }
            return attributeInByte;
        }

        private bool AuthAttributes(string userattributesinbytes, string AccessPolicy)
        {

            if (userattributesinbytes == AccessPolicy)
            {
                return true;
            }
            return true;
        }
    }

}
