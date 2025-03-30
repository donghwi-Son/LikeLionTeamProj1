using System.Collections;
using UnityEngine;

public class LYJ_NormalEnemy : MonoBehaviour
{
    float[] hpForWave = { 100, 150, 200, 250, 300 }; // temp
    float hp;
    float moveSpeed = 1.5f;
    bool isHitRecent;
    Rigidbody2D _rb;
    SpriteRenderer spriteRenderer;
    Transform target;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        target = LYJ_GameManager.Instance.Player.transform;
        isHitRecent = false;
    }

    void OnEnable()
    {
        hp = hpForWave[LYJ_GameManager.Instance.SpawnManager.CurrentWave];
        isHitRecent = false;
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (isHitRecent) { return; }
        if (collision.CompareTag("Bullet"))
        {
            hp -= collision.GetComponent<LYJ_Bullet>().Damage;
        }
        if (collision.CompareTag("Alcohol"))
        {
            hp -= collision.GetComponent<LYJ_AlcoholBurner>().Damage;
        }
        StartCoroutine(HitReaction());

        if (hp <= 0)
        {
            Die();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Aggro")) {return;}
        target = LYJ_GameManager.Instance.Player.transform;
    }

    void FixedUpdate()
    {
        Vector2 nextVec = (target.position-transform.position).normalized * moveSpeed;
        _rb.linearVelocity = nextVec;
        spriteRenderer.flipX = target.transform.position.x < transform.position.x;
    }

    IEnumerator HitReaction()
    {
        isHitRecent = true;
        yield return new WaitForSeconds(0.1f);
        isHitRecent = false;
    }

    void Die()
    {
        StopCoroutine(HitReaction());
        gameObject.SetActive(false);
    }
}
