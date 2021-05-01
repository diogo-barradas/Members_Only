using System.Security.Cryptography;
using System.Text;

namespace Members_Only
{
    class HashCode
    {
        public string PassHash(string data)
        {
            MD5 sha = MD5.Create();
            byte[] hashdata = sha.ComputeHash(Encoding.Default.GetBytes(data));
            StringBuilder returnValue = new StringBuilder();

            for (int i = 0; i < hashdata.Length; i++)
            {
                returnValue.Append(hashdata[i].ToString());
            }
            return returnValue.ToString();
            //encriptar o valor das passwords
        }
    }
}