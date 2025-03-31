using System.Collections;
using UnityEngine;

public class LYJ_Enemy_Oil : LYJ_NormalEnemy
{
    const float FIRE_CHANCE = 0.1f;
    const float BURN_DAMAGE = 0.5f;
    const float BURN_DELAY = 0.5f;
    float[] hpForWave = { 150, 200, 250, 300, 350 }; // temp
    float hp;
    private float moveSpeed = 1.5f;
    bool isHitRecent;
    int burnStack;

    Rigidbody2D _rb;
    WaitForSeconds oilDropInterval;
    WaitForSeconds burnDelay;
    SpriteRenderer spriteRenderer;
    [SerializeField]
    GameObject oil;

    Transform target;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        oilDropInterval = new WaitForSeconds(2f);
        burnDelay = new WaitForSeconds(BURN_DELAY);
        spriteRenderer = GetComponent<SpriteRenderer>();
        target = LYJ_GameManager.Instance.Player.transform;
        isHitRecent = false;
    }

    void OnEnable()
    {
        StartCoroutine(DropOil());
        hp = hpForWave[LYJ_GameManager.Instance.SpawnManager.CurrentWave];
        isHitRecent = false;
        burnStack = 0;
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
            BurnFire(FIRE_CHANCE);
        }
        if (collision.CompareTag("Alcohol"))
        {
            hp -= collision.GetComponent<LYJ_AlcoholBurner>().Damage;
            BurnFire(100);
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

    IEnumerator DropOil()
    {
        while (gameObject.activeSelf)
        {
            yield return oilDropInterval;
            Instantiate(oil, transform.position, Quaternion.identity); // 나중에 오브젝트 풀링으로 수정 필요
        }
    }

    void BurnFire(float percent)
    {
        if (burnStack >= 3) { burnStack = 3; }
        if (Random.value <= percent)
        {
            StartCoroutine(Burn());
        }
    }

    IEnumerator HitReaction()
    {
        isHitRecent = true;
        yield return new WaitForSeconds(0.1f);
        isHitRecent = false;
    }

    IEnumerator Burn()
    {
        while (hp >= 0 && burnStack <= 3)
        {
            hp -= BURN_DAMAGE * burnStack;
            yield return burnDelay;
        }
    }

    void Die()
    {
        StopCoroutine(HitReaction());
        StopCoroutine(Burn());
        StopCoroutine(DropOil());
        burnStack = 0;
        gameObject.SetActive(false);
    }
}
