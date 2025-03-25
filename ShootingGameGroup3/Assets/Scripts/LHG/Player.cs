using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    private float speed;

    // 대쉬 관련 변수
    public float dashDistance = 2f; // 대쉬 거리
    public float dashCooldown = 5f; // 쿨타임
    private float lastDashTime; // 마지막 대쉬 시간

    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");

        transform.Translate(new Vector2(moveInput * speed * Time.deltaTime, 0));

        if (moveInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (moveInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        Move();

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        // 대쉬 입력 처리
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= lastDashTime + dashCooldown)
        {
            Dash();
        }
    }

    void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveX, moveY).normalized * moveSpeed * Time.deltaTime;
        transform.Translate(movement);
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - bulletSpawnPoint.position).normalized;
        bullet.GetComponent<Bullet>().SetDirection(direction);
    }

    void Dash()
    {
        // 대쉬 이동
        Vector2 dashDirection = new Vector2(transform.localScale.x, 0).normalized; // 현재 방향으로 대쉬
        transform.Translate(dashDirection * dashDistance, Space.World);
        lastDashTime = Time.time; // 마지막 대쉬 시간 업데이트
    }
}