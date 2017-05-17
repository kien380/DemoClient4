using System;
using System.Collections.Generic;
using System.Text;

namespace App1
{
    public class AccountLoginVm
    {
        public List<AccountLogin> Accounts { get; set; }

        public AccountLoginVm()
        {
            Accounts = new List<AccountLogin>();
            Accounts.Add(new AccountLogin() { Username = "Admin", Password = "admin123"});
            Accounts.Add(new AccountLogin() { Username = "BaoNV", Password = "bao123" });
        }
    }
}
