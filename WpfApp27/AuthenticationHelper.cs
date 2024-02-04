using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp27
{
    public class AuthenticationHelper
    {
        public static bool VerifyPassword(string enteredPassword, string storedHash)
        {
            string enteredPasswordHash = HashHelper.HashPassword(enteredPassword);
            return enteredPasswordHash == storedHash;
        }
    }
}
