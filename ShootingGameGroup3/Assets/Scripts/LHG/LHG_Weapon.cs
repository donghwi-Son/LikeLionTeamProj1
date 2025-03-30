using UnityEngine;

public class LHG_Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;

    public void Fire(Vector2 direction)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        LHG_Bullet bulletScript = bullet.GetComponent<LHG_Bullet>();
        if (bulletScript != null)
        {
            bulletScript.SetDirection(direction);
        }
    }
}
