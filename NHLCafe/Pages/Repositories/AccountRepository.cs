using System.Collections.Generic;
using System.Linq;
using Dapper;

namespace NHLCafe.Pages
{
    public static class AccountRepository
    {
        public static Account GetAccount(string username, string password)
        {
            using (var db = DBUtils.Connect())
            {
                return db.QuerySingleOrDefault<Account>("SELECT * FROM Account WHERE Username = @Username AND Password = @Password", new
                {
                    Username = username,
                    Password = password
                });
            }
        }
        
        
        
        public static Account AddAccount(Account account)
        {
            using var db = DBUtils.Connect();
            int newAccountIdId = db.ExecuteScalar<int>(
                @"INSERT INTO Account (Email, Username, Password) 
                    VALUES (@Email, @Username, @Password); 
                    SELECT LAST_INSERT_ID();", new
                {
                    Email = account.Email,
                    Username = account.Username,
                    Password = account.Password
                });
            account.AccountId = newAccountIdId;

            return account;
        }
        
        public static bool AccountEmailNotExists(string accountEmail)
        {
            if (!string.IsNullOrWhiteSpace(accountEmail))
            {
                accountEmail = accountEmail.Trim().ToLower();
            }

            using var db = DBUtils.Connect();
            int rowCount = db.ExecuteScalar<int>(
                "SELECT COUNT(1) FROM Account WHERE Email = @Email", 
                new { Email = accountEmail}
            );

            return rowCount < 1;
        }
    }
}