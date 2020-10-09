using PostgreSQLUnityLib;
using System;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            PostgreSQLConnProps postgreSQLConnProps = new PostgreSQLConnProps();
            PostgreSQLConn postgreSQLConn = new PostgreSQLConn();
            postgreSQLConn.InitializeMySqlConn(postgreSQLConnProps);
            PostgreSQLUtilities postgreSQLUtilities = new PostgreSQLUtilities();
            QueryResult queryResult = postgreSQLUtilities.GetQueryResult(postgreSQLConn, "select * from recipes");
            Console.WriteLine(queryResult.queryResultRows[0].columnData[1]);
            Console.ReadLine();
        }
    }
}
