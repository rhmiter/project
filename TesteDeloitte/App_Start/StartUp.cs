using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Configuration;
using System.IO;
using System.Web.Configuration;

namespace TesteDeloitte
{
    public class StartUp
    {
        public static void ExecuteSQL()
        {
            string script = File.ReadAllText($"{AppDomain.CurrentDomain.BaseDirectory}\\Data\\Deloitte.sql");

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TesteDeloitte"].ToString());

            Server server = new Server(new ServerConnection(conn));

            server.ConnectionContext.ExecuteNonQuery(script);
        }
    }
}
