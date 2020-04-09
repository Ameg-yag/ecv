using System;
using System.IO;
using ecv.main;

namespace ecv.parsing
{
    public class Parser
    {
        private string _usage = @"ECV - Encrypt Configuration Variables
Encrypt configuration files such as json, yaml, xml with strong symmetric encryption.
After choosing a file, a prompt will appear to insert a encryption key. 
Consider using an strong key. You can use some password generator, such: https://bitbox.tarcisiomarinho.io
This program only let you encrypt with strong credentials, password strenght will be checked.

Usage: 
    Create new encrypted file 
    ./ecv create {file}
    ./ecv recover {file}
    ./ecv check {file}

Examples: 
    ./ecv create /home/$USER/MyApp/appsettings.json  // Encrypt the appsettings.json file with your supplied key
    ./ecv recover /home/$USER/MyApp/appsettings.json // recover the appsettings.json encrypted with your supplied key
    ./ecv check /home/$USER/MyApp/appsettings.json // Check file integrity appsettings.json with your supplied key

Algorithm used: AES with GCM operation mode.    

Errors:
    -
    -
    -
        ";

        public string Usage { get => _usage; }

        private void Error(string msg, int statusCode)
        {
            Console.WriteLine(msg);
            System.Environment.Exit(statusCode);
        }

        public Main Parse(string[] args)
        {
            if (args[0] == "-h" || args[0] == "--help")
            {
                this.Error(this._usage, 0);
            }

            if (args.Length != 3)
            {
                this.Error("Invalid number of arguments, use -h or --help for usage.\n", 1);
            }
            
            
            if (args[1] != "create" && args[1] != "recover" || args[1] != "check")
            {
                this.Error("invalid argument: " + args[1] + "\nUsage: ecv --help\n", 2);
            }

            if (!File.Exists(args[2]) )
            {
                this.Error("File : "+ args[2] + " doesn't exist.", 3);
            }

            return new Main { Operation = args[1], FilePath = args[2]};
        }
    }

}