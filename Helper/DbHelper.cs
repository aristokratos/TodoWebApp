using Microsoft.Data.SqlClient;

namespace AnotherTodo.Helper
{
    public class DbHelper
    {
        public static SqlConnection connection()
        {
            return new SqlConnection(@"Data Source=DESKTOP-4CFIOMJ;Initial Catalog=TodoNew_Db;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
    }
}
