using System.Collections;
using UnityEngine;

public class sdh_Player : MonoBehaviour
{
    
    SpriteRenderer psr; 
    SpriteRenderer gunsr;
    Rigidbody2D rb;
    float speed = 3f;
    public Transform gunT;
    public Transform fp;
    bool isRolling = false;
    bool isDashCool = false;


    void Start()
    {
<<<<<<< HEAD
        // SpriteRenderer ì»´í¬ë„ŒíŠ¸ ê°€ì ¸ì˜¤ê¸°
=======
        rb = GetComponent<Rigidbody2D>();
>>>>>>> SDH
        psr = GetComponent<SpriteRenderer>();
        gunsr = gunT.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!isRolling)
        {
            Move();
            FlipPlayer();
        }
        FlipGun();
        Roll();
    }

    private void FlipPlayer()
    {
        // ë§ˆìš°ìŠ¤ ìœ„ì¹˜ ê³„ì‚°
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // ë§ˆìš°ìŠ¤ ìœ„ì¹˜ì— ë”°ë¼ Flip ì„¤ì •
        if (mousePosition.x < transform.position.x)
        {
            // ë§ˆìš°ìŠ¤ê°€ ì™¼ìª½ì¼ ë•Œ
            psr.flipX = true;
<<<<<<< HEAD
            gunsr.flipY = true; // ì´ë„ ìˆ˜ì§ ë°˜ì „
            fp.localPosition = new Vector3(fp.localPosition.x, -Mathf.Abs(fp.localPosition.y), fp.localPosition.z);
 
=======
>>>>>>> SDH
        }
        else
        {
            // ë§ˆìš°ìŠ¤ê°€ ì˜¤ë¥¸ìª½ì¼ ë•Œ
            psr.flipX = false;
<<<<<<< HEAD
            gunsr.flipY = false; // ì´ ì›ë˜ ë°©í–¥
=======
        }
    }

    void FlipGun()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // ¸¶¿ì½º À§Ä¡¿¡ µû¶ó Flip ¼³Á¤
        if (mousePosition.x < transform.position.x)
        {
            gunsr.flipY = true; // ÃÑ ¼öÁ÷ ¹İÀü
            fp.localPosition = new Vector3(fp.localPosition.x, -Mathf.Abs(fp.localPosition.y), fp.localPosition.z);

        }
        else
        {
            gunsr.flipY = false; // ÃÑ ¿ø·¡ ¹æÇâ
>>>>>>> SDH
            fp.localPosition = new Vector3(fp.localPosition.x, Mathf.Abs(fp.localPosition.y), fp.localPosition.z);
        }
    }

    private void Move()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        Vector3 vec = new Vector3(moveX, moveY, 0);

        if (vec.magnitude > 1) // ëŒ€ê°ì„  ì´ë™ ì‹œ ì •ê·œí™”
        {
            vec.Normalize();
        }

        rb.linearVelocity = vec * speed;
    }

    void Roll()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !isRolling && !isDashCool)
        {
            StartCoroutine(StartRoll());
        }
    }

    IEnumerator StartRoll()
    {
        isRolling = true;
        isDashCool = true;
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        Vector3 dir = new Vector3(moveX, moveY, 0).normalized;
        if (dir == Vector3.zero)
        {
            Vector3 wP = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 dirvec = wP - transform.position;
            dir = new Vector3(dirvec.x, dirvec.y, 0).normalized;
        }
        rb.AddForce(dir * 10, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.2f);
        rb.linearVelocity = Vector2.zero;
        isRolling = false;
        yield return new WaitForSeconds(2.8f);
        isDashCool = false;
    }

    void GetHit()
    {
        //ÇÇ°İÃ³¸®
    }
}
