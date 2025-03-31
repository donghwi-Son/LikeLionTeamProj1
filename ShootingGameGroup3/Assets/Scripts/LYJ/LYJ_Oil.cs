using System.Collections;
using UnityEngine;

public class LYJ_Oil : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    bool isBurn;
    public bool IsBurn => isBurn;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        isBurn = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            StartCoroutine(OilBurning());
        }
    }

    IEnumerator OilBurning()
    {
        isBurn = true;
        yield return new WaitForSeconds(3f);
        isBurn = false;
        PoolManager.Instance.ReturnGameObject(gameObject);
    }
}