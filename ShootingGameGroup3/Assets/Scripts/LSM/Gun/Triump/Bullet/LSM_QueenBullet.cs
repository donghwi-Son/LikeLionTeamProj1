using UnityEngine;

public class LSM_QueenBullet : MonoBehaviour
{
    private const float speed = 15f; // 총알의 속도
    private const float rotateSpeed = 5f;
    private const float orbitRadius = 1.5f; // 회전 반지름

    private Transform target;
    private bool orbitPlayer = false;
    private bool isSecondBullet = false; // 두 번째 발인지 구분하는 변수
    private float lifetime = 0f;
    private float secondBulletDelay = 1f; // 두 번째 발이 대기하는 시간 (1초 예시)
    private float secondBulletTimer = 0f; // 대기 시간을 측정하는 타이머

    public float maxLifetime = 10f;
    public float targetChangeDistance = 10f; // 새로운 목표를 찾기 위한 거리
    private float angleOffset;

    public Transform player;

    // 총알이 목표를 설정하는 메서드
    public void SetTarget(GameObject enemy, bool isSecond = false)
    {
        isSecondBullet = isSecond;

        if (enemy != null)
        {
            target = enemy.transform;
            orbitPlayer = false;
        }
        else
        {
            // 씬에서 플레이어 찾기
            player = GameObject.FindWithTag("Player")?.transform;
            if (player != null)
            {
                orbitPlayer = true;
            }
            else
            {
                Destroy(gameObject); // 플레이어도 없으면 총알 제거
            }
        }
    }

    void Start()
    {
        angleOffset = Random.Range(0f, 360f); // 회전 각도 초기화
    }

    void Update()
    {
        lifetime += Time.deltaTime;
        secondBulletTimer += Time.deltaTime;

        // 두 번째 발 처리
        if (isSecondBullet && secondBulletTimer >= secondBulletDelay)
        {
            FindNewTarget(); // 새 목표 찾기
            if (target == null)
                target = Object.FindFirstObjectByType<LSM_QueenBullet>().target;
        }

        // 첫 번째 발 처리
        if (!isSecondBullet && target == null)
        {
            FindNewTarget(); // 목표를 찾지 못했다면 새로 찾기
        }

        if (target != null)
        {
            TrackEnemy(); // 목표를 추적
        }
        else if (orbitPlayer)
        {
            OrbitAroundPlayer(); // 목표 없으면 플레이어 주위를 회전
            FindNewTarget(); // 회전하면서도 적을 찾음
        }

        // 수명이 다하면 총알 제거
        if (lifetime >= maxLifetime)
        {
            Destroy(gameObject);
        }
    }

    // 목표 추적
    void TrackEnemy()
    {
        if (target == null)
            return;

        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
        transform.right = direction;
    }

    // 플레이어 주위를 회전
    void OrbitAroundPlayer()
    {
        if (player == null)
        {
            Destroy(gameObject);
            return;
        }

        float angle = Time.time * rotateSpeed + angleOffset;
        Vector3 offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * orbitRadius;
        transform.position = player.position + offset;
        transform.right = offset.normalized;
    }

    // 새로운 목표를 찾는 함수
    void FindNewTarget()
    {
        Collider2D[] monstersInRange = Physics2D.OverlapCircleAll(
            transform.position,
            targetChangeDistance
        );

        foreach (var monster in monstersInRange)
        {
            if (monster.CompareTag("Monster"))
            {
                SetTarget(monster.gameObject, isSecondBullet); // 두 번째 발은 isSecond 매개변수를 true로 설정
                break;
            }
        }
    }

    // 충돌 시 데미지 처리
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            int damage = 10;
            if (LSM_Triump.Instance.fatal) // fatal 상태라면 데미지 증가
            {
                damage += 30;
            }

            collision.gameObject.GetComponent<LSM_Monster>().Damage(damage); // 몬스터에 데미지 적용
            Destroy(gameObject); // 총알 제거
        }
    }
}
