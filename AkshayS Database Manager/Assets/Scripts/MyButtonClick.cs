using UnityEngine;
using UnityEngine.UI;

public class MyButtonClick : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Button button = gameObject.GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(ButtonOnClick);
        }
    }

    void ButtonOnClick()
    {
        Debug.Log($"Button's associated Text value is {gameObject.GetComponentInChildren<Text>().text}");
    }
}
