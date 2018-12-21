using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Cryptogram.Models
{
    public static class Decryptor
    {
        public static void DecryptFile(string path, string password)
        {
            var fileInfo = new FileInfo(path);
            if (!".ENC".Equals(fileInfo.Extension))
            {
                MessageBox.Show($"{fileInfo.Extension} must be .ENC", "Decryption failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                using (var aes = new RijndaelManaged())
                {
                    var key = Encoding.UTF8.GetBytes("secretKey");
                    var IV = Encoding.UTF8.GetBytes(password);
                    var outputFilePath = $"{Path.GetFileNameWithoutExtension(fileInfo.Name)}.txt";

                    using (var fsCrypt = new FileStream(path, FileMode.Open))
                    {
                        using (var fsOut = new FileStream(outputFilePath, FileMode.Create))
                        {
                            using (var decryptor = aes.CreateDecryptor(key, IV))
                            {
                                using (var cs = new CryptoStream(fsCrypt, decryptor, CryptoStreamMode.Read))
                                {
                                    int data;
                                    while ((data = cs.ReadByte()) != -1)
                                    {
                                        fsOut.WriteByte((byte)data);
                                    }
                                    MessageBox.Show("File decrypted", "Decryption",
                                        MessageBoxButtons.OK, MessageBoxIcon.None);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Decryption failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}