# ECV - Encrypt Configuration Variables

When developing web applications there's configuration variables, such as db connection string, passwords, etc 
that need to be encrypted to be versioned. 
When uploading sensitive information to the versioning system, such as github, gitlab, etc, you take some risks.
Sensitive information should never be leaked.

With this program, you can store senstive information inside a encrypted configuration file, and upload


# Usage

./ecv create {file} // create a new secure file

./ecv recover {file} // decrypt a file

./ecv check {file} // check integrity


# Algorithm

AES with GCM operation mode


# TODO 

    Support CI/CD pipelines reading password from file

    future usage: 
        ./ecv recover {fileDecrypt} {passwordPath}
