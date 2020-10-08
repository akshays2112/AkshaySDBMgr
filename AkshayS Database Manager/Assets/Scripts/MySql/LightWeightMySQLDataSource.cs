using System.Collections.Generic;
using UnityEngine;
using MySqlUnityLib;

public class LightWeightMySQLDataSource : MonoBehaviour
{
    public GameObject sqlConnectionGameObject;
    public string queryString = "select * from dbo.Recipes";
    public int maxRowsToGet = 1000;

    public QueryResult GetData()
    {
        MySQLConnection mySqlConnection = sqlConnectionGameObject.GetComponent<MySQLConnection>();
        MySqlConn mySqlConn = mySqlConnection.InitMySqlConn();
        MySqlUtilities mySqlUtilities = new MySqlUtilities();
        return mySqlUtilities.GetQueryResult(mySqlConn, queryString);
    }
}
