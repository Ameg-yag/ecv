# ECV - Encrypt Configuration Variables

When developing web applications there's configuration variables, such as db connection string, passwords, etc 
that need to be encrypted to be versioned. 
When uploading sensitive information to the versioning system, such as github, gitlab, etc, you take some risks.
Sensitive information should never be leaked.

With this program, you can store senstive information inside a encrypted configuration file, and upload


# Usage

./ecv create // create a new secure file
./ecv recover // decrypt a file
./ecv check  // check integrity
