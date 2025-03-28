using System;
using UnityEngine;

public class LHG_Bullet : MonoBehaviour
{
    public float speed = 10f; // 발사체의 이동 속도
    public int damage = 1; // 발사체의 피해량
    public int bounceCount = 2; // 최대 바운스 횟수

    private Vector2 direction; // 발사체의 이동 방향
    private int currentBounces = 0; // 현재 바운스 횟수

    public GameObject Effect; // 발사체가 충돌했을 때 생성할 이펙트

    // 발사체의 방향을 설정하는 메서드
    public void SetDirection(Vector2 dir)
    {
        direction = dir;
    }

    void Update()
    {
        // 발사체를 현재 방향으로 이동시킴
        transform.Translate(direction * speed * Time.deltaTime);
        // 카메라의 뷰포트 좌표를 가져옴
        Vector3 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        // 총알이 화면 밖으로 나가면 파괴
        if (transform.position.x < -screenBounds.x || transform.position.x > screenBounds.x ||
            transform.position.y < -screenBounds.y || transform.position.y > screenBounds.y)
        {
            Destroy(gameObject);
        }
    }

    // 충돌이 발생했을 때 호출되는 메서드
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 충돌한 객체가 "Monster" 태그를 가진 경우
        if (collision.CompareTag("Monster"))
        {
            LHG_Monster monster = collision.GetComponent<LHG_Monster>();           
            if (monster != null)
            {
                // 몬스터에게 피해를 줌
                monster.TakeDamage(damage);
                CreateEffect(); // 이펙트 생성
                Bounce(collision.transform.position); // 바운스 처리
            }
        }
        // 충돌한 객체가 "MiniMonster" 태그를 가진 경우
        else if (collision.CompareTag("MiniMonster"))
        {
            LHG_MiniMonster miniMonster = collision.GetComponent<LHG_MiniMonster>();
            if (miniMonster != null)
            {
                // 미니 몬스터에게 피해를 줌
                miniMonster.TakeDamage(damage);
                CreateEffect(); // 이펙트 생성
                Bounce(collision.transform.position); // 바운스 처리
            }
        }
        // 충돌한 객체가 "Monster2" 태그를 가진 경우
        else if (collision.CompareTag("Monster2"))
        {
            LHG_Monster2 monster2 = collision.GetComponent<LHG_Monster2>();
            if (monster2 != null)
            {
                // 몬스터2에게 피해를 줌
                monster2.TakeDamage(damage);
                CreateEffect(); // 이펙트 생성
                Bounce(collision.transform.position); // 바운스 처리
            }
        }
    }

    // 이펙트를 생성하는 메서드
    void CreateEffect()
    {
        GameObject go = Instantiate(Effect, transform.position, Quaternion.identity);
        Destroy(go, 0.5f); // 0.5초 후 이펙트 오브젝트 삭제
    }

    // 바운스 처리 메서드
    void Bounce(Vector2 hitPoint)
    {
        // 최대 바운스 횟수에 도달하지 않은 경우
        if (currentBounces < bounceCount)
        {
            currentBounces++; // 바운스 횟수 증가
            Vector2 bounceDirection = (Vector2)transform.position - hitPoint; // 반사 방향 계산
            direction = bounceDirection.normalized; // 방향을 정규화

            FindClosestMonster(); // 가장 가까운 몬스터 찾기
        }
        else
        {
            Destroy(gameObject); // 바운스 횟수를 초과하면 발사체 삭제
        }
    }

    // 가장 가까운 몬스터를 찾는 메서드
    void FindClosestMonster()
    {
        // 현재 위치에서 10f 반경 내의 모든 콜라이더를 가져옴
        Collider2D[] monsters = Physics2D.OverlapCircleAll(transform.position, 10f);
        Transform closestMonsterTransform = null; // 가장 가까운 몬스터의 변환
        float closestDistance = Mathf.Infinity; // 가장 가까운 거리 초기화

        // 모든 몬스터에 대해 반복
        foreach (Collider2D collider in monsters)
        {
            // 몬스터 또는 미니 몬스터 태그를 가진 경우
            if (collider.CompareTag("Monster") || collider.CompareTag("MiniMonster"))
            {
                float distance = Vector2.Distance(transform.position, collider.transform.position); // 거리 계산
                if (distance < closestDistance) // 가장 가까운 몬스터 업데이트
                {
                    closestDistance = distance;
                    closestMonsterTransform = collider.transform;
                }
            }
        }

        // 가장 가까운 몬스터가 발견된 경우
        if (closestMonsterTransform != null)
        {
            Vector2 targetDirection = (closestMonsterTransform.position - transform.position).normalized; // 목표 방향 계산
            direction = targetDirection; // 방향 업데이트
        }
    }
}