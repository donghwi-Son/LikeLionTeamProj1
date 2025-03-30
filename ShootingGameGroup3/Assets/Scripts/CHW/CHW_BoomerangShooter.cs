using UnityEngine;

public class CHW_BoomerangShooter : MonoBehaviour
{
    public Transform player;
    public GameObject boomerangPrefab;
    public Vector2 offset = new Vector2(0.5f, 0f);
    private bool facingRight = true;

    [Header("Boomerang Settings")]
    private bool canThrowBoomerang = true;
    private GameObject currentBoomerang;


    [Header("Boomerang Sound")]
    // 부메랑이 회수될 때까지 반복 재생할 사운드 클립 (Inspector에서 할당)
    public AudioClip chw_boomerang;
    private AudioSource audioSource;

    private void Start()
    {
        // AudioSource 컴포넌트가 없으면 추가
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        // 사운드 클립 할당 및 루프 설정
        audioSource.clip = chw_boomerang;
        audioSource.loop = true;
    }

    private void Update()
    {
        FollowPlayer();
        if (Input.GetMouseButtonDown(0) && canThrowBoomerang)
        {
            ThrowBoomerang();
        }
    }

    private void FollowPlayer()
    {
        if (player == null) return;

        Vector3 playerPosition = player.position;
        float direction = player.localScale.x;

        if ((direction > 0 && !facingRight) || (direction < 0 && facingRight))
        {
            facingRight = !facingRight;
            offset.x = -offset.x; // 좌우 반전
        }

        transform.position = new Vector3(playerPosition.x + offset.x, playerPosition.y + offset.y, 0f);
    }

    private void ThrowBoomerang()

     
    {
        if (!canThrowBoomerang) return;
        canThrowBoomerang = false;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        Vector3 throwDirection = (mousePosition - transform.position).normalized;
        GameObject boomerang = Instantiate(boomerangPrefab, transform.position, Quaternion.identity);

        CHW_Boomerang boomerangScript = boomerang.GetComponent<CHW_Boomerang>();
        if (boomerangScript != null)
        {
            boomerangScript.Initialize(transform, throwDirection);
        }
        else
        {
            Debug.LogError("Boomerang script not found on prefab!");
        }
        // 부메랑이 회수되기 전까지 사운드 루프 재생
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }


    public void RetrieveBoomerang()
    {
        canThrowBoomerang = true;
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}