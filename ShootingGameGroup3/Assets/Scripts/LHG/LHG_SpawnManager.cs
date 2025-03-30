using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LHG_SpawnManager : MonoBehaviour
{
    public GameObject monsterPrefab1; // 스폰할 첫 번째 몬스터 프리팹
    public GameObject monsterPrefab2; // 스폰할 두 번째 몬스터 프리팹
    public float spawnInterval = 2f; // 스폰 간격
    private float timer;
    private int monsterCount = 0; // 스폰 몬스터 수
    private int currentStage = 0; // 현재 스테이지
    private int[] stageMonsterCounts = { 3, 6, 9 }; // 각 스테이지에서의 최대 몬스터 수
    private int[] stageHealth = { 1, 2, 3 }; // 각 스테이지에서의 몬스터 체력
    private bool allMonstersDefeated = false; // 모든 몬스터가 처리되었는지 여부

    public Text gameOverText; // 게임 종료 텍스트 UI

    private void Start()
    {
        // 게임 종료 텍스트를 처음에 비활성화
        gameOverText.gameObject.SetActive(false);
    }

    private void Update()
    {
        // 타이머 업데이트
        timer += Time.deltaTime;

        // 스폰 간격이 지나고 현재 몬스터 수가 최대 수보다 적으면 몬스터 스폰
        if (timer >= spawnInterval && monsterCount < GetCurrentMaxMonsters() && !allMonstersDefeated)
        {
            SpawnMonster();
            timer = 0f; // 타이머 리셋
        }

        // 모든 몬스터가 처리되었는지 체크
        if (monsterCount >= GetCurrentMaxMonsters() && !allMonstersDefeated)
        {
            allMonstersDefeated = true;
            StartCoroutine(ProceedToNextStage());
        }
    }

    void SpawnMonster()
    {
        // 랜덤으로 몬스터 프리팹 선택
        GameObject selectedMonsterPrefab = Random.Range(0, 2) == 0 ? monsterPrefab1 : monsterPrefab2;

        // 선택된 몬스터 프리팹을 스폰
        GameObject monster = Instantiate(selectedMonsterPrefab, transform.position, Quaternion.identity);

        // 몬스터의 체력을 현재 스테이지에 맞게 설정
        LHG_Monster monsterScript = monster.GetComponent<LHG_Monster>();
        if (monsterScript != null)
        {
            monsterScript.health = GetCurrentMonsterHealth(); // 현재 스테이지에 맞는 체력 설정
        }

        monsterCount++; // 몬스터 수 증가
    }

    private int GetCurrentMaxMonsters()
    {
        // 현재 스테이지에 맞는 최대 몬스터 수 반환
        return stageMonsterCounts[currentStage];
    }

    private int GetCurrentMonsterHealth()
    {
        // 현재 스테이지에 맞는 몬스터 체력 반환
        return stageHealth[currentStage];
    }

    public void IncreaseStage()
    {
        // 스테이지 증가
        if (currentStage < stageMonsterCounts.Length - 1)
        {
            currentStage++;
        }
        else
        {
            // 모든 스테이지가 종료된 경우 게임 종료 텍스트 표시
            ShowGameOverText();
        }
    }

    private IEnumerator ProceedToNextStage()
    {
        // 5초 대기
        yield return new WaitForSeconds(5f);

        // 다음 스테이지로 진행하는 로직 추가
        Debug.Log("다음 스테이지로 진행합니다.");
        IncreaseStage(); // 스테이지 증가
        allMonstersDefeated = false; // 몬스터 처리 상태 리셋
        monsterCount = 0; // 몬스터 수 리셋
    }

    private void ShowGameOverText()
    {
        // 게임 종료 텍스트 활성화
        gameOverText.gameObject.SetActive(true);
        gameOverText.text = "게임 종료!"; // 텍스트 설정
    }
}

