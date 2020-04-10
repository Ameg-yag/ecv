using System;
using System.Security.Cryptography;
using System.Text;
using ecv.crypto;
using ecv.getpass;
using ecv.parsing;
using ecv.exceptions;
using ecv.entropy;
using ecv.response;

namespace ecv
{
    class Program
    {

        static void Exit(string message, int code)
        {
            Console.WriteLine(message);
            System.Environment.Exit(code);
        }

        static void Main(string[] args)
        {
            Parser p = new Parser();
            var obj = p.Parse(args);
            Console.Write("Password: ");
            // var password = GetPass.Prompt();
            var password = "testing_password";
            Response resp = null;
            try
            {
                resp = obj.work(password);
            }
            catch (Exception e)
            {
                if (e is EncryptionFailedException || e is DecryptionFailedException || e is FileReadException)
                {
                    Exit(e.Message, 3);
                }
                else
                {
                    Exit("Unknown exception happened: " + e.Message, 4);
                }
            }
            Exit(resp.Message, resp.Code);
        }
    }
}