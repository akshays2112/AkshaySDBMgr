using Npgsql;

namespace PostgreSQLUnityLib
{
    public class PostgreSQLConn
    {
        internal NpgsqlConnection postgreSQLConnection;

        public void InitializeMySqlConn(PostgreSQLConnProps props)
        {
            postgreSQLConnection = new NpgsqlConnection($"Server={props.serverName};Database={props.databaseName};User={props.userID};Password={props.password};TreatTinyAsBoolean=true;");
        }

        public void ConnectToMySQL()
        {
            if(postgreSQLConnection != null && postgreSQLConnection.State == System.Data.ConnectionState.Closed)
            {
                postgreSQLConnection.Open();
            }
        }

        public void DisconnectFromMySql()
        {
            if (postgreSQLConnection != null && postgreSQLConnection.State == System.Data.ConnectionState.Open)
            {
                postgreSQLConnection.Close();
            }
        }
    }
}
