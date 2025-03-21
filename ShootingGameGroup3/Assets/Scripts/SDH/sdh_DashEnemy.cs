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
    public Material flashM;
    public Material defaultM;

    float speed = 2f;
    bool isattack = false;
    float attdis = 3f;



    void Start()
    {
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

        if (!isattack)
        {
            MoveTowardPlayer();
            CheckFlip();
            if (Vector3.Distance(transform.position, pt.position) <= attdis)
            {
                Charge();
            }

        }



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
        col.isTrigger = true;
        sr.material = defaultM;
        anim.SetTrigger("Att");
        rb.AddForce(dir * 20, ForceMode2D.Impulse);
        StartCoroutine("cooldown");
    }

    IEnumerator cooldown()
    {
        yield return new WaitForSeconds(0.5f);
        col.isTrigger = false;
        rb.linearVelocity = Vector3.zero;
        isattack = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //플레이어와 충돌 시 플레이어 피격처리
    }
}
