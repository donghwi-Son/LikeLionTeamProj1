using UnityEngine;

public class Skeleton : MonoBehaviour
{
    public float health = 2f;
    public float moveSpeed = 1;
    public float detectionRange = 5f;
    public static int killedSkeletons = 0;
    private Transform player;
    private SpawnManager spawnManager;


    public float boneSpeed = 10f; // Bone �ӵ�
    public float fireRate = 2f; // �߻� ����
    private float nextFireTime = 1f;



    
    public AudioClip chw_throwBone;     // 뼈를 던질 때 재생할 사운드 클립 
    public AudioClip chw_dieSkeleton;   // 사망 사운드

    Animator eAnimator;
    public GameObject bonePrefab;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        eAnimator = GetComponent<Animator>();
        spawnManager = Object.FindFirstObjectByType<SpawnManager>();//spawnManager = FindObjectOfType<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null && Time.time >= nextFireTime)
        {
            float distance = Vector2.Distance(transform.position, player.position);
            if (distance <= detectionRange)
            {
                eAnimator.SetTrigger("Attack");
                ShootBone();
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    public void EnemyTakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("플레이어 체력: " + health);

        if (health <= 0)
        {
            // 스켈레톤 죽음 처리
            killedSkeletons++;
            spawnManager?.RemoveSkeleton(gameObject);
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(chw_dieSkeleton, transform.position);

            // 한 웨이브의 3마리가 죽었으면
            if (killedSkeletons >= 3)
            {
                killedSkeletons = 0; // 킬 카운터 초기화
                // SpawnManager에서 다음 웨이브를 시작하거나, 보스 소환 여부를 결정
                spawnManager?.TriggerNextWave();
            }

            Debug.Log("죽인 스켈레톤 수 : " + killedSkeletons);
        }


    }

    void ShootBone()
    {
        // Bone 
        GameObject bone = Instantiate(bonePrefab, transform.position, Quaternion.identity);

        //  (Player - BoneShooter ��ġ)
        Vector2 direction = (player.position - transform.position).normalized;

        // Bone�� Rigidbody2D 
        Rigidbody2D rb = bone.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.linearVelocity = direction * boneSpeed; // 속도 적용
            rb.angularVelocity = 360f; // 초당 360도 회전 (값을 조정 가능)
        }

        
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bone.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        AudioSource.PlayClipAtPoint(chw_throwBone, transform.position);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            EnemyTakeDamage(1);
            Debug.Log("닿았다");
        }
    }

}
