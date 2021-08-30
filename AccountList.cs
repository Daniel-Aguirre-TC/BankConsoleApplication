using System;
using System.Collections.Generic;
using System.Text;

namespace BankEncapsulation
{
    public static class AccountList
    {
        public static List<BankAccount> allAccounts = new List<BankAccount>()
        {
            new BankAccount("Daniel.Aguirre", "1234", 1000),

        };
    }
}
