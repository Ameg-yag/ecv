
using ecv.crypto;
using ecv.enums;
using System.IO;
using System.Text;

namespace ecv.main
{
    public class Main
    {
        public string Operation { get; set; }
        public string FilePath { get; set; }
        private string Password;
        private AESGCM _gcm;

        public Main(string Operation, string FilePath)
        {
            this.Operation = Operation;
            this.FilePath = FilePath;
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
            string content = handleLargeFile(AESOps.Decrypt);
        }

        private void Create()
        {
            string content = handleLargeFile(AESOps.Encrypt);
        }

        private void Recover()
        {
            string content = handleLargeFile(AESOps.Decrypt);
        }

        private void StoreToDisk(string content)
        {
            var newFile = "newfile.txt";
            File.Create(newFile);
            File.WriteAllText(newFile, content);
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