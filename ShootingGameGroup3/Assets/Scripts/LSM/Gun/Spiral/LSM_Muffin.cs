using System.Collections.Generic;
using UnityEngine;

public class LSM_Muffin : MonoBehaviour
{
    public GameObject muffin_bullet; // 총알 프리팹
    public Transform pos; // 발사 위치

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 특정 키 입력 시 발사
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Vector3 target_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        target_pos.z = 0;
        Vector3 dir = (target_pos - pos.position).normalized;
        GameObject bullet = Instantiate(muffin_bullet, pos.position, Quaternion.identity);
        bullet.GetComponent<LSM_MuffinBullet>().SetDirection(dir);
    }
}
