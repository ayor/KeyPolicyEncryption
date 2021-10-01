using System.IO;

namespace KeyPolicyEncryption.Stream
{
    public static class ReadWrite
    {
        #region Reader

        public static string ReadData(string path)
        {
            string filepath = @"C:\Users\DOSUMU AYOMIDE\Downloads\Documents\Cloud\Data\" + path;
            string data = "";
            using (StreamReader sr = new StreamReader(filepath))
            {
                data = sr.ReadToEnd();
            }
            return data;
        }

        #endregion

        #region Writer
        public static void WriteData(string path, string content)
        {
            string filepath = @"C:\Users\DOSUMU AYOMIDE\Downloads\Documents\Cloud\Data\" + path;
            using (StreamWriter sw = new StreamWriter(filepath))
            {
                sw.WriteLine(content);
            }
        }

        #endregion
    }

}
