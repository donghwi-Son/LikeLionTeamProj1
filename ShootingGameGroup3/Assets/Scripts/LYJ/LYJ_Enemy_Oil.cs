using System.Collections;
using UnityEngine;

public class LYJ_Enemy_Oil : MonoBehaviour
{
    private float moveSpeed = 1.5f;
    Rigidbody2D _rb;
    WaitForSeconds oilDropInterval;
    SpriteRenderer spriteRenderer;
    [SerializeField]
    GameObject oil;

    Transform target;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        oilDropInterval = new WaitForSeconds(2f);
        spriteRenderer = GetComponent<SpriteRenderer>();
        target = LYJ_GameManager.Instance.Player.transform;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag("Aggro")) {return;}
        target = collision.transform;
        if (target == null)
        {
            target = LYJ_GameManager.Instance.Player.transform;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Aggro")) {return;}
        target = LYJ_GameManager.Instance.Player.transform;
    }

    void OnEnable()
    {
        StartCoroutine(DropOil());
    }

    void FixedUpdate()
    {
        Vector2 nextVec = (target.position-transform.position).normalized * moveSpeed;
        _rb.linearVelocity = nextVec;
        spriteRenderer.flipX = target.transform.position.x < transform.position.x;
    }

    IEnumerator DropOil()
    {
        while (gameObject.activeSelf)
        {
            yield return oilDropInterval;
            Instantiate(oil, transform.position, Quaternion.identity); // 나중에 오브젝트 풀링으로 수정 필요
        }
    }
}
