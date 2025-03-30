using System.Collections.Generic;
using UnityEngine;

public class LYJ_SpawnManager : MonoBehaviour
{
    List<GameObject> enemies;
    List<Transform> spawnPoints;
    float waveInterval = 30f;
    int currentWave = 1;
    public int CurrentWave => currentWave;

    void Awake()
    {
        foreach (var item in enemies)
        {
            LYJ_PoolManager.Instance.CreatePool(item, 10);
        }
    }
    void Update()
    {
        if (currentWave > 5) { return; }
        if (LYJ_GameManager.Instance.CurrentStageTime % waveInterval == 0)
        {
            StartNewWave();
        }
    }

    void StartNewWave()
    {
        for (int i = 0; i < 5+currentWave; ++i)
        {
            int randomPointNo = Random.Range(0, spawnPoints.Count);
            GameObject enemy = LYJ_PoolManager.Instance.GetGameObject(enemies[i]);
            enemy.transform.position = spawnPoints[randomPointNo].position;
        }
        currentWave++;
    }
}
