using System.Security.Cryptography;
using System.Text;

namespace MauiApp1;

public class Decryptor
{
    private string _Dkey;

    public Decryptor(string decryptKey) => _Dkey = decryptKey;

    public string DecryptFile(string filePath)
    {
        var content = File.ReadAllText(filePath).Replace("ENCRYPTED:", "");;
        //return DecryptString(content, _Dkey);
        var decrytedContent = DecryptString(content, _Dkey);
        var decrytedFilePath = Path.ChangeExtension(filePath, ".txt");
        File.WriteAllText(decrytedFilePath, decrytedContent);
        return decrytedFilePath;
    }

    private string DecryptString(string cipherText, string key)
    {
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Encoding.UTF8.GetBytes(key);
            aesAlg.IV = new byte[16]; // Initialization vector for AES

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msDecrypt = new MemoryStream(cipherBytes))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        return srDecrypt.ReadToEnd();
                    }
                }
            }

        }
    } 


}
 