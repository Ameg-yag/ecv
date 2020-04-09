using System;
using System.Security.Cryptography;
using System.Text;
using ecv.crypto;
using ecv.getpass;
using ecv.parsing;

namespace ecv
{
    class Program
    {

        static void Main(string[] args)
        {


            Parser p = new Parser();

            var obj = p.Parse(args);
            var password = GetPass.Prompt();
            obj.work(password);

        }
    }
}