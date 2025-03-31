using System.Collections.Generic;
using UnityEngine;

public class LYJ_SpawnManager : MonoBehaviour
{
    List<GameObject> enemies;
    List<Transform> spawnPoints;
    float waveInterval;
    int currentWave;
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
        if (LYJ_GameManager.Instance.CurrentStageTime >= waveInterval)
        {
            StartWave();
        }
    }

    void StartWave()
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
