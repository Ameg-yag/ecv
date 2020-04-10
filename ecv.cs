using System;
using System.Security.Cryptography;
using System.Text;
using ecv.crypto;
using ecv.getpass;
using ecv.parsing;
using ecv.exceptions;
using ecv.entropy;

namespace ecv
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser p = new Parser();
            var obj = p.Parse(args);
            Console.Write("Password: ");
            // var password = GetPass.Prompt();
            var password = "testing_password";

            try
            {
                obj.work(password);
            }
            catch (Exception e)
            {
                if (e is EncryptionFailedException || e is DecryptionFailedException || e is FileReadException)
                {
                    Console.WriteLine(e.Message);
                }
                else
                {
                    Console.WriteLine("Unknown exception happened\n", e.Message);
                }
            }
        }
    }
}