using UnityEngine;

public class LSM_Player : MonoBehaviour
{
    public GameObject gunPrefab; // 원본 총 프리팹
    private GameObject gunInstance; // 인스턴스화된 총
    public Transform gunPos;
    private Rigidbody2D rb;

    public float moveSpeed = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (gunPrefab != null && gunPos != null)
        {
            gunInstance = Instantiate(gunPrefab, gunPos.position, gunPos.rotation);
            gunInstance.transform.parent = gunPos; // 총을 플레이어의 손 위치에 고정
        }
        else
        {
            Debug.LogError("총 프리팹 또는 gunPos가 설정되지 않았습니다.");
        }
    }

    void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        Vector2 movement = new Vector2(moveX, moveY).normalized * moveSpeed;
        rb.linearVelocity = movement;

        // 입력이 없을 때 즉시 정지
        if (moveX == 0 && moveY == 0)
        {
            rb.linearVelocity = Vector2.zero;
        }
    }
}
