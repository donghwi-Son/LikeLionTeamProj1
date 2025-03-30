using UnityEngine;

public class LHG_Weapon : MonoBehaviour
{
    public GameObject bulletPrefab; // 발사할 총알 프리팹
    public Transform firePoint; // 총알이 발사될 위치

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
}
