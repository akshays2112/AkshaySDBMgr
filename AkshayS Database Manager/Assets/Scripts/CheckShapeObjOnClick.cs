using UnityEngine;
using Shapes;

public class CheckShapeObjOnClick : MonoBehaviour
{
    void Update()
    {
        // Check for mouse input
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            // Casts the ray and get the first game object hit
            Physics.Raycast(ray, out hit);
            Debug.Log("This hit at " + hit.collider?.GetComponent<Rectangle>()?.Color.ToString());
        }
    }
}
