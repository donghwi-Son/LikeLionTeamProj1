using System.Collections;
using UnityEngine;

public class LSM_Muffin : MonoBehaviour
{
    public GameObject muffin_bullet; // 총알 프리팹
    public Transform pos; // 발사 위치
    public float shoot_delay = 10f; // 발사 간격
    public bool isReady = true; // 발사 준비 상태

    private float remainingCooldown = 0f;
    private bool isCooldown = false;

    void Update()
    {
        if (isCooldown)
        {
            remainingCooldown -= Time.deltaTime;
            if (remainingCooldown <= 0f)
            {
                isCooldown = false;
                isReady = true;
            }
        }
    }

    public void Shoot()
    {
        if (!isReady)
            return;

        Vector3 target_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        target_pos.z = 0;
        Vector3 dir = (target_pos - pos.position).normalized;
        GameObject bullet = Instantiate(muffin_bullet, pos.position, Quaternion.identity);
        bullet.GetComponent<LSM_MuffinBullet>().SetDirection(dir);

        isReady = false;
        isCooldown = true;
        remainingCooldown = shoot_delay;
    }

    private void OnEnable()
    {
        // 무기 활성화시 남은 쿨다운 확인
        if (remainingCooldown > 0f)
        {
            isCooldown = true;
            isReady = false;
        }
    }
}
