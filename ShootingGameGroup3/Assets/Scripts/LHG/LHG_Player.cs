using System;
using UnityEngine;

public class LHG_Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    private float speed;

    // �뽬 ���� ����
    public float dashDistance = 2f; // �뽬 �Ÿ�
    public float dashCooldown = 5f; // ��Ÿ��
    private float lastDashTime; // ������ �뽬 �ð�

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

        // �뽬 �Է� ó��
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
        bullet.GetComponent<LHG_Bullet>().SetDirection(direction);
    }

    void Dash()
    {
        // �뽬 �̵�
        Vector2 dashDirection = new Vector2(transform.localScale.x, 0).normalized; // ���� �������� �뽬
        transform.Translate(dashDirection * dashDistance, Space.World);
        lastDashTime = Time.time; // ������ �뽬 �ð� ������Ʈ
    }
}