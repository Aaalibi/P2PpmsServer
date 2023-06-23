using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace P2PpmsServer.Utilities.Encryption
{
    public class AESService
    {

        public static string Encrypt(string input, byte[] encryptionKey, byte[] aesIV)
        {
            UnicodeEncoding UE = new UnicodeEncoding();


            using (Aes aesAlgorithm = Aes.Create())
            {
                Console.WriteLine($"Aes Cipher Mode : {aesAlgorithm.Mode}");
                Console.WriteLine($"Aes Padding Mode: {aesAlgorithm.Padding}");
                Console.WriteLine($"Aes Key Size : {aesAlgorithm.KeySize}");
                Console.WriteLine($"Aes Block Size : {aesAlgorithm.BlockSize}");


                byte[] aesKey = encryptionKey;
                aesAlgorithm.Key = aesKey;
                aesAlgorithm.IV = aesIV;
                Console.WriteLine("Key: " + Convert.ToBase64String(aesAlgorithm.Key));
                Console.WriteLine("IV:  " + Convert.ToBase64String(aesAlgorithm.IV));

                ICryptoTransform encryptor = aesAlgorithm.CreateEncryptor();

                byte[] encryptedData;

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(input);
                        }
                        encryptedData = ms.ToArray();
                    }
                }

                return Convert.ToBase64String(encryptedData);
            }
        }
    }
}
