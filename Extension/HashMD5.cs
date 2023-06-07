using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace WebVPP.Extension
{
    public static class HashMD5
    {
        //Compute hash the string
        public static string GetHash(HashAlgorithm hashAlgorithm, string input)
        {
            //chuyen hoa chuoi nhap vao thanh mang byte va tinh ham bam
            byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));
            //tao stringbuilder moi de gan mang byte va tao chuoi moi
            //
            var sBuilder = new StringBuilder();

            //vong lap for lap qua tung byte cua ham bam va
            //format tung cai thanh chuoi thap luc phan
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            //return chuoi thap luc phan 
            return sBuilder.ToString();
        }

        //verify hash va chuoi 
        public static bool VerifyHash(HashAlgorithm hashAlgorithm, string input, string hash)
        {
            //hash chuoi nhap vao 
            var hashOfInput = GetHash(hashAlgorithm, input);

            //
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            return comparer.Compare(hashOfInput, hash) == 0;
        }
    }
}
