/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Skeleton Settings")]
    public GameObject skeletonPrefab;
    public GameObject bossSkeletonPrefab;
    private List<GameObject> activeSkeletons = new List<GameObject>();

    private Vector3[] spawnOffsets = new Vector3[] {
        new Vector3(-3, 0, 0),
        new Vector3(0, 0, 0),
        new Vector3(3, 0, 0)
    };

    private int currentWave = 0;
    private int maxWaves = 3;
    private bool isBossSpawned = false;
    private bool isSpawningWave = false; // 웨이브 딜레이 중인지 확인

    private void Start()
    {
        StartCoroutine(StartWaveWithDelay());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && !isBossSpawned)
        {
            ForceSpawnBoss();
        }

        if (activeSkeletons.Count == 0 && currentWave < maxWaves && !isBossSpawned && !isSpawningWave)
        {
            StartCoroutine(StartWaveWithDelay());
        }
        else if (currentWave >= maxWaves && !isBossSpawned)
        {
            SpawnBoss();
        }
    }

    private IEnumerator StartWaveWithDelay()
    {
        isSpawningWave = true; // 딜레이 시작
        yield return new WaitForSeconds(2f); // 2초 대기

        StartWave();
        isSpawningWave = false; // 딜레이 끝
    }

    private void StartWave()
    {
        currentWave++;
        Debug.Log($"Wave {currentWave} 시작!");

        foreach (Vector3 offset in spawnOffsets)
        {
            Vector3 spawnPoint = transform.position + offset;
            GameObject skeleton = Instantiate(skeletonPrefab, spawnPoint, Quaternion.identity);
            activeSkeletons.Add(skeleton);
        }
    }

    public void RemoveSkeleton(GameObject skeleton)
    {
        if (activeSkeletons.Contains(skeleton))
        {
            activeSkeletons.Remove(skeleton);
            Destroy(skeleton);
        }
    }

    private void SpawnBoss()
    {
        isBossSpawned = true;
        Debug.Log("보스 스켈레톤 소환!");

        Vector3 bossPosition = transform.position;
        Instantiate(bossSkeletonPrefab, bossPosition, Quaternion.identity);
    }

    private void ForceSpawnBoss()
    {
        Debug.Log("현재 웨이브 강제 중단, 보스 스켈레톤 소환!");

        foreach (GameObject skeleton in activeSkeletons)
        {
            Destroy(skeleton);
        }
        activeSkeletons.Clear();

        SpawnBoss();
    }
}
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Skeleton Settings")]
    public GameObject skeletonPrefab;
    public GameObject bossSkeletonPrefab;
    private List<GameObject> activeSkeletons = new List<GameObject>();

    private Vector3[] spawnOffsets = new Vector3[] {
        new Vector3(-3, 0, 0),
        new Vector3(0, 0, 0),
        new Vector3(3, 0, 0)
    };

    private int currentWave = 0;
    private int maxWaves = 3;
    private bool isBossSpawned = false;
    private bool isSpawningWave = false; // 웨이브 딜레이 중인지 확인

    private void Start()
    {
        StartCoroutine(StartWaveWithDelay());
    }

    // 기존 Update에서 자동으로 웨이브 시작하는 로직은
    // killedSkeletons 카운트로 관리하므로 꼭 필요하지 않을 수 있습니다.
    // 혹은 디버깅 용도로 남겨둘 수 있습니다.
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && !isBossSpawned)
        {
            ForceSpawnBoss();
        }
    }

    private IEnumerator StartWaveWithDelay()
    {
        isSpawningWave = true; // 딜레이 시작
        yield return new WaitForSeconds(2f); // 2초 대기

        StartWave();
        isSpawningWave = false; // 딜레이 끝
    }

    private void StartWave()
    {
        currentWave++;
        Debug.Log($"Wave {currentWave} 시작!");

        foreach (Vector3 offset in spawnOffsets)
        {
            Vector3 spawnPoint = transform.position + offset;
            GameObject skeleton = Instantiate(skeletonPrefab, spawnPoint, Quaternion.identity);
            activeSkeletons.Add(skeleton);
        }
    }

    // 스켈레톤이 죽을 때 SpawnManager에서 제거할 때 사용
    public void RemoveSkeleton(GameObject skeleton)
    {
        if (activeSkeletons.Contains(skeleton))
        {
            activeSkeletons.Remove(skeleton);
            // Destroy(skeleton); // CHW_Skeleton에서 이미 Destroy() 호출됨
        }
    }

    // CHW_Skeleton에서 호출하여 다음 웨이브 또는 보스 소환을 트리거
    public void TriggerNextWave()
    {
        if (!isSpawningWave)
        {
            if (currentWave < maxWaves)
            {
                StartCoroutine(StartWaveWithDelay());
            }
            else if (!isBossSpawned)
            {
                SpawnBoss();
            }
        }
    }

    private void SpawnBoss()
    {
        isBossSpawned = true;
        Debug.Log("보스 스켈레톤 소환!");

        Vector3 bossPosition = transform.position;
        Instantiate(bossSkeletonPrefab, bossPosition, Quaternion.identity);
    }

    private void ForceSpawnBoss()
    {
        Debug.Log("현재 웨이브 강제 중단, 보스 스켈레톤 소환!");

        foreach (GameObject skeleton in activeSkeletons)
        {
            Destroy(skeleton);
        }
        activeSkeletons.Clear();

        SpawnBoss();
    }
}