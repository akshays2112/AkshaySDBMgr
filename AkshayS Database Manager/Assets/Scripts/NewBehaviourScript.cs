using UnityEngine;
//using Microsoft.Data.SqlClient;
using UnityEngine.UI;
using SQLServerUnityLib;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SQLServerConnProps sqlServerConnProps = new SQLServerConnProps();
        sqlServerConnProps.serverName = "192.168.29.224";
        sqlServerConnProps.userID = "sa";
        SQLServerConn sqlServerConn = new SQLServerConn(sqlServerConnProps);
        SQLServerUtilities sqlServerUtilities = new SQLServerUtilities();
        QueryResult queryResult = sqlServerUtilities.GetQueryResult(sqlServerConn, "select Name from Recipes");
        gameObject.GetComponent<Text>().text = queryResult.queryResultRows[0].columnData[0];
    }

    //void SimpleTestSQLServer()
    //{
    //    SqlConnection sqlConnection = new SqlConnection("Server=192.168.29.224;Database=AkshaySDemoDB;User=sa;Password=P@ssword123;");
    //    sqlConnection.Open();
    //    SqlCommand sqlCommand = new SqlCommand("select name from recipes", sqlConnection);
    //    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
    //    while (sqlDataReader.Read())
    //    {
    //        gameObject.GetComponent<Text>().text = sqlDataReader["Name"].ToString();
    //        break;
    //    }
    //    sqlDataReader.Close();
    //    sqlConnection.Close();
    //}
}
