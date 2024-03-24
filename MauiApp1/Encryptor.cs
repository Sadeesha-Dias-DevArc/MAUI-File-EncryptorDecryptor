using System.Security.Cryptography;
using System.Text;

namespace MauiApp1;

public class Encryptor
{
    private string _Ekey;

    public Encryptor(string encryptionKey) => _Ekey = encryptionKey;

    public bool IsEncrypted(string filePath)
    {
        return File.ReadAllText(filePath).Contains("ENCRYPTED:");
    }

    public string EncryptFile(string filePath) 
    {
        var content  = File.ReadAllText(filePath);
        var encryptedContent = EncryptString(content, _Ekey);
        var encryptedFilePath = Path.ChangeExtension(filePath, ".encrypted");
        File.WriteAllText(encryptedFilePath, $"ENCRYPTED:{encryptedContent}");
        return encryptedFilePath;
    }

    private string EncryptString(string text, string key)  
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Encoding.UTF8.GetBytes(key);
            aesAlg.IV = new byte[16]; // Initialization vector for AES
            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream  msEncryption = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncryption, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(text);
                    }
                }
                return Convert.ToBase64String(msEncryption.ToArray());
            }
        }
    }

}
