using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace MySqlUnityLib
{
    public class MySqlUtilities
    {
        public QueryResult GetQueryResult(MySqlConn mySqlConn, string query)
        {
            QueryResult queryResult = new QueryResult();
            mySqlConn.ConnectToMySQL();
            MySqlCommand mySqlCommand = new MySqlCommand(query, mySqlConn.mySqlConnection);
            using (MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader())
            {
                int fieldCount = mySqlDataReader.FieldCount;
                if (fieldCount > 0)
                {
                    queryResult.queryResultColumns = new QueryResultColumn[fieldCount];
                    for (int colIndex = 0; colIndex < fieldCount; colIndex++)
                    {
                        queryResult.queryResultColumns[colIndex] = new QueryResultColumn();
                        queryResult.queryResultColumns[colIndex].name = mySqlDataReader.GetName(colIndex);
                    }
                    List<List<string>> data = new List<List<string>>();
                    while (mySqlDataReader.Read())
                    {
                        List<string> rowData = new List<string>();
                        for (int colIndex = 0; colIndex < fieldCount; colIndex++)
                        {
                            rowData.Add(mySqlDataReader[colIndex].ToString());
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
            mySqlConn.DisconnectFromMySql();
            return queryResult;
        }
    }
}
