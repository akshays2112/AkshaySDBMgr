using Npgsql;
using System.Collections.Generic;

namespace PostgreSQLUnityLib
{
    public class PostgreSQLUtilities
    {
        public QueryResult GetQueryResult(PostgreSQLConn postgreSQLConn, string query)
        {
            QueryResult queryResult = new QueryResult();
            postgreSQLConn.ConnectToMySQL();
            NpgsqlCommand postgreSQLCommand = new NpgsqlCommand(query, postgreSQLConn.postgreSQLConnection);
            using (NpgsqlDataReader postgreSQLDataReader = postgreSQLCommand.ExecuteReader())
            {
                int fieldCount = postgreSQLDataReader.FieldCount;
                if (fieldCount > 0)
                {
                    queryResult.queryResultColumns = new QueryResultColumn[fieldCount];
                    for (int colIndex = 0; colIndex < fieldCount; colIndex++)
                    {
                        queryResult.queryResultColumns[colIndex] = new QueryResultColumn();
                        queryResult.queryResultColumns[colIndex].name = postgreSQLDataReader.GetName(colIndex);
                    }
                    List<List<string>> data = new List<List<string>>();
                    while (postgreSQLDataReader.Read())
                    {
                        List<string> rowData = new List<string>();
                        for (int colIndex = 0; colIndex < fieldCount; colIndex++)
                        {
                            rowData.Add(postgreSQLDataReader[colIndex].ToString());
                        }
                        data.Add(rowData);
                    }
                    queryResult.queryResultRows = new QueryResultRow[data.Count];
                    for (int rowIndex = 0; rowIndex < data.Count; rowIndex++)
                    {
                        queryResult.queryResultRows[rowIndex] = new QueryResultRow();
                        queryResult.queryResultRows[rowIndex].columnData = data[rowIndex].ToArray();
                    }
                }
            }
            postgreSQLConn.DisconnectFromMySql();
            return queryResult;
        }
    }
}
