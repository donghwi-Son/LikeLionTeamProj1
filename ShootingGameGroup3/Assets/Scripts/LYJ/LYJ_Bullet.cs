using UnityEngine;

public class LYJ_Bullet : MonoBehaviour
{
    Rigidbody2D _rb;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void ShootBullet(Vector2 targetVector, float speed)
    {
        transform.rotation = Quaternion.FromToRotation(Vector3.right, targetVector);
        _rb.linearVelocity = targetVector.normalized * speed;
    }

}
