using UnityEngine;

public class LSM_KingBullet : MonoBehaviour
{
    public int baseDamage = 10;

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
        LSM_Monster monster = collision.gameObject.GetComponent<LSM_Monster>();
        if (monster == null)
            return;

        // 치명 상태일 경우 데미지 증가
        int damage = baseDamage;
        if (LSM_Triump.Instance.fatal)
        {
            damage += 30;
        }

        // 방어무시 적용 여부에 따른 데미지 계산
        int finalDamage = monster.invincibility ? damage : damage * 2;

        // 몬스터에게 데미지 적용
        monster.Damage(finalDamage);

        // 데미지 로그
        Debug.Log($"데미지({(monster.invincibility ? "방어무시 적용" : "방어무시 미적용")}): {finalDamage}");

        // 총알 삭제
        Destroy(gameObject);
    }
}
