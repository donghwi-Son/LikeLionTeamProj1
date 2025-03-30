using UnityEngine;

public class LSM_CardBullet : MonoBehaviour
{
    void Start() { }

    void Update() { }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
