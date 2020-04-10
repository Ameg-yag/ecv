# ECV - Encrypt Configuration Variables

When developing web applications there's configuration variables, such as db connection string, passwords, etc 
that need to be encrypted to be versioned. 

When uploading sensitive information to the versioning system, such as github, gitlab, etc, you take some risks.

Sensitive information should never be leaked.

With this program, you can store senstive information inside a encrypted configuration file, and upload


# Usage

ECV - Encrypt Configuration Variables

Encrypt configuration files such as json, yaml, xml with strong symmetric encryption.

After choosing a file, a prompt will appear to insert a encryption key. 

Consider using an strong key. You can use some password generator, such: https://bitbox.tarcisiomarinho.io

This program only let you encrypt with strong credentials, password strenght will be checked.


    Create new encrypted file: 

    ./ecv create {file}

    Recover encrypted file:

    ./ecv recover {file}

    Check file/password integrity:

    ./ecv check {file}

**Examples**: 

    ./ecv create /home/$USER/MyApp/appsettings.json  // Encrypt the appsettings.json file with your supplied key

    ./ecv recover /home/$USER/MyApp/appsettings.json // recover the appsettings.json encrypted with your supplied key

    ./ecv check /home/$USER/MyApp/appsettings.json // Check file integrity appsettings.json with your supplied key


**Success**:

    - File created successfully - exit code: 0

    - File is reliable - exit code: 0

    - File recovered successfully - exit code: 0
**Errors**:

    - Invalid number of arguments - exit code: 1 

    - File supplied doesn't exist - exit code: 2

    - Couldn't create a new file - exit code: 3

    - Couldn't open the file - exit code: 3

    - Integrity Failed - exit code: 3

    - Failed encrypting the file - exit code: 3

    - Unknown exception happened - exit code: 4

# Algorithm

AES with GCM operation mode


# TODO 

    - [] Support CI/CD pipelines reading password from file
