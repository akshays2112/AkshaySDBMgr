using UnityEngine;
using Shapes;

public class TestCreateShapesRect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject goRect = new GameObject();
        Rectangle rect = goRect.AddComponent<Rectangle>();
        rect.Width = rect.Height = 3f;
        rect.Color = new Color(0.5f, 0f, 0f, 0.5f);
        BoxCollider bcRect = goRect.AddComponent<BoxCollider>();
        bcRect.size = new Vector3(3f, 3f, 0.0001f);
    }
}
