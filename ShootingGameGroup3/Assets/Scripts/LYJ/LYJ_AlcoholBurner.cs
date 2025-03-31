using System.Collections;
using UnityEngine;

public class LYJ_AlcoholBurner : MonoBehaviour
{
    #region Basic Stats
    private float _damage;
    public float Damage => _damage;

    #endregion

    WaitForSeconds rotationTerm;
    Rigidbody2D _rb;
    float moveSpeed;

    float timer;

    Vector2 targetVec;
    [SerializeField]
    GameObject boom;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        rotationTerm = new WaitForSeconds(0.5f);
    }

    void OnEnable()
    {
        StartCoroutine(RotateRandom());
        moveSpeed = 3f;
        _damage = 4f;
        targetVec = GameManager.Instance.MouseManager.GetMousePos()-transform.position;
    }

    void Update()
    {
        if (!gameObject.activeSelf) { return; }
        timer += Time.deltaTime;
        if (timer > 5f) { PoolManager.Instance.ReturnGameObject(gameObject); }
    }

    void FixedUpdate()
    {
        if (!gameObject.activeSelf) { return; }
        Vector2 newDir = Vector2.Lerp(_rb.linearVelocity.normalized, targetVec.normalized, 0.3f);
        // targetVec = Vector2.Lerp(targetVec, transform.up, 0.1f);
        _rb.linearVelocity = newDir * moveSpeed;

        float angle = Mathf.Atan2(newDir.y, newDir.x) * Mathf.Rad2Deg;
        _rb.MoveRotation(Mathf.LerpAngle(_rb.rotation, angle, 0.1f));
    }

    IEnumerator RotateRandom()
    {
        while (gameObject.activeSelf)
        {
            yield return rotationTerm;
            float randomAngle = Random.Range(-40, 40);
            targetVec = Quaternion.Euler(0, 0, randomAngle) * targetVec;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!gameObject.activeSelf) { return; }
        if (!collision.CompareTag("Monster")) { return; }
        Instantiate(boom, transform.position, Quaternion.identity);
        PoolManager.Instance.ReturnGameObject(gameObject);
    }
}
