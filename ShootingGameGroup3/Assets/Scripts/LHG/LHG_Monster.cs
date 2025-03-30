using System.Collections;
using UnityEngine;

public class LHG_Monster : MonoBehaviour
{
    public int health = 3; // 몬스터의 체력
    public GameObject MiniMonsterPrefab; // 생성할 미니 몬스터 프리팹
    public int numberOfMiniMonster = 3; // 생성할 미니 몬스터의 수

    public float moveSpeed = 1f; // 몬스터의 이동 속도
    private Transform player; // 플레이어의 Transform
    public float chaseDistance = 10f; // 몬스터가 플레이어를 추적하는 거리

    private Vector3 randomDirection; // 랜덤 방향
    private float changeDirectionTime = 2f; // 방향 변경 주기
    private float timer; // 타이머

    private SpriteRenderer spriteRenderer; // 스프라이트 렌더러
    private Color originalColor; // 원래 색상

    private void Start()
    {
        // 태그가 "Player"인 게임 오브젝트를 찾아서 player 변수에 할당
        player = GameObject.FindGameObjectWithTag("Player").transform;
        SetRandomDirection(); // 초기 랜덤 방향 설정

        // 스프라이트 렌더러와 원래 색상 저장
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
            // 플레이어 방향 계산
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;

            // 플레이어 방향에 따라 몬스터 스프라이트 회전
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

            // 타이머 증가
            timer += Time.deltaTime;
            // 주기가 지나면 랜덤 방향 변경
            if (timer >= changeDirectionTime)
            {
                SetRandomDirection();
                timer = 0f; // 타이머 초기화
            }
        }
    }

    private void SetRandomDirection()
    {
        // -1과 1 사이의 랜덤 값을 생성하여 랜덤 방향 설정
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);
        randomDirection = new Vector3(randomX, randomY, 0).normalized;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 충돌한 오브젝트가 "Bullet" 태그를 가진 경우
        if (collision.CompareTag("Bullet"))
        {
            // 피해를 받음
            TakeDamage(1);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage; // 체력 감소
        if (health <= 0)
        {
            // 체력이 0 이하가 되면 미니 몬스터로 분리
            SplitIntoMiniMonster();
            Destroy(gameObject); // 몬스터 오브젝트 파괴
        }
        else
        {
            // 체력이 남아있으면 투명해지는 코루틴 시작
            StartCoroutine(BecomeTransparent(2f));
        }
    }

    private IEnumerator BecomeTransparent(float duration)
    {
        // 몬스터를 투명하게 만들기 위한 코루틴
        Color transparentColor = originalColor;
        transparentColor.a = 0.1f; // 투명도 설정
        spriteRenderer.color = transparentColor;

        yield return new WaitForSeconds(duration); // 지정된 시간 대기

        // 원래 색상으로 복원
        spriteRenderer.color = originalColor;
    }

    private void SplitIntoMiniMonster()
    {
        // 미니 몬스터 생성
        for (int i = 0; i < numberOfMiniMonster; i++)
        {
            // 랜덤한 위치에 미니 몬스터 생성
            Vector3 spawnPosition = transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
            Instantiate(MiniMonsterPrefab, spawnPosition, Quaternion.identity);
        }
    }
}