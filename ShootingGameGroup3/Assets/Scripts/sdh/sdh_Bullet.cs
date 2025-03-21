using UnityEngine;

public class sdh_Bullet : MonoBehaviour
{
    float speed = 10f; // 총알 속도
    public float dmg;
    private Vector3 direction; // 총알이 날아갈 방향
    Rigidbody2D rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector3 wP = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 dirvec = wP - transform.position;
        direction = new Vector3(dirvec.x, dirvec.y, 0).normalized;


        float angle = Mathf.Atan2(direction.y , direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);


        rb.AddForce(direction*speed, ForceMode2D.Impulse);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            Debug.Log("적을 때림");
        }
        if (collision.CompareTag("Shield"))
        {
            Destroy(gameObject);
            Debug.Log("방패에 막힘");
        }
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
