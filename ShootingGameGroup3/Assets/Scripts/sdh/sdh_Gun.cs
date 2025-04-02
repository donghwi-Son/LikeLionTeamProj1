using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class sdh_Gun : MonoBehaviour
{
    public GameObject bullet1; // 총알 프리팹
    public GameObject bullet2; // 총알 프리팹
    public GameObject bullet3; // 총알 프리팹
    public Transform firePoint;    // 총알 생성 위치

    public Material FlashM;
    public Material DefaultM;
    float chargetime = 0;
    SpriteRenderer spr;
    Coroutine flashRoutine;
    private void Start()
    {
        spr = GetComponent<SpriteRenderer>();
    }
    void Update()
    { 
        Vector3 mouseWP = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RotateTowardsMouse(mouseWP);






        if (Input.GetMouseButtonDown(0))
        {
            flashRoutineStart();
        }


        if (Input.GetMouseButton(0))
        {
            Charging();
        }



        if (Input.GetMouseButtonUp(0))
        {
            ShootOut(mouseWP);
        }


    }

    public void ShootOut(Vector3 mouseWP)
    {
        if (chargetime >= 2)
        {
            UltraStrongShoot(mouseWP);
        }
        else if (chargetime > 1)
        {
            StrongShoot(mouseWP);
        }

        else
        {
            Shoot(mouseWP);
        }
        chargetime = 0;
        if (flashRoutine != null)
        {
            StopCoroutine(flashRoutine);
            spr.material = DefaultM;
        }
    }
    public void Charging()
    {
        chargetime += Time.deltaTime;
        if (chargetime > 2)
        {
            if (flashRoutine != null)
            {
                StopCoroutine(flashRoutine);
                spr.material = FlashM;
            }
        }
    }
    public void flashRoutineStart()
    {
        flashRoutine = StartCoroutine(Flash());
    }
    IEnumerator Flash() // 2번 반짝이면 중공격
    {
        while (true)
        {
            yield return new WaitForSeconds(0.25f);
            spr.material = DefaultM;
            yield return new WaitForSeconds(0.25f);
            spr.material = FlashM;
        }
    }

    private void RotateTowardsMouse(Vector3 wp)
    {

        // 현재 오브젝트 위치에서 마우스 위치로의 방향 계산
        Vector3 direction = wp - transform.position;

        // Z축 회전을 위한 각도 계산
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // 현재 오브젝트의 회전 설정
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }


    public void Shoot(Vector3 wP)
    {
        Instantiate(bullet1, firePoint.position, Quaternion.identity);
    }
    void UltraStrongShoot(Vector3 wP)
    {
        Instantiate(bullet3, firePoint.position, Quaternion.identity);
    }
    void StrongShoot(Vector3 wP)
    {
        Instantiate(bullet2, firePoint.position, Quaternion.identity);
    }
}
