
using ecv.crypto;
using ecv.enums;
using ecv.exceptions;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ecv.main
{
    public class Main
    {
        public string Operation { get; set; }
        public string FilePath { get; set; }
        private string Password;
        private AESGCM _gcm;

        public Main(string operation, string filePath)
        {
            this.Operation = operation;
            this.FilePath = filePath;

        }
        public Main() { }

        private string handleLargeFile(AESOps op)
        {
            StringBuilder output = new StringBuilder();
            using (FileStream fs = File.Open(this.FilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (BufferedStream bs = new BufferedStream(fs))
            using (StreamReader sr = new StreamReader(bs))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    switch (op)
                    {
                        case AESOps.Encrypt:
                            var encrypted = this._gcm.Encrypt(Encoding.ASCII.GetBytes(line));
                            output.Append(Encoding.UTF8.GetString(encrypted));

                            break;
                        case AESOps.Decrypt:
                            var decrypted = this._gcm.Encrypt(Encoding.ASCII.GetBytes(line));
                            output.Append(Encoding.UTF8.GetString(decrypted));
                            break;
                    }
                }
            }
            return output.ToString();
        }

        private void Check()
        {
            try
            {
                string content = handleLargeFile(AESOps.Decrypt);
                Console.WriteLine("Reliable");
            }
            catch (CryptographicException e)
            {
                throw new DecryptionFailedException("Integrity Failed for file: " + this.FilePath + " Corrupted file or wrong password\n");
            }
        }

        private void Create()
        {
            string content = "";
            try
            {
                content = handleLargeFile(AESOps.Encrypt);
            }
            catch (CryptographicException e)
            {
                throw new EncryptionFailedException("Failed encrypting file: " + this.FilePath + "\n");
            }
            this.StoreToDisk(content, AESOps.Encrypt);
        }

        private void Recover()
        {
            string content = "";
            try
            {
                content = handleLargeFile(AESOps.Decrypt);
            }
            catch (CryptographicException e)
            {
                throw new DecryptionFailedException("Failed Recovering file: " + this.FilePath + " File corrupted or wrong password.\n");
            }
            this.StoreToDisk(content, AESOps.Decrypt);
        }

        private void StoreToDisk(string content, AESOps op)
        {
            string dir = "";
            switch (op)
            {
                case AESOps.Encrypt:
                    dir = Path.Combine(new string[] {
                                    this.FilePath.Replace(Path.GetFileName(this.FilePath), ""),
                                    "ECV_" + Path.GetFileName(this.FilePath)
                                });
                    break;
                    
                case AESOps.Decrypt:
                    dir = "";
                    break;
            }

            File.Create(dir);
            File.WriteAllText(dir, content);
        }

        public void work(string password)
        {
            this.Password = password;
            this._gcm = new AESGCM(Encoding.ASCII.GetBytes(password));
            if (this.Operation == "check")
            {
                this.Check();
            }
            else if (this.Operation == "create")
            {
                this.Create();
            }
            else if (this.Operation == "recover")
            {
                this.Recover();
            }
        }
    }
}