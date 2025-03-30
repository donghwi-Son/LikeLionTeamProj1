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
