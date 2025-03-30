using System.Collections;
using UnityEngine;

public class LYJ_NormalEnemy : MonoBehaviour
{
    float[] hpForWave = { 100, 150, 200, 250, 300 }; // temp
    float hp;
    float moveSpeed = 1.5f;
    bool isHitRecent;
    bool isHitWithOil;
    Rigidbody2D _rb;
    SpriteRenderer spriteRenderer;
    Transform target;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        target = LYJ_GameManager.Instance.Player.transform;
        isHitRecent = false;
        isHitWithOil = false;
    }

    void OnEnable()
    {
        hp = hpForWave[LYJ_GameManager.Instance.SpawnManager.CurrentWave];
        isHitRecent = false;
        isHitWithOil = false;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag("Aggro")) { return; }
        target = collision.transform;
        if (target == null)
        {
            target = LYJ_GameManager.Instance.Player.transform;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isHitRecent)
        {
            if (collision.CompareTag("Bullet"))
            {
                hp -= collision.GetComponent<LYJ_Bullet>().Damage;
            }
            if (collision.CompareTag("Alcohol"))
            {
                hp -= collision.GetComponent<LYJ_AlcoholBurner>().Damage;
            }
            StartCoroutine(HitReaction());
        }
        if (!isHitWithOil && collision.CompareTag("Oil") && collision.GetComponent<LYJ_Oil>().IsBurn)
        {
            hp -= 0.5f;
            StartCoroutine(OilReaction());
        } 
        

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
    IEnumerator OilReaction()
    {
        isHitWithOil = true;
        yield return new WaitForSeconds(0.5f);
        isHitWithOil = false;
    }

    void Die()
    {
        StopCoroutine(HitReaction());
        LYJ_PoolManager.Instance.ReturnGameObject(gameObject);
    }
}
