using UnityEngine;

public class LYJ_Bullet : MonoBehaviour
{
    Rigidbody2D _rb;
    float damage;
    public float Damage => damage;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void ShootBullet(Vector2 targetVector, float speed, float damage)
    {
        this.damage = damage;
        transform.rotation = Quaternion.FromToRotation(Vector3.right, targetVector);
        _rb.linearVelocity = targetVector.normalized * speed;
    }

}
