using UnityEngine;
using UnityEngine.UI;

public class MyScrollbarScrolled : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<Scrollbar>().onValueChanged.AddListener((float val) => ScrollbarCallback(val));
    }

    void ScrollbarCallback(float value)
    {
        Transform headerContentGameObject = gameObject.transform.parent.parent.GetChild(1).GetChild(0).GetChild(0);
        RectTransform headerContentRectTransform = headerContentGameObject.GetComponent<RectTransform>();
        RectTransform contentRectTransform = gameObject.transform.parent.GetChild(0).GetChild(0).GetComponent<RectTransform>();
        headerContentRectTransform.localPosition = new Vector3(contentRectTransform.localPosition.x,
            headerContentRectTransform.localPosition.y, headerContentRectTransform.localPosition.z);
    }
}
