using UnityEngine;

public class LYJ_Money : MonoBehaviour
{
const float MAX_FLYING_TIME = 2f;

    float currentFlyingTime;
    Vector3 destVector;
    Rigidbody2D _rb;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        if (!gameObject.activeSelf) {return;}

        currentFlyingTime += Time.deltaTime; // 분리 필요한가..?

        if (Vector2.Distance(transform.position, destVector) <= 0.1f || currentFlyingTime >= MAX_FLYING_TIME)
        {
            _rb.linearVelocity = Vector2.zero;
            _rb.bodyType = RigidbodyType2D.Kinematic;
            return;
        }

    }

    public void ShootMoney(Vector2 targetVector, float speed)
    {
        destVector = (Vector2)transform.position + targetVector;
        transform.rotation = Quaternion.FromToRotation(Vector3.right, targetVector);
        _rb.linearVelocity = targetVector.normalized * speed;
        Destroy(gameObject, 4f);
    }


    
}
