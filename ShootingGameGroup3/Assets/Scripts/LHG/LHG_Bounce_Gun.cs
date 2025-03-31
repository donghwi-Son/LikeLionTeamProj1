using UnityEngine;

public class LHG_Bounce_Gun : MonoBehaviour
{
    public GameObject bulletPrefab; // 발사할 총알 프리팹
    public Transform firePoint; // 총알이 발사될 위치

    void Update()
    {
        // 마우스 왼쪽 버튼 클릭 시 총알 발사
        if (Input.GetMouseButtonDown(0))
        {
            // 마우스 위치에서 총알 방향 계산
            Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - firePoint.position).normalized;

            // 주어진 방향으로 총알 발사
            Fire(direction);
        }
    }

    // 주어진 방향으로 총알을 발사하는 메서드
    public void Fire(Vector2 direction)
    {
        // 총알 프리팹 인스턴스화
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // 총알의 LHG_Bullet 스크립트 가져오기
        LHG_Bullet bulletScript = bullet.GetComponent<LHG_Bullet>();

        // 총알 스크립트가 존재하는 경우 방향 설정
        if (bulletScript != null)
        {
            bulletScript.SetDirection(direction); // 총알의 방향 설정
        }
    }

    // 게임 매니저에서 호출할 수 있는 공개 메서드
    public void FireAtDirection(Vector2 direction)
    {
        Fire(direction);
    }
}