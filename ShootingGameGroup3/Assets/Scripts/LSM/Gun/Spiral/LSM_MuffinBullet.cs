using System.Collections;
using UnityEngine;

public class LSM_MuffinBullet : MonoBehaviour
{
    public Vector2 direction;
    private Vector2 startPosition;
    private float maxDistance = 5f;

    private Rigidbody2D rb;
    private float bullet_speed = 10f;
    private bool hasStartedMoving = false;
    private bool hasSmellsCalled = false;

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }

    void FixedUpdate()
    {
        float currentDistance = Vector2.Distance(startPosition, transform.position);

        if (!hasStartedMoving && currentDistance > 0.1f)
        {
            hasStartedMoving = true;
        }

        if (hasStartedMoving && currentDistance >= maxDistance)
        {
            rb.linearVelocity = Vector2.zero;

            if (!hasSmellsCalled)
            {
                hasSmellsCalled = true;
                Smells();
            }
            return;
        }

        rb.linearVelocity = direction * bullet_speed;
    }

    void Smells()
    {
        // 주변의 Monster 태그를 가진 모든 오브젝트를 찾음
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");

        // 각 몬스터의 isSmells를 true로 설정
        foreach (GameObject monster in monsters)
        {
            LSM_Monster monsterScript = monster.GetComponent<LSM_Monster>();
            if (monsterScript != null)
            {
                monsterScript.isSmells = true;
            }
        }

        Debug.Log("향기를 풍깁니다.");
        StartCoroutine(Destroy());
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
