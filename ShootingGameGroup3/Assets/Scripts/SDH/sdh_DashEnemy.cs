using System.Collections;
using System.Reflection;
using UnityEngine;

public class sdh_DashEnemy : MonoBehaviour
{
    Transform pt;
    SpriteRenderer sr;
    Rigidbody2D rb;
    Animator anim;
    Collider2D col;
    AudioSource mys;
    public AudioClip hitSound;
    public AudioClip dieSound;
    public AudioClip attSound;
    public Material flashM;
    public Material defaultM;

    float HP = 100f;
    float speed = 4f;
    bool isattack = false;
    bool isDead = false;
    bool isHit = false;
    bool startMV = false;
    float attdis = 3f;

    void Start()
    {
        mys = GetComponent<AudioSource>();
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            pt = playerObject.transform;
        }
    }

    void Update()
    {
        if (startMV)
        {
            if (!isattack && !isDead)
            {
                if (!isHit)
                {
                    MoveTowardPlayer();
                    CheckFlip();
                }
                if (Vector3.Distance(transform.position, pt.position) <= attdis)
                {
                    Charge();
                }
            }
        }
    }

    public void GetDmg(float dmg)
    {
        mys.PlayOneShot(hitSound);
        HP -= dmg;
        StartCoroutine(Hit());
        if (HP <= 0)
        {
            DontMove();
            Die();
        }
    }

    IEnumerator Hit()
    {
        DontMove();
        isHit = true;
        sr.material = flashM;
        yield return new WaitForSeconds(0.1f);
        sr.material = defaultM;
        isHit = false;
    }

    void DontMove()
    {
        rb.linearVelocity = Vector2.zero;
    }

    public void StartMove()
    {
        startMV = true;
    }
    void Die()
    {
        mys.PlayOneShot(dieSound);
        col.enabled = false;
        isDead = true;
        anim.SetTrigger("Die");
        Invoke("Disappear", 1f);
    }

    void Disappear()
    {
        Destroy(gameObject);
    }

    void CheckFlip()
    {
        if (pt.position.x < transform.position.x)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
    }

    void MoveTowardPlayer()
    {
        rb.linearVelocity = (pt.position - transform.position).normalized * speed;
    }

    void Charge()
    {
        Debug.Log("충전");
        rb.linearVelocity = Vector3.zero;
        anim.SetTrigger("Charge");
        sr.material = flashM;
        isattack = true;
        Vector3 dir = (pt.position - transform.position).normalized;
        StartCoroutine(Slash(dir));
    }

    IEnumerator Slash(Vector3 dir)
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log("발사");
        mys.PlayOneShot(attSound);
        col.isTrigger = true;
        sr.material = defaultM;
        anim.SetTrigger("Att");
        rb.AddForce(dir * 15, ForceMode2D.Impulse);
        StartCoroutine("cooldown");
    }

    IEnumerator cooldown()
    {
        yield return new WaitForSeconds(0.5f);
        col.isTrigger = false;
        rb.linearVelocity = Vector3.zero;
        yield return new WaitForSeconds(0.5f);
        isattack = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어와 충돌 시 플레이어 피격처리
    }
}