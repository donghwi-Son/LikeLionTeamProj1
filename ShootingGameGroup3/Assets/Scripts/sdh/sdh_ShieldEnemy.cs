using System.Collections;
using Unity.Jobs;
using UnityEngine;

public class sdh_ShieldEnemy : MonoBehaviour
{
    Transform pt; // 플레이어 transform
    Rigidbody2D rb;
    Animator anim;
    AudioSource mys;
    Collider2D col;
    public Material flashM;
    public Material defaultM;
    SpriteRenderer sr;

    float speed = 1f;
    float turnDelay = 0.1f;
    float nextCheckTime = 0;
    float moveDelay = 1f;
    float moveTime = 0;
    float HP = 20;
    bool isAtt = false;
    bool isDead = false;
    bool isHit = false;
    public AudioClip hitSound;
    public AudioClip dieSound;
    public AudioClip AttSound;



    private void Start()
    {
        col = GetComponent<Collider2D>();
        mys = GetComponent<AudioSource>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            pt = playerObject.transform;
        }
    }

    private void Update()
    {
        if (!isDead)
        {
            if (!isAtt && !isHit)
            {
                if (Vector3.Distance(transform.position, pt.position) < 5f)
                {
                    MoveTowardPlayer();
                }
                else if (Time.time >= moveTime)
                {
                    if (Random.value > 0.5f)
                    {
                        DontMove();
                    }
                    else
                    {
                        MoveRandom();
                    }
                    moveTime = Time.time + moveDelay;
                }

                if (Time.time >= nextCheckTime)
                {
                    CheckFlipX();
                    nextCheckTime = Time.time + turnDelay; // 다음 체크 시간 갱신
                }
            }

            if (Vector3.Distance(transform.position, pt.position) < 1f && !isAtt)
            {
                StartCoroutine(Att());
            }
        }
    }

    IEnumerator Att()
    {
        isAtt = true;
        DontMove();
        sr.material = flashM;
        yield return new WaitForSeconds(0.5f);
        sr.material = defaultM;
        anim.SetTrigger("Att");
        mys.PlayOneShot(AttSound);
        yield return new WaitForSeconds(0.5f);
        isAtt = false;
    }

    void MoveRandom()
    {
        rb.linearVelocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * speed;
    }

    void MoveTowardPlayer()
    {
        rb.linearVelocity = (pt.position - transform.position).normalized * speed;
    }

    void DontMove()
    {
        rb.linearVelocity = Vector2.zero;
    }

    void CheckFlipX()
    {
        if (transform.position.x < pt.position.x) // 적이 왼쪽
        {
            if (Random.value > 0.5f) // 50% 확률로 행동
            {
                Invoke("TurnLeft", 0.5f);
            }
        }
        else
        {
            if (Random.value > 0.5f) // 50% 확률로 행동
            {
                Invoke("TurnRight", 0.5f);
            }
        }
    }

    void TurnRight()
    {
        Vector3 v = transform.rotation.eulerAngles;
        v.y = 180;
        transform.rotation = Quaternion.Euler(v);
    }

    void TurnLeft()
    {
        Vector3 v = transform.rotation.eulerAngles;
        v.y = 0;
        transform.rotation = Quaternion.Euler(v);
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
}