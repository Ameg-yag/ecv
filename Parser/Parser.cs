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
Success:
    - File created successfully - exit code: 0
    - File is reliable - exit code: 0
    - File recovered successfully - exit code: 0
Errors:
    - Invalid number of arguments - exit code: 1 
    - File supplied doesn't exist - exit code: 2
    - Couldn't create a new file - exit code: 3
    - Couldn't open the file - exit code: 3
    - Integrity Failed - exit code: 3
    - Failed encrypting the file - exit code: 3
    - Unknown exception happened - exit code: 4";

        public string Usage { get => _usage; }

        private void Error(string msg, int statusCode)
        {
            Console.WriteLine(msg);
            System.Environment.Exit(statusCode);
        }

        public Main Parse(string[] args)
        {
            if(args.Length < 1){
                this.Error("Invalid number of arguments, use -h or --help for usage.", 1);
            }

            if (args[0] == "-h" || args[0] == "--help")
            {
                this.Error(this._usage, 0);
            }

            if (args.Length != 2)
            {
                this.Error("Invalid number of arguments, use -h or --help for usage.", 1);
            }
            
            
            if (args[0] != "create" && args[0] != "recover" && args[0] != "check")
            {
                this.Error("invalid argument: " + args[0] + "\nUsage: ecv --help", 1);
            }

            if (!File.Exists(args[1]))
            {
                this.Error("File : "+ args[1] + " doesn't exist.", 2);
            }

            return new Main { Operation = args[0], FilePath = args[1]};
        }
    }

}