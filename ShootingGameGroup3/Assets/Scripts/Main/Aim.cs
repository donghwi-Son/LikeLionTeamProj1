using UnityEngine;

public class Aim : MonoBehaviour
{
    Vector3 mousePos;

    void FixedUpdate()
    {
        mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        transform.position = mousePos;
    }

    public Vector3 GetMousePos()
    {
        return new Vector3(mousePos.x, mousePos.y, 0);
    }
}
