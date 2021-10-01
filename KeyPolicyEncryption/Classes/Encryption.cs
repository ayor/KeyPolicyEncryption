using System;
using System.Text;
using System.Collections.Generic;
using KeyPolicyEncryption.Models;

                                                                         
namespace KeyPolicyEncryption.Classes
{
    public class Encryption
    {
        private string _message;

        private List<string> _UserAttributes;

        private string _cypherText = "";

        private readonly List<string> Claims = new List<string>();

        //Encryption takes in the message, primary key and the UserAttributes 
        // and returns the cypher text

        public Encryption(string Pk, List<string> userAttributes, string filecontent)
        {
            //Creating the UserAttributes

            _UserAttributes = userAttributes;
            _message = filecontent;
        }

        public string getCypherText(string pk, string mk)
        {

            try
            {
                string accessPolicy = getAccessPolicy();

                byte[] messagebytes = Encoding.UTF8.GetBytes(_message);

                //for (int i = 0; i < messagebytes.Length; i++)
                //{

                //    _cypherText += (messagebytes[i] + " ");
                //}
                //claims contains the UserAttributes converted to string

                _cypherText = Convert.ToBase64String(messagebytes);
                Ciphertext cipher = new Ciphertext(_cypherText, accessPolicy);
                string cipherText = String.Concat(mk, ".", cipher.cipher, ".", pk, cipher.accesspolicy);
                return cipherText;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private string getAccessPolicy()
        {

            string accessPolicy = "";
            try
            {

                


                //convert UserAttributes to strings

                foreach (string attribute in _UserAttributes)
                {
                    Claims.Add(attribute);

                }

                //Adds the UserAttributes to the claims list



                //convert the claims to bytes
                foreach (string claim in Claims)
                {
                    accessPolicy += Enc.EncryptPassword(claim);
                }

                
                return accessPolicy;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}


