using UnityEngine;

public class LHG_SpawnManager : MonoBehaviour
{
    public GameObject monsterPrefab1; // 스폰할 첫 번째 몬스터 프리팹
    public GameObject monsterPrefab2; // 스폰할 두 번째 몬스터 프리팹
    public float spawnInterval = 2f; // 스폰 간격
    private float timer;
    private int monsterCount = 0; // 스폰 몬스터 수
    public int maxMonsters = 5; // 최대 몬스터 수

    private void Update()
    {
        // 타이머 업데이트
        timer += Time.deltaTime;

        // 스폰 간격이 지나고 현재 몬스터 수가 최대 수보다 적으면 몬스터 스폰
        if (timer >= spawnInterval && monsterCount < maxMonsters)
        {
            SpawnMonster();
            timer = 0f; // 타이머 리셋
        }
    }

    void SpawnMonster()
    {
        // 랜덤으로 몬스터 프리팹 선택
        GameObject selectedMonsterPrefab = Random.Range(0, 2) == 0 ? monsterPrefab1 : monsterPrefab2;

        // 선택된 몬스터 프리팹을 스폰
        Instantiate(selectedMonsterPrefab, transform.position, Quaternion.identity);
        monsterCount++; // 몬스터 수 증가
    }
}

