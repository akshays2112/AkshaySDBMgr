using System;

namespace PostgreSQLUnityLib
{
    [Serializable]
    public class PostgreSQLConnProps
    {
        public string serverName = "192.168.29.251";
        public string databaseName = "akshaysdemodb";
        public string userID = "akshays";
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
