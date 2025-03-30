using UnityEngine;

public class LHG_MiniMonster : MonoBehaviour
{
    public int health = 3; // 적의 체력
    public float moveSpeed = 1f; // 적의 이동 속도
    private Transform player; // 플레이어의 Transform
    public float chaseDistance = 7f; // 추적 거리

    private void Start()
    {
        // 태그가 "Player"인 게임 오브젝트를 찾아서 player 변수에 할당
        player = GameObject.FindGameObjectWithTag("Player").transform;
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

            // 적의 위치를 플레이어 방향으로 이동
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }

    // 적이 피해를 받을 때 호출되는 메서드
    public void TakeDamage(int damage)
    {
        health -= damage; // 체력 감소
        if (health <= 0)
        {
            // 체력이 0 이하가 되면 적 오브젝트 파괴
            Destroy(gameObject);
        }
    }

    // 충돌이 발생했을 때 호출되는 메서드
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 충돌한 오브젝트가 "Bullet" 태그를 가진 경우
        if (collision.CompareTag("Bullet"))
        {
            // 피해를 받음
            TakeDamage(1);
        }
    }
}