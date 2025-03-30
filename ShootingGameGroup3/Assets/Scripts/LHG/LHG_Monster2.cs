using System.Collections;
using UnityEngine;

public class LHG_Monster2 : MonoBehaviour
{
    public int health = 10; // 몬스터의 체력
    public float moveSpeed = 2f; // 몬스터의 이동 속도
    private Transform player; // 플레이어의 Transform
    public float chaseDistance = 10f; // 플레이어를 추적할 거리

    private Vector3 randomDirection; // 랜덤 방향
    private float changeDirectionTime = 3f; // 방향 변경 주기
    private float timer; // 타이머

    private SpriteRenderer spriteRenderer; // 스프라이트 렌더러
    private Color originalColor; // 원래 색상

    public AudioClip deathSound; // 몬스터 죽을 때 재생할 사운드 클립
    private AudioSource audioSource; // AudioSource 컴포넌트

    private void Start()
    {
        // 플레이어의 Transform을 찾고 랜덤 방향을 설정
        player = GameObject.FindGameObjectWithTag("Player").transform;
        SetRandomDirection();

        // 스프라이트 렌더러와 원래 색상 초기화
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    private void Update()
    {
        // 매 프레임마다 Move 메서드 호출
        Move();
    }

    private void Move()
    {
        // 플레이어와의 거리 계산
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // 플레이어가 추적 거리 이내에 있을 경우
        if (distanceToPlayer < chaseDistance)
        {
            // 플레이어 방향으로 이동
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;

            // 플레이어 방향에 따라 스프라이트 회전
            if (direction.x > 0)
            {
                spriteRenderer.flipX = false; // 오른쪽
            }
            else if (direction.x < 0)
            {
                spriteRenderer.flipX = true; // 왼쪽
            }
        }
        else
        {
            // 랜덤 방향으로 이동
            transform.position += randomDirection * moveSpeed * Time.deltaTime;

            // 방향 변경 타이머 업데이트
            timer += Time.deltaTime;
            if (timer >= changeDirectionTime)
            {
                SetRandomDirection(); // 랜덤 방향 설정
                timer = 0f; // 타이머 초기화
            }
        }
    }

    private void SetRandomDirection()
    {
        // 랜덤한 x, y 값을 생성하여 랜덤 방향 설정
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);
        randomDirection = new Vector3(randomX, randomY, 0).normalized; // 정규화하여 방향 설정
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 총알과 충돌 시 데미지 처리
        if (collision.CompareTag("Bullet"))
        {
            TakeDamage(1); // 1의 데미지 받기
        }
    }

    public void TakeDamage(int damage)
    {
        // 체력 감소
        health -= damage;
        if (health <= 0)
        {
            
            Destroy(gameObject); // 현재 몬스터 오브젝트 삭제
        }
        else
        {
            StartCoroutine(BecomeTransparent(2f)); // 투명해지는 코루틴 시작
        }
    }

    private IEnumerator BecomeTransparent(float duration)
    {
        // 몬스터를 투명하게 만드는 코루틴
        Color transparentColor = originalColor;
        transparentColor.a = 0.1f; // 투명도 설정
        spriteRenderer.color = transparentColor; // 색상 변경

        yield return new WaitForSeconds(duration); // 지정된 시간 대기

        spriteRenderer.color = originalColor; // 원래 색상으로 복원
    }

    private void PlayDeathSound()
    {
        if (deathSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(deathSound); // 죽을 때 사운드 재생
        }
    }
}
