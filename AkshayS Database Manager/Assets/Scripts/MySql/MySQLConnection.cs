using UnityEngine;
using MySqlUnityLib;
using System;

public class MySQLConnection : MonoBehaviour
{
    public MySqlConnProps mySqlConnProps;

    public MySqlConn InitMySqlConn()
    {
        MySqlConn mySqlConn = new MySqlConn();
        mySqlConn.InitializeMySqlConn(mySqlConnProps);
        return mySqlConn;
    }
}
