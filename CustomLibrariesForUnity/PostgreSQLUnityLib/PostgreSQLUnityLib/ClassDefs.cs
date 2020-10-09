using System;

namespace PostgreSQLUnityLib
{
    [Serializable]
    public class PostgreSQLConnProps
    {
        public string serverName = "127.0.0.1";
        public string databaseName = "akshaysdemodb";
        public string userID = "postgres";
        public string password = "P@ssword123";
    }

    public class QueryResultColumn
    {
        public string name;
    }

    public class QueryResultRow
    {
        public string[] columnData;
    }

    public class QueryResult
    {
        public QueryResultColumn[] queryResultColumns;
        public QueryResultRow[] queryResultRows;
    }
}
