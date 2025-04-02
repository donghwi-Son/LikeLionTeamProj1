using UnityEngine;

public class sdh_Portal : MonoBehaviour
{

    public Canvas canvas;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Instantiate(canvas);
        }
    }
}
