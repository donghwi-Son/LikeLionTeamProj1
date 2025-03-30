using UnityEngine;

public class LSM_JackBullet : MonoBehaviour
{
    public int baseDamage = 10;
    private int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            HandleMonsterCollision(collision);
        }
    }

    // 몬스터와 충돌 시 처리하는 함수
    private void HandleMonsterCollision(Collider2D collision)
    {
        // 충정 상태 설정
        LSM_Triump.Instance.loyalty = true;
        Debug.Log("충정");

        // 치명 상태인 경우 데미지 증가
        damage = baseDamage;
        if (LSM_Triump.Instance.fatal)
        {
            damage += 30;
            Debug.Log("치명 상태: 데미지 증가");
        }

        // 몬스터에게 데미지 적용
        LSM_Monster monster = collision.gameObject.GetComponent<LSM_Monster>();
        if (monster != null)
        {
            monster.Damage(damage);
        }

        // 총알 삭제
        Destroy(gameObject);
    }
}
