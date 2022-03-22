using System.Data;
using MySql.Data.MySqlClient;

namespace NHLCafe.Pages
{
    public static class DBUtils
    {
        public static IDbConnection Connect()
        {
            return new MySqlConnection(
                "Server=127.0.0.1;Port=3306; " +
                "Database=nhlcafedb; " +
                "Uid=root; Pwd=1230;"
            );
        }
    }
}