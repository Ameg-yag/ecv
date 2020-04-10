
using ecv.crypto;
using ecv.enums;
using ecv.exceptions;
using ecv.files;
using ecv.response;
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

        private byte[] handleLargeFile(AESOps op)
        {
            byte[] content = null;
            try
            {
                content = System.IO.File.ReadAllBytes(this.FilePath);
            }
            catch (Exception e)
            {
                throw new FileReadException("Couldn't open file: " + this.FilePath);
            }
            switch (op)
            {
                case AESOps.Encrypt:
                    return this._gcm.Encrypt(content);

                case AESOps.Decrypt:
                    return this._gcm.Decrypt(content);
            }
            return null;
        }

        private Response Check()
        {
            try
            {
                this.handleLargeFile(AESOps.Decrypt);
            }
            catch (CryptographicException e)
            {
                throw new DecryptionFailedException("Integrity Failed for file: " + this.FilePath + " Corrupted file or wrong password\n");
            }
            return new Response { Code = 0, Message = "File is reliable: " + this.FilePath };
        }

        private Response Create()
        {
            byte[] content = null;
            try
            {
                content = this.handleLargeFile(AESOps.Encrypt);
            }
            catch (CryptographicException e)
            {
                throw new EncryptionFailedException("Failed encrypting file: " + this.FilePath + "\n");
            }
            
            var newFile = Files.StoreToDisk(this.FilePath, AESOps.Encrypt, content);
            return new Response { Code = 0, Message = "File created successfully: " + newFile };
        }

        private Response Recover()
        {
            byte[] content = null;
            try
            {
                content = this.handleLargeFile(AESOps.Decrypt);
            }
            catch (CryptographicException e)
            {
                throw new DecryptionFailedException("Failed Recovering file: " + this.FilePath + " File corrupted or wrong password.\n");
            }
            var newFile = Files.StoreToDisk(this.FilePath, AESOps.Decrypt, content);
            return new Response { Code = 0, Message = "File recovered successfully: " + newFile };
        }


        public Response work(string password)
        {
            this.Password = password;
            this._gcm = new AESGCM(Encoding.ASCII.GetBytes(password));
            if (this.Operation == "check")
            {
                return this.Check();
            }
            else if (this.Operation == "create")
            {
                return this.Create();
            }
            else
            {
                return this.Recover();
            }
        }
    }
}