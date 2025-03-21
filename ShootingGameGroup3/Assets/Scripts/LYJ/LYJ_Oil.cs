using UnityEngine;

public class LYJ_Oil : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            spriteRenderer.color = Color.red;
            Destroy(gameObject, 1f);
        }
    }
}