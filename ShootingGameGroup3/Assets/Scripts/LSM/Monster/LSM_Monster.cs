using UnityEngine;

public class LSM_Monster : MonoBehaviour
{
    public Transform player;
    public SpriteRenderer spriteRenderer;

    public int health = 100;
    public int speed = 3;
    public bool invincibility = false;
    public bool isSmells = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Death();
        Tracking();
    }

    public void SetInvincibility(bool state)
    {
        invincibility = state;
        Debug.Log("무적 상태: " + (invincibility ? "활성화" : "비활성화"));
    }

    public void Damage(int damage)
    {
        if (invincibility)
        {
            Debug.Log("무적 상태");
            return;
        }

        health -= damage;
        Debug.Log("남은 체력: " + health);
    }

    public void Death()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            Debug.Log("몬스터가 죽었습니다.");
        }
    }

    public void Tracking()
    {
        if (player == null)
            return;

        if (isSmells)
        {
            GameObject[] muffins = GameObject.FindGameObjectsWithTag("Muffin");
            if (muffins.Length > 0)
            {
                GameObject nearestMuffin = null;
                float minDistance = float.MaxValue;

                foreach (GameObject muffin in muffins)
                {
                    float distance = Vector2.Distance(
                        transform.position,
                        muffin.transform.position
                    );
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        nearestMuffin = muffin;
                    }
                }

                if (nearestMuffin != null)
                {
                    Vector3 direction = nearestMuffin.transform.position - transform.position;
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
                    spriteRenderer.flipX = (direction.x > 0);
                }
            }
        }
        else
        {
            Vector3 direction = player.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
            spriteRenderer.flipX = (direction.x > 0);
        }
    }
}
