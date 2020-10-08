using System.Collections.Generic;
using UnityEngine;
using SQLServerUnityLib;

public class LightWeightSQLServerDataSource : MonoBehaviour
{
    public GameObject sqlConnectionGameObject;
    public string queryString = "select * from dbo.Recipes";
    public int maxRowsToGet = 1000;

    public QueryResult GetData()
    {
        SQLServerConnection mySqlConnection = sqlConnectionGameObject.GetComponent<SQLServerConnection>();
        SQLServerConn mySqlConn = mySqlConnection.InitMySqlConn();
        SQLServerUtilities mySqlUtilities = new SQLServerUtilities();
        return mySqlUtilities.GetQueryResult(mySqlConn, queryString);
    }
}
