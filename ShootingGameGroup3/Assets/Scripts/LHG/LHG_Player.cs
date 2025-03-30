using System;
using UnityEngine;

public class LHG_Player : MonoBehaviour
{
    public float moveSpeed = 5f; // 플레이어의 이동 속도
    public GameObject bulletPrefab; // 발사할 총알 프리팹
    public Transform bulletSpawnPoint; // 총알이 발사될 위치
    private float speed; // 현재 속도 (사용되지 않음)

    // 대시 관련 변수
    public float dashDistance = 2f; // 대시 거리
    public float dashCooldown = 5f; // 대시 쿨타임
    private float lastDashTime; // 마지막 대시 시간

    void Update()
    {
        // 수평 입력 값 가져오기
        float moveInput = Input.GetAxis("Horizontal");

        // 플레이어 이동
        transform.Translate(new Vector2(moveInput * speed * Time.deltaTime, 0));

        // 플레이어의 방향에 따라 스케일 조정
        if (moveInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // 오른쪽 방향
        }
        else if (moveInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // 왼쪽 방향
        }

        // 이동 메서드 호출
        Move();

        // 마우스 왼쪽 버튼 클릭 시 총알 발사
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        // 스페이스바를 눌러 대시
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= lastDashTime + dashCooldown)
        {
            Dash();
        }
    }

    void Move()
    {
        // 수평 및 수직 입력 값 가져오기
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        // 이동 벡터 계산 및 정규화
        Vector2 movement = new Vector2(moveX, moveY).normalized * moveSpeed * Time.deltaTime;

        // 플레이어 이동
        transform.Translate(movement);
    }

    void Shoot()
    {
        // 총알 프리팹 인스턴스화
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);

        // 마우스 위치에서 총알 방향 계산
        Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - bulletSpawnPoint.position).normalized;

        // 총알의 방향 설정
        bullet.GetComponent<LHG_Bullet>().SetDirection(direction);
    }

    void Dash()
    {
        // 대시 방향 계산 (현재 플레이어의 방향)
        Vector2 dashDirection = new Vector2(transform.localScale.x, 0).normalized; // 현재 방향으로 대시
        transform.Translate(dashDirection * dashDistance, Space.World); // 대시 이동
        lastDashTime = Time.time; // 마지막 대시 시간 업데이트
    }
}