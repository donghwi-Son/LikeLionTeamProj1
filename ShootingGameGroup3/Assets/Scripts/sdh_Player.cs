using UnityEngine;

public class sdh_Player : MonoBehaviour
{
    float speed = 5f;
    public Transform gunT;
    private SpriteRenderer psr; 
    private SpriteRenderer gunsr;
    public Transform fp;


    void Start()
    {
        // SpriteRenderer 컴포넌트 가져오기
        psr = GetComponent<SpriteRenderer>();
        gunsr = gunT.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Move();
        FlipPlayerAndGun();
    }

    private void FlipPlayerAndGun()
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

        transform.Translate(vec * speed * Time.deltaTime);
    }


}
