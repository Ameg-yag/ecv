using System;

namespace ecv.entropy
{
    public static class PasswordStrenght
    {
        public static bool IsStrongPassword(string password) => Math.Log2(Math.Pow(95, password.Length)) > 60;
    }
}