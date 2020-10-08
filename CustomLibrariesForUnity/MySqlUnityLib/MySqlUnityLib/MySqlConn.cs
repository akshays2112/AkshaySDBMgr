using MySql.Data.MySqlClient;

namespace MySqlUnityLib
{
    public class MySqlConn
    {
        internal MySqlConnection mySqlConnection;

        public void InitializeMySqlConn(MySqlConnProps props)
        {
            mySqlConnection = new MySqlConnection($"Server={props.serverName};Database={props.databaseName};User={props.userID};Password={props.password};TreatTinyAsBoolean=true;");
        }

        public void ConnectToMySQL()
        {
            if(mySqlConnection != null && mySqlConnection.State == System.Data.ConnectionState.Closed)
            {
                mySqlConnection.Open();
            }
        }

        public void DisconnectFromMySql()
        {
            if (mySqlConnection != null && mySqlConnection.State == System.Data.ConnectionState.Open)
            {
                mySqlConnection.Close();
            }
        }
    }
}
