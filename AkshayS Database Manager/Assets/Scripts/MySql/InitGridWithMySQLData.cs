using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MySqlUnityLib;

public class InitGridWithMySQLData : MonoBehaviour
{
    public GameObject contentGameObjectOfControl;
    public bool genericHeaderItems = false;
    public bool genericRowItems = false;
    public GameObject prefabGenericEmptyGameObject;
    public GameObject prefabGenericText;
    public GameObject prefabCustomHeaderListItem;
    public GameObject prefabCustomRowListItem;
    public GameObject dataSource;
    public string gridTitle;
    public ClassDefs.Margins margins;

    // Start is called before the first frame update
    private void Start()
    {
        if (contentGameObjectOfControl != null && ((!genericRowItems && prefabCustomRowListItem != null) ||
            (genericRowItems && prefabGenericEmptyGameObject != null && prefabGenericText != null)) &&
            ((!genericHeaderItems && prefabCustomHeaderListItem != null) || (genericHeaderItems && prefabGenericEmptyGameObject != null &&
            prefabGenericText != null)))
        {
            if (dataSource != null)
            {
                QueryResult queryResult = dataSource.GetComponent<LightWeightMySQLDataSource>().GetData();
                GameObject listHeaderItemGameObject = null;
                if (queryResult != null && queryResult.queryResultColumns != null && queryResult.queryResultColumns.Length > 0)
                {
                    if (genericHeaderItems)
                    {
                        listHeaderItemGameObject = Instantiate(prefabGenericEmptyGameObject,
                            contentGameObjectOfControl.transform.parent.parent.parent.transform.GetChild(1).GetChild(0).GetChild(0), false);
                        for (int colIndex = 0; colIndex < queryResult.queryResultColumns.Length; colIndex++)
                        {
                            Instantiate(prefabGenericText, listHeaderItemGameObject.transform, false);
                        }
                    }
                    else
                    {
                        listHeaderItemGameObject = Instantiate(prefabCustomHeaderListItem,
                            contentGameObjectOfControl.transform.parent.parent.parent.transform.GetChild(1).GetChild(0).GetChild(0), false);
                    }
                    if (listHeaderItemGameObject != null)
                    {
                        int colIndex = 0;
                        for (int childIndex = 0; childIndex < listHeaderItemGameObject.transform.childCount &&
                            colIndex < queryResult.queryResultColumns.Length; childIndex++)
                        {
                            Text text = listHeaderItemGameObject.transform.GetChild(childIndex).GetComponent<Text>();
                            if (text != null)
                            {
                                text.text = queryResult.queryResultColumns[colIndex].name;
                                colIndex++;
                            }
                        }
                    }
                }
                List<GameObject> listRowItemGameObjects = new List<GameObject>();
                if (queryResult != null && queryResult.queryResultColumns != null && queryResult.queryResultColumns.Length > 0 &&
                    queryResult.queryResultRows != null && queryResult.queryResultRows.Length > 0)
                {

                    foreach (QueryResultRow rowData in queryResult.queryResultRows)
                    {
                        GameObject listRowItemGameObject = null;
                        if (genericRowItems)
                        {
                            listRowItemGameObject = Instantiate(prefabGenericEmptyGameObject, contentGameObjectOfControl.transform, false);
                            for (int colIndex = 0; colIndex < rowData.columnData.Length; colIndex++)
                            {
                                Instantiate(prefabGenericText, listRowItemGameObject.transform, false);
                            }
                        }
                        else
                        {
                            listRowItemGameObject = Instantiate(prefabCustomRowListItem, contentGameObjectOfControl.transform, false);
                        }
                        if (listRowItemGameObject != null)
                        {
                            int colIndex = 0;
                            for (int childIndex = 0; childIndex < listRowItemGameObject.transform.childCount &&
                                colIndex < rowData.columnData.Length; childIndex++)
                            {
                                Text text = listRowItemGameObject.transform.GetChild(childIndex).GetComponent<Text>();
                                if (text != null)
                                {
                                    text.text = rowData.columnData[colIndex];
                                    colIndex++;
                                }
                            }
                        }
                        listRowItemGameObjects.Add(listRowItemGameObject);
                    }
                }
                ClassDefs.ListControlDataProperties listControlDataProperties = new ClassDefs.ListControlDataProperties();
                listControlDataProperties.genericHeaderItems = genericHeaderItems;
                listControlDataProperties.genericListItems = genericRowItems;
                listControlDataProperties.title = gridTitle;
                listControlDataProperties.listRowGameObjects = listRowItemGameObjects.ToArray();
                listControlDataProperties.listHeaderGameObject = listHeaderItemGameObject;
                listControlDataProperties.margins = margins;
                contentGameObjectOfControl.GetComponent<GridControlData>().DrawData(listControlDataProperties);
            }
        }
        else
        {
            ClassDefs.ListControlDataProperties listControlDataProperties = new ClassDefs.ListControlDataProperties();
            contentGameObjectOfControl.GetComponent<GridControlData>().DrawData(listControlDataProperties);
        }
    }
}
