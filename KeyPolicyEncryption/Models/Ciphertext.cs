namespace KeyPolicyEncryption.Models
{
    public class Ciphertext
    {
        public string cipher { get; set; }
        public string accesspolicy { get; set; }

        public Ciphertext(string Cipher, string AccessPolicy)
        {
           this.cipher = Cipher;
           this.accesspolicy = AccessPolicy;
        }
    }
}
