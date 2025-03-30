using UnityEngine;

public class Skeleton : MonoBehaviour
{
    public float moveSpeed = 1;
    public float detectionRange = 5f;
    private Transform player;



    public float boneSpeed = 10f; // Bone �ӵ�
    public float fireRate = 2f; // �߻� ����
    private float nextFireTime = 1f;



    Animator eAnimator;
    public GameObject bonePrefab;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        eAnimator = GetComponent<Animator>();
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
    }

}
