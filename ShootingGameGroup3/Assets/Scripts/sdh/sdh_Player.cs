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
        // SpriteRenderer 컴포넌트 가져오기
        rb = GetComponent<Rigidbody2D>();
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
        // 마우스 위치 계산
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // 마우스 위치에 따라 Flip 설정
        if (mousePosition.x < transform.position.x)
        {
            // 마우스가 왼쪽일 때
            psr.flipX = true;
            gunsr.flipY = true; // 총도 수직 반전
            fp.localPosition = new Vector3(fp.localPosition.x, -Mathf.Abs(fp.localPosition.y), fp.localPosition.z);
 
        }
        else
        {
            // 마우스가 오른쪽일 때
            psr.flipX = false;
            gunsr.flipY = false; // 총 원래 방향
        }
    }

    void FlipGun()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // ���콺 ��ġ�� ���� Flip ����
        if (mousePosition.x < transform.position.x)
        {
            gunsr.flipY = true; // �� ���� ����
            fp.localPosition = new Vector3(fp.localPosition.x, -Mathf.Abs(fp.localPosition.y), fp.localPosition.z);

        }
        else
        {
            gunsr.flipY = false; // �� ���� ����
            fp.localPosition = new Vector3(fp.localPosition.x, Mathf.Abs(fp.localPosition.y), fp.localPosition.z);
        }
    }

    private void Move()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        Vector3 vec = new Vector3(moveX, moveY, 0);

        if (vec.magnitude > 1) // 대각선 이동 시 정규화
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

    public void GetHit()
    {
        Debug.Log("플레이어 피격");
    }
}
