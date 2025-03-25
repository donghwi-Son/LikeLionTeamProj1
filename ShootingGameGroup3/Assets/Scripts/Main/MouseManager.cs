using UnityEngine;

public class MouseManager : MonoBehaviour
{
    Vector3 mousePos;

    void FixedUpdate()
    {
        mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
    }

    public Vector3 GetMousePos()
    {
        return mousePos;
    }
}
