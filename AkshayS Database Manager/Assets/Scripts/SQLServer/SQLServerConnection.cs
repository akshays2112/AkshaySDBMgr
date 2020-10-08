using UnityEngine;
using SQLServerUnityLib;

public class SQLServerConnection : MonoBehaviour
{
    public SQLServerConnProps mySqlConnProps;

    public SQLServerConn InitMySqlConn()
    {
        SQLServerConn mySqlConn = new SQLServerConn(mySqlConnProps);
        return mySqlConn;
    }
}
