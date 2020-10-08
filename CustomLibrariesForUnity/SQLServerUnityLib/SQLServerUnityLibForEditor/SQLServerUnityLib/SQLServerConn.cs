using System.Data.SqlClient;

namespace SQLServerUnityLib
{
    public class SQLServerConn
    {
        internal SqlConnection mySqlConnection;

        public SQLServerConn(SQLServerConnProps props)
        {
            mySqlConnection = new SqlConnection($"Server={props.serverName};Database={props.databaseName};User={props.userID};Password={props.password};");
        }

        public void Open()
        {
            if(mySqlConnection != null && mySqlConnection.State == System.Data.ConnectionState.Closed)
            {
                mySqlConnection.Open();
            }
        }

        public void Close()
        {
            if (mySqlConnection != null && mySqlConnection.State == System.Data.ConnectionState.Open)
            {
                mySqlConnection.Close();
            }
        }
    }
}
